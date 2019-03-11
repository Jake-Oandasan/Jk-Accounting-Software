IF OBJECT_ID('uspUpdateJournals') IS NOT NULL
	DROP PROCEDURE uspUpdateJournals
GO

CREATE PROCEDURE uspUpdateJournals(@JournalTypeId INT, @Id INT, @IsPost BIT)
AS
SET NOCOUNT ON

DECLARE @JournalId INT
DECLARE @TransactionNo VARCHAR(50)
DECLARE @Code VARCHAR(50)
DECLARE @CompanyId INT
DECLARE @IsNew BIT = 0

DECLARE @InputVATAccountId INT
DECLARE @PayableAccountId INT
DECLARE @OutputVATAccountId INT
DECLARE @ReceivableAccountId INT
DECLARE @CustomerOverPaymentAccountId INT
DECLARE @PaymentMethodName VARCHAR(100)
DECLARE @WithholdingTaxAccountId INT

SELECT @Code = Code
FROM tblJournalTypes
WHERE Id = @JournalTypeId

IF @JournalTypeId = 1
	SELECT @JournalId = JournalId,
		@CompanyId = CompanyId
	FROM tblJournalVouchers
	WHERE Id = @Id
ELSE IF @JournalTypeId = 2
	SELECT @JournalId = JournalId,
		@CompanyId = CompanyId
	FROM tblBills
	WHERE Id = @Id
ELSE IF @JournalTypeId = 3
	SELECT @JournalId = JournalId,
		@CompanyId = CompanyId
	FROM tblSalesInvoices
	WHERE Id = @Id
ELSE IF @JournalTypeId = 4
	SELECT @JournalId = JournalId,
		@CompanyId = CompanyId
	FROM tblCashReceiptVouchers
	WHERE Id = @Id
ELSE IF @JournalTypeId = 5
	SELECT @JournalId = JournalId,
		@CompanyId = CompanyId
	FROM tblBillsPayment
	WHERE Id = @Id

SELECT @InputVATAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'INPUT VAT'

SELECT @PayableAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'ACCOUNTS PAYABLE'

SELECT @OutputVATAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'OUTPUT VAT'

SELECT @ReceivableAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'ACCOUNTS RECEIVABLE'

SELECT @CustomerOverPaymentAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'CUSTOMER OVERPAYMENT'

SELECT @WithholdingTaxAccountId = a.Id
FROM tblAccounts a
	INNER JOIN tblSystemAccountCodes ac ON ac.Id = a.SystemAccountCodeId
WHERE a.CompanyId = @CompanyId
	AND ac.Name = 'WITHHOLDING TAX PAYABLE'

