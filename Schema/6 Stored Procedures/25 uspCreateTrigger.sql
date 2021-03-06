IF OBJECT_ID('uspCreateTrigger') IS NOT NULL
	DROP PROCEDURE uspCreateTrigger
GO

CREATE PROCEDURE uspCreateTrigger(@Id INT)
AS
SET NOCOUNT ON

DECLARE @TableName VARCHAR(100)
DECLARE @IdentifierColumnName VARCHAR(100)
DECLARE @SeparatorColumnName VARCHAR(100)
DECLARE @SeparatorColumnId INT
DECLARE @Track BIT
DECLARE @Enable BIT
DECLARE @ParentTableName VARCHAR(100)
DECLARE @CommandTextInitialization VARCHAR(MAX) = ''
DECLARE @CommandTextColumn VARCHAR(MAX) = ''
DECLARE @CommandTextFinalization VARCHAR(MAX) = ''
DECLARE @TriggerName VARCHAR(100)
DECLARE @NewLine VARCHAR(1) = CHAR(13)

SELECT @TableName = TableName,
	@IdentifierColumnName = IdentifierColumnName,
	@SeparatorColumnName = ISNULL(SeparatorColumnName, ''),
	@SeparatorColumnId = ISNULL(SeparatorColumnId, ''),
	@Track = Track,
	@Enable = [Enable]
FROM tblSystemLogTableConfig
WHERE Id = @Id

SELECT @ParentTableName = t.TableName
FROM tblSystemLogTableLinks tl
	INNER JOIN tblSystemLogTableConfig t ON t.Id = tl.TableId
WHERE tl.ChildTableId = @Id

SET @TriggerName = @TableName + '_INSERT_UPDATE_DELETE'

--Drop trigger if existing
IF EXISTS(SELECT * FROM sys.triggers WHERE name = @TriggerName)
	EXEC('DROP TRIGGER ' + @TriggerName)

--Do not create trigger if it is not tagged as track
IF @Track = 0
	RETURN

--Recreate trigger
SET @CommandTextInitialization =
	'CREATE TRIGGER ' + @TriggerName + ' ON ' + @TableName + ' FOR INSERT, UPDATE, DELETE' + @NewLine +
	'AS' + @NewLine +
	'SET NOCOUNT ON' + @NewLine +
	'BEGIN'
SET @CommandTextInitialization +=
	@NewLine

--Declarations for Table
SET @CommandTextInitialization +=
	'DECLARE @IsNew BIT' + @NewLine +
	'DECLARE @IsEdit BIT' + @NewLine +
	'DECLARE @IsDelete BIT' + @NewLine +
	'DECLARE @Mode VARCHAR(50)' + @NewLine +
	'DECLARE @TableName VARCHAR(100)' + @NewLine +
	'DECLARE @SeparatorId INT' + @NewLine +
	'DECLARE @TableId INT' + @NewLine +
	'DECLARE @tmp TABLE(TableId INT, ColumnId INT, CompanyId INT, MasterId INT, DetailId INT,' + @NewLine +
	'	OldValue VARCHAR(MAX), NewValue VARCHAR(MAX), New BIT, Edit BIT, [Delete] BIT)' + @NewLine +
	@NewLine

--Set Bits
SET @CommandTextInitialization +=
	'SET @IsNew = CASE WHEN EXISTS(SELECT * FROM Inserted) AND NOT EXISTS(SELECT * FROM Deleted) THEN 1 ELSE 0 END' + @NewLine +
	'SET @IsEdit = CASE WHEN EXISTS(SELECT * FROM Inserted) AND EXISTS(SELECT * FROM Deleted) THEN 1 ELSE 0 END' + @NewLine +
	'SET @IsDelete = CASE WHEN NOT EXISTS(SELECT * FROM Inserted) AND EXISTS(SELECT * FROM Deleted) THEN 1 ELSE 0 END' + @NewLine +
	@NewLine

--Set Mode
SET @CommandTextInitialization +=
	'IF @IsNew = 1' + @NewLine +
	'	SET @Mode = ''New''' + @NewLine +
	'IF @IsEdit = 1' + @NewLine +
	'	SET @Mode = ''Edit''' + @NewLine +
	'IF @IsDelete = 1' + @NewLine +
	'	SET @Mode = ''Delete''' + @NewLine +
	@NewLine

