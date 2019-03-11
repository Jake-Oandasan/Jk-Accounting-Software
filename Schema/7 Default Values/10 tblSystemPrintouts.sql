DECLARE @tmp TABLE(FormCaption VARCHAR(100), Report VARCHAR(100), PrintoutFormName VARCHAR(100))

INSERT INTO @tmp(FormCaption, Report, PrintoutFormName)
VALUES
	('Journal Voucher', 'Journal Voucher', 'EJournalVoucherPrintoutForm'),
	('Sales Invoice', 'Sales Invoice', 'ESalesInvoicePrintoutForm'),
	('Bill', 'Bill', 'EBillPrintoutForm'),
	('Bills Payment', 'BIR Form 2307', 'EBIRForm2307PrintoutForm')

INSERT INTO tblSystemPrintouts(FormCaption, Report, PrintoutFormName)
SELECT t.FormCaption, t.Report, t.PrintoutFormName
FROM @tmp t
WHERE NOT EXISTS(
	SELECT *
	FROM tblSystemPrintouts sp
	WHERE sp.FormCaption = t.FormCaption
		AND sp.Report = t.Report
		AND sp.PrintoutFormName = t.PrintoutFormName
)
GO