IF @IsPost = 1
BEGIN
	IF @JournalTypeId = 1
	BEGIN
		IF @JournalId IS NULL
		BEGIN
			INSERT INTO tblJournals(CompanyId, JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, SourceId, SourceTransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified)
			SELECT CompanyId, @JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, Id, TransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified
			FROM tblJournalVouchers
			WHERE Id = @Id
				AND @JournalTypeId = 1
	
			SET @JournalId = SCOPE_IDENTITY()

			UPDATE tblJournalVouchers
			SET JournalId = @JournalId
			WHERE Id = @Id

			SET @IsNew = 1
		END
		ELSE
		BEGIN
			UPDATE j
			SET j.[Date] = jv.[Date],
				j.ReferenceNo = jv.ReferenceNo,
				j.ReferenceNo2 = jv.ReferenceNo2,
				j.Remarks = jv.Remarks,
				j.ModifiedById = jv.ModifiedById,
				j.DateModified = jv.DateModified
			FROM tblJournals j
				INNER JOIN tblJournalVouchers jv ON jv.JournalId = j.Id
			WHERE j.Id = @JournalId
				AND @JournalTypeId = 1
		END
	
		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks
		FROM tblJournalVoucherDetails
		WHERE JournalVoucherId = @Id
	END
	ELSE IF @JournalTypeId = 2
	BEGIN
		IF @JournalId IS NULL
		BEGIN
			INSERT INTO tblJournals(CompanyId, JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, SourceId, SourceTransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified)
			SELECT CompanyId, @JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, Id, TransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified
			FROM tblBills
			WHERE Id = @Id
				AND @JournalTypeId = 2
	
			SET @JournalId = SCOPE_IDENTITY()

			UPDATE tblBills
			SET JournalId = @JournalId
			WHERE Id = @Id

			SET @IsNew = 1
		END
		ELSE
		BEGIN
			UPDATE j
			SET j.[Date] = pv.[Date],
				j.ReferenceNo = pv.ReferenceNo,
				j.ReferenceNo2 = pv.ReferenceNo2,
				j.Remarks = pv.Remarks,
				j.ModifiedById = pv.ModifiedById,
				j.DateModified = pv.DateModified
			FROM tblJournals j
				INNER JOIN tblBills pv ON pv.JournalId = j.Id
			WHERE j.Id = @JournalId
				AND @JournalTypeId = 2
		END
	
		--Purchases
		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, AccountId, SubsidiaryId, SUM(GrossAmount), 0, Remarks
		FROM tblBillDetails
		WHERE BillId = @Id
		GROUP BY AccountId, SubsidiaryId, Remarks

		--Input VAT
		IF EXISTS(
			SELECT *
			FROM tblBills
			WHERE Id = @Id
				AND VATAmount > 0
		)
		AND @InputVATAccountId IS NULL
		BEGIN
			RAISERROR('No Input VAT Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, @InputVATAccountId, SubsidiaryId, SUM(VATAmount), 0, Remarks
		FROM tblBillDetails
		WHERE BillId = @Id
		GROUP BY SubsidiaryId, Remarks
		HAVING SUM(VATAmount) > 0

		--Payable
		IF @PayableAccountId IS NULL
		BEGIN
			RAISERROR('No Payable Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, @PayableAccountId, SubsidiaryId, 0, NetAmount, Remarks
		FROM tblBills
		WHERE Id = @Id
	END
	ELSE IF @JournalTypeId = 3
	BEGIN
		IF @JournalId IS NULL
		BEGIN
			INSERT INTO tblJournals(CompanyId, JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, SourceId, SourceTransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified)
			SELECT CompanyId, @JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, Id, TransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified
			FROM tblSalesInvoices
			WHERE Id = @Id
				AND @JournalTypeId = 3
	
			SET @JournalId = SCOPE_IDENTITY()

			UPDATE tblSalesInvoices
			SET JournalId = @JournalId
			WHERE Id = @Id

			SET @IsNew = 1
		END
		ELSE
		BEGIN
			UPDATE j
			SET j.[Date] = si.[Date],
				j.ReferenceNo = si.ReferenceNo,
				j.ReferenceNo2 = si.ReferenceNo2,
				j.Remarks = si.Remarks,
				j.ModifiedById = si.ModifiedById,
				j.DateModified = si.DateModified
			FROM tblJournals j
				INNER JOIN tblSalesInvoices si ON si.JournalId = j.Id
			WHERE j.Id = @JournalId
				AND @JournalTypeId = 3
		END

		IF @ReceivableAccountId IS NULL
		BEGIN
			RAISERROR('No Receivable Account has been set-up.', 11, 1)
			RETURN
		END
	
		--Receivable
		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, @ReceivableAccountId, SubsidiaryId, NetAmount, 0, Remarks
		FROM tblSalesInvoices
		WHERE Id = @Id

		--Sales
		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, AccountId, SubsidiaryId, 0, SUM(GrossAmount), Remarks
		FROM tblSalesInvoiceDetails
		WHERE SalesInvoiceId = @Id
		GROUP BY AccountId, SubsidiaryId, Remarks

		--Output VAT
		IF EXISTS(
			SELECT *
			FROM tblSalesInvoices
			WHERE Id = @Id
				AND VATAmount > 0
		)
		AND @OutputVATAccountId IS NULL
		BEGIN
			RAISERROR('No Output VAT Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit, Remarks)
		SELECT @JournalId, @OutputVATAccountId, SubsidiaryId, 0, SUM(VATAmount), Remarks
		FROM tblSalesInvoiceDetails
		WHERE SalesInvoiceId = @Id
		GROUP BY SubsidiaryId, Remarks
		HAVING SUM(VATAmount) > 0
	END
	ELSE IF @JournalTypeId = 4
	BEGIN
		--Post payment to invoice
		EXEC uspUpdateInvoiceAmounts @Id, @IsPost

		IF @JournalId IS NULL
		BEGIN
			INSERT INTO tblJournals(CompanyId, JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, SourceId, SourceTransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified)
			SELECT CompanyId, @JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, Id, TransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified
			FROM tblCashReceiptVouchers
			WHERE Id = @Id
				AND @JournalTypeId = 4
	
			SET @JournalId = SCOPE_IDENTITY()

			UPDATE tblCashReceiptVouchers
			SET JournalId = @JournalId
			WHERE Id = @Id

			SET @IsNew = 1
		END
		ELSE
		BEGIN
			UPDATE j
			SET j.[Date] = cv.[Date],
				j.ReferenceNo = cv.ReferenceNo,
				j.ReferenceNo2 = cv.ReferenceNo2,
				j.Remarks = cv.Remarks,
				j.ModifiedById = cv.ModifiedById,
				j.DateModified = cv.DateModified
			FROM tblJournals j
				INNER JOIN tblCashReceiptVouchers cv ON cv.JournalId = j.Id
			WHERE j.Id = @JournalId
				AND @JournalTypeId = 4
		END

		SELECT @PaymentMethodName = pm.Name
		FROM tblCashReceiptVoucherDetails cpd
			INNER JOIN tblPaymentMethods pm ON pm.Id = cpd.PaymentMethodId
		WHERE cpd.CashReceiptVoucherId = @Id
			AND pm.AccountId IS NULL

		IF NULLIF(@PaymentMethodName, '') IS NOT NULL
		BEGIN
			RAISERROR('Please set-up account for this Payment Method: %s', 11, 1, @PaymentMethodName)
			RETURN
		END

		--Payment
		INSERT INTO tblJournalDetails(JournalId, AccountId, Debit, Credit)
		SELECT @JournalId, pm.AccountId, SUM(cvd.Amount), 0
		FROM tblCashReceiptVoucherDetails cvd
			INNER JOIN tblPaymentMethods pm ON pm.Id = cvd.PaymentMethodId
		WHERE cvd.CashReceiptVoucherId = @Id
		GROUP BY pm.AccountId

		--Receivable
		IF @ReceivableAccountId IS NULL
		BEGIN
			RAISERROR('No Receivable Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit)
		SELECT @JournalId, @ReceivableAccountId, cv.SubsidiaryId, 0, SUM(cid.AppliedAmount)
		FROM tblCashReceiptVoucherInvoiceDetails cid
			INNER JOIN tblCashReceiptVouchers cv ON cv.Id = cid.CashReceiptVoucherId
		WHERE cid.CashReceiptVoucherId = @Id
		GROUP BY cv.SubsidiaryId

		--Overpayment
		IF @CustomerOverPaymentAccountId IS NULL
			AND EXISTS(
				SELECT cv.Id
				FROM tblCashReceiptVouchers cv
					INNER JOIN (
						SELECT cd.CashReceiptVoucherId, SUM(cd.Amount) AS Amount
						FROM tblCashReceiptVoucherDetails cd
						GROUP BY cd.CashReceiptVoucherId
					) cd ON cd.CashReceiptVoucherId = cv.Id
					INNER JOIN (
						SELECT cid.CashReceiptVoucherId, SUM(cid.AppliedAmount) AS AppliedAmount
						FROM tblCashReceiptVoucherInvoiceDetails cid
						GROUP BY cid.CashReceiptVoucherId
					) cid ON cid.CashReceiptVoucherId = cv.Id
				WHERE cv.Id = @Id
					AND cd.Amount <> cid.AppliedAmount
			)
		BEGIN
			RAISERROR('No Customer Overpayment Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit)
		SELECT @JournalId, @CustomerOverPaymentAccountId, cv.SubsidiaryId, 0, cd.Amount - cid.AppliedAmount
		FROM tblCashReceiptVouchers cv
			INNER JOIN (
				SELECT cd.CashReceiptVoucherId, SUM(cd.Amount) AS Amount
				FROM tblCashReceiptVoucherDetails cd
				GROUP BY cd.CashReceiptVoucherId
			) cd ON cd.CashReceiptVoucherId = cv.Id
			INNER JOIN (
				SELECT cid.CashReceiptVoucherId, SUM(cid.AppliedAmount) AS AppliedAmount
				FROM tblCashReceiptVoucherInvoiceDetails cid
				GROUP BY cid.CashReceiptVoucherId
			) cid ON cid.CashReceiptVoucherId = cv.Id
		WHERE cv.Id = @Id
			AND cd.Amount > cid.AppliedAmount
	END
	ELSE IF @JournalTypeId = 5
	BEGIN
		--Post payment to bills
		EXEC uspUpdateBillsAmounts @Id, @IsPost

		IF @JournalId IS NULL
		BEGIN
			INSERT INTO tblJournals(CompanyId, JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, SourceId, SourceTransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified)
			SELECT CompanyId, @JournalTypeId, TransactionNo, [Date],
				ReferenceNo, ReferenceNo2, Remarks, Id, TransactionNo,
				CreatedById, DateCreated, ModifiedById, DateModified
			FROM tblBillsPayment
			WHERE Id = @Id
				AND @JournalTypeId = 5
	
			SET @JournalId = SCOPE_IDENTITY()

			UPDATE tblBillsPayment
			SET JournalId = @JournalId
			WHERE Id = @Id

			SET @IsNew = 1
		END
		ELSE
		BEGIN
			UPDATE j
			SET j.[Date] = bp.[Date],
				j.ReferenceNo = bp.ReferenceNo,
				j.ReferenceNo2 = bp.ReferenceNo2,
				j.Remarks = bp.Remarks,
				j.ModifiedById = bp.ModifiedById,
				j.DateModified = bp.DateModified
			FROM tblJournals j
				INNER JOIN tblBillsPayment bp ON bp.JournalId = j.Id
			WHERE j.Id = @JournalId
				AND @JournalTypeId = 5
		END

		SELECT @PaymentMethodName = pm.Name
		FROM tblBillsPaymentDetails bpd
			INNER JOIN tblPaymentMethods pm ON pm.Id = bpd.PaymentMethodId
		WHERE bpd.BillsPaymentId = @Id
			AND pm.AccountId IS NULL

		--Payable
		IF @PayableAccountId IS NULL
		BEGIN
			RAISERROR('No Payable Account has been set-up.', 11, 1)
			RETURN
		END

		INSERT INTO tblJournalDetails(JournalId, AccountId, SubsidiaryId, Debit, Credit)
		SELECT @JournalId, @PayableAccountId, bp.SubsidiaryId, SUM(bpbd.AppliedAmount), 0
		FROM tblBillsPaymentBillDetails bpbd
			INNER JOIN tblBillsPayment bp ON bp.Id = bpbd.BillsPaymentId
		WHERE bpbd.BillsPaymentId = @Id
		GROUP BY bp.SubsidiaryId

		IF NULLIF(@PaymentMethodName, '') IS NOT NULL
		BEGIN
			RAISERROR('Please set-up account for this Payment Method: %s', 11, 1, @PaymentMethodName)
			RETURN
		END

		--Payment
		INSERT INTO tblJournalDetails(JournalId, AccountId, Debit, Credit)
		SELECT @JournalId, pm.AccountId, 0, SUM(bpd.Amount)
		FROM tblBillsPaymentDetails bpd
			INNER JOIN tblPaymentMethods pm ON pm.Id = bpd.PaymentMethodId
		WHERE bpd.BillsPaymentId = @Id
		GROUP BY pm.AccountId
	END
	
	EXEC uspUpdateLedgers @JournalId, 1

	--Assign correct TransactionNo
	IF @IsNew = 1
	BEGIN
		UPDATE tblSystemSeries
		SET @TransactionNo = NextSeries,
			NextNumber = NextNumber + 1
		WHERE CompanyId = @CompanyId
			AND Code = @Code

		UPDATE tblJournals
		SET TransactionNo = @TransactionNo
		WHERE Id = @JournalId
	END
END
ELSE
BEGIN
	EXEC uspUpdateLedgers @JournalId, 0
	
	DELETE
	FROM tblJournalDetails
	WHERE JournalId = @JournalId

	--if Cash Receipt Voucher, unpost payment to invoice
	IF @JournalTypeId = 4
	BEGIN
		EXEC uspUpdateInvoiceAmounts @Id, @IsPost
	END

	--if Cash Disbursement Voucher, unpost payment to bills
	ELSE IF @JournalTypeId = 5
	BEGIN
		EXEC uspUpdateBillsAmounts @Id, @IsPost
	END
END
GO