--Set TableName
SET @CommandTextInitialization +=
	'SET @TableName = ''' + @TableName + '''' + @NewLine +
	@NewLine

--Set TableId
IF NULLIF(@SeparatorColumnName, '') IS NULL
	SET @CommandTextInitialization +=
		'SELECT @TableId = Id' + @NewLine +
		'	FROM tblSystemLogTableConfig' + @NewLine +
		'	WHERE TableName = @TableName' + @NewLine +
		@NewLine
ELSE
BEGIN
	SET @CommandTextInitialization +=
		'IF @IsNew = 1' + @NewLine +
		'	BEGIN' + @NewLine +
		'		SELECT @SeparatorId = Inserted.' + @SeparatorColumnName + @NewLine +
		'		FROM Inserted' + @NewLine +
		'	END' + @NewLine +
		'	ELSE' + @NewLine +
		'	BEGIN' + @NewLine +
		'		SELECT @SeparatorId = Deleted.' + @SeparatorColumnName + @NewLine +
		'		FROM Deleted' + @NewLine +
		'	END' + @NewLine +
		@NewLine +

		'	SELECT @TableId = Id' + @NewLine +
		'	FROM tblSystemLogTableConfig' + @NewLine +
		'	WHERE TableName = @TableName' + @NewLine +
		'		AND SeparatorColumnId = @SeparatorId' + @NewLine +
		@NewLine
END

DECLARE @col TABLE(KeyId INT IDENTITY(1, 1), ColumnName VARCHAR(100), DataType VARCHAR(100),
	TableSouce VARCHAR(100), TableSourceResult VARCHAR(100) PRIMARY KEY(KeyId))
DECLARE @KeyId INT
DECLARE @ColumnName VARCHAR(100)
DECLARE @DataType VARCHAR(100)
DECLARE @TableSource VARCHAR(100)
DECLARE @TableSourceResult VARCHAR(100)

INSERT INTO @col(ColumnName, DataType, TableSouce, TableSourceResult)
SELECT ColumnName, DataType, TableSource, TableSourceResult
FROM tblSystemLogColumnConfig
WHERE TableId = @Id
	AND Track = 1
ORDER BY [Index]

--Declarations for Columns
SET @CommandTextColumn +=
	'DECLARE @ColumnId INT' + @NewLine +
	'DECLARE @CompanyId INT' + @NewLine +
	'DECLARE @MasterId INT' + @NewLine +
	'DECLARE @DetailId INT' + @NewLine +
	'DECLARE @OldValue VARCHAR(MAX)' + @NewLine +
	'DECLARE @NewValue VARCHAR(MAX)' + @NewLine +
	@NewLine

WHILE 1 = 1
BEGIN
	SELECT TOP 1
		@KeyId = KeyId,
		@ColumnName = ColumnName,
		@DataType = DataType,
		@TableSource = TableSouce,
		@TableSourceResult = TableSourceResult
	FROM @col

	--Check columns
	SET @CommandTextColumn +=
		'--' + @ColumnName + @NewLine +
		'SET @OldValue = NULL' + @NewLine +
		'SET @NewValue = NULL' + @NewLine +
		@NewLine +

		'IF @IsNew = 1' + @NewLine +
		'BEGIN' + @NewLine

		IF NULLIF(@ParentTableName, '') IS NULL
		BEGIN
			SET @CommandTextColumn +=
				'	SET @CompanyId = (SELECT Inserted.CompanyId FROM Inserted)' + @NewLine +
				'	SET @MasterId = (SELECT Inserted.Id FROM Inserted)' + @NewLine
		END
		ELSE
		BEGIN
			SET @CommandTextColumn +=
				'	SET @CompanyId = (SELECT p.CompanyId FROM Inserted i INNER JOIN ' + @ParentTableName +' p ON p.Id = i.' + @IdentifierColumnName +')' + @NewLine +
				'	SET @MasterId = (SELECT Inserted.' + @IdentifierColumnName +' FROM Inserted)' + @NewLine +
				'	SET @DetailId = (SELECT Inserted.Id FROM Inserted)' + @NewLine
		END

		IF @DataType = 'bit'
			SET @CommandTextColumn +=
				'	SET @NewValue = (SELECT CASE WHEN Inserted.' + @ColumnName + ' = 1 THEN ''True'' ELSE ''False'' END FROM Inserted)' + @NewLine
		ELSE IF @DataType = 'dateTime'
			SET @CommandTextColumn +=
				'	SET @NewValue = (SELECT CONVERT(VARCHAR, Inserted.' + @ColumnName + ', 101) FROM Inserted)' + @NewLine
		ELSE
		BEGIN
			IF NULLIF(@TableSource, '') IS NULL
				SET @CommandTextColumn +=
					'	SET @NewValue = (SELECT Inserted.' + @ColumnName + ' FROM Inserted)' + @NewLine
			ELSE
				SET @CommandTextColumn +=
					'	SET @NewValue = (SELECT ' + @TableSourceResult +' FROM ' + @TableSource + ' WHERE Id = (SELECT Inserted.' + @ColumnName + ' FROM Inserted))' + @NewLine
		END

	SET @CommandTextColumn +=
		'END' + @NewLine +
		'ELSE' + @NewLine +
		'BEGIN' + @NewLine

		IF NULLIF(@ParentTableName, '') IS NULL
		BEGIN
			SET @CommandTextColumn +=
				'	SET @CompanyId = (SELECT Deleted.CompanyId FROM Deleted)' + @NewLine +
				'	SET @MasterId = (SELECT Deleted.Id FROM Deleted)' + @NewLine
		END
		ELSE
		BEGIN
			SET @CommandTextColumn +=
				'	SET @CompanyId = (SELECT p.CompanyId FROM Deleted d INNER JOIN ' + @ParentTableName + ' p ON p.Id = d.' + @IdentifierColumnName +')' + @NewLine +
				'	SET @MasterId = (SELECT Deleted.' + @IdentifierColumnName +' FROM Deleted)' + @NewLine +
				'	SET @DetailId = (SELECT Deleted.Id FROM Deleted)' + @NewLine
		END

		IF @DataType = 'bit'
		BEGIN
			SET @CommandTextColumn +=
				'	SET @NewValue = (SELECT CASE WHEN Inserted.' + @ColumnName + ' = 1 THEN ''True'' ELSE ''False'' END FROM Inserted)' + @NewLine +
				'	SET @OldValue = (SELECT CASE WHEN Deleted.' + @ColumnName + ' = 1 THEN ''True'' ELSE ''False'' END FROM Deleted)' + @NewLine +
				'END' + @NewLine +
				@NewLine
		END
		ELSE IF @DataType = 'datetime'
		BEGIN
			SET @CommandTextColumn +=
				'	SET @NewValue = (SELECT CONVERT(VARCHAR, Inserted.' + @ColumnName + ', 101) FROM Inserted)' + @NewLine +
				'	SET @OldValue = (SELECT CONVERT(VARCHAR, Deleted.' + @ColumnName + ', 101) FROM Deleted)' + @NewLine +
				'END' + @NewLine +
				@NewLine
		END
		ELSE
		BEGIN
			IF NULLIF(@TableSource, '') IS NULL
			BEGIN
				SET @CommandTextColumn +=
					'	SET @NewValue = (SELECT Inserted.' + @ColumnName + ' FROM Inserted)' + @NewLine +
					'	SET @OldValue = (SELECT Deleted.' + @ColumnName + ' FROM Deleted)' + @NewLine +
					'END' + @NewLine +
					@NewLine
			END
			ELSE
			BEGIN
				SET @CommandTextColumn +=
					'	SET @NewValue = (SELECT ' + @TableSourceResult + ' FROM ' + @TableSource + ' WHERE Id = (SELECT Inserted.' + @ColumnName + ' FROM Inserted))' + @NewLine +
					'	SET @OldValue = (SELECT ' + @TableSourceResult + ' FROM ' + @TableSource + ' WHERE Id = (SELECT Deleted.' + @ColumnName + ' FROM Deleted))' + @NewLine +
					'END' + @NewLine +
					@NewLine
			END
		END

	SET @CommandTextColumn +=
		'IF ISNULL(@OldValue, '''') <> ISNULL(@NewValue, '''')' + @NewLine +
		'BEGIN' + @NewLine +
		'	SELECT @ColumnId = Id' + @NewLine +
		'	FROM tblSystemLogColumnConfig' + @NewLine +
		'	WHERE TableId = @TableId' + @NewLine +
		'		AND ColumnName = ''' + @ColumnName + '''' + @NewLine +
		@NewLine +
		'	INSERT INTO @tmp(TableId, ColumnId, CompanyId, MasterId, DetailId, OldValue, NewValue, New, Edit, [Delete])' + @NewLine +
		'	SELECT @TableId, @ColumnId, @CompanyId, @MasterId, @DetailId, @OldValue, @NewValue, @IsNew, @IsEdit, @IsDelete' + @NewLine +
		'END' + @NewLine +
		@NewLine

	DELETE
	FROM @col
	WHERE KeyId = @KeyId

	IF NOT EXISTS(SELECT * FROM @col)
		BREAK
END

--Set User and DateTime
SET @CommandTextFinalization +=
	'DECLARE @SystemUserId INT' + @NewLine +
	'DECLARE @DateTime DATETIME' + @NewLine +
	@NewLine

	IF NULLIF(@ParentTableName, '') IS NULL
	BEGIN
		SET @CommandTextFinalization +=
			'IF @IsNew = 1 OR @IsEdit = 1' + @NewLine +
			'BEGIN' + @NewLine +
			'	SET @SystemUserId = (SELECT Inserted.ModifiedById FROM Inserted)' + @NewLine +
			'	SET @DateTime = (SELECT Inserted.DateModified FROM Inserted)' + @NewLine +
			'END' + @NewLine +
			'ELSE' + @NewLine +
			'BEGIN' + @NewLine +
			'	SET @SystemUserId = (SELECT Deleted.ModifiedById FROM Deleted)' + @NewLine +
			'	SET @DateTime = (SELECT Deleted.DateModified FROM Deleted)' + @NewLine +
			'END' + @NewLine +
			@NewLine
	END
	ELSE
	BEGIN
		SET @CommandTextFinalization +=
			'IF @IsNew = 1 OR @IsEdit = 1' + @NewLine +
			'BEGIN' + @NewLine +
			'	SET @SystemUserId = (SELECT p.ModifiedById FROM Inserted i INNER JOIN ' + @ParentTableName +' p ON p.Id = i.' + @IdentifierColumnName + ')' + @NewLine +
			'	SET @DateTime = (SELECT p.DateModified FROM Inserted i INNER JOIN ' + @ParentTableName +' p ON p.Id = i.' + @IdentifierColumnName + ')' + @NewLine +
			'END' + @NewLine +
			'ELSE' + @NewLine +
			'BEGIN' + @NewLine +
			'	SET @SystemUserId = (SELECT p.ModifiedById FROM Deleted d INNER JOIN ' + @ParentTableName +' p ON p.Id = d.' + @IdentifierColumnName + ')' + @NewLine +
			'	SET @DateTime = (SELECT p.DateModified FROM Deleted d INNER JOIN ' + @ParentTableName +' p ON p.Id = d.' + @IdentifierColumnName + ')' + @NewLine +
			'END' + @NewLine +
			@NewLine
	END

--Insert to physical table
SET @CommandTextFinalization +=
	'INSERT INTO tblSystemLogs(TableId, ColumnId, CompanyId, MasterId, DetailId,' + @NewLine +
	'	OldValue, NewValue, New, Edit, [Delete], SystemUserId, [DateTime])' + @NewLine +
	'SELECT TableId, ColumnId, CompanyId, MasterId, DetailId,' + @NewLine +
	'	OldValue, NewValue, New, Edit, [Delete], @SystemUserId, @DateTime' + @NewLine +
	'FROM @tmp' + @NewLine

--End
SET @CommandTextFinalization +=
	'END'

EXEC(@CommandTextInitialization + @CommandTextColumn + @CommandTextFinalization)

UPDATE tblSystemLogTableConfig
SET TriggerName = @TriggerName
WHERE TableName = @TableName

--Error
--IF @Enable = 1
--	EXEC('ENABLE TRIGGER ' + @TriggerName + ' ON dbo.' + @TableName)
--ELSE
--	EXEC('DISABLE TRIGGER ' + @TriggerName + ' ON dbo.' + @TableName)
GO

