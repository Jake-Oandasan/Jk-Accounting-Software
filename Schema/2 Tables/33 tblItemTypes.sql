IF OBJECT_ID('tblItemTypes') IS NULL
	CREATE TABLE tblItemTypes(
		Id INT IDENTITY(1, 1) NOT NULL,
		Code VARCHAR(50) NOT NULL,
		Name VARCHAR(100) NULL,
		Active BIT NOT NULL CONSTRAINT DF_tblItemTypes_Active DEFAULT 1,
	)
GO

IF OBJECT_ID('tblItemTypes_PK') IS NOT NULL
	ALTER TABLE tblItemTypes DROP CONSTRAINT tblItemTypes_PK
GO

ALTER TABLE tblItemTypes ADD CONSTRAINT tblItemTypes_PK PRIMARY KEY(Id)
GO

