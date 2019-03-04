SET IDENTITY_INSERT tblAlphaNumericTaxCodes ON

INSERT INTO tblAlphaNumericTaxCodes(Id, Code, NatureOfIncomePayment, OldRate, NewRate, Remarks)
VALUES
	(1,		'WI010',	'PROFESSIONAL (LAWYERS, CPAS, ENGINEERS, ETC.) - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(2,		'WI011',	'PROFESSIONAL (LAWYERS, CPAS, ENGINEERS, ETC.) - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(3,		'WI020',	'PROFESSIONAL ENTERTAINERS SUCH AS, BUT NOT LIMITED TO ACTORS AND ACTRESSES, SINGERS, LYRICISTS, COMPOSERS, EMCEES - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(4,		'WI021',	'PROFESSIONAL ENTERTAINERS SUCH AS, BUT NOT LIMITED TO ACTORS AND ACTRESSES, SINGERS, LYRICISTS, COMPOSERS, EMCEES - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(5,		'WI030',	'PROFESSIONAL ATHLETES INCLUDING BASKETBALL PLAYERS, PELOTARIS AND JOCKEYS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(6,		'WI031',	'PROFESSIONAL ATHLETES INCLUDING BASKETBALL PLAYERS, PELOTARIS AND JOCKEYS - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(7,		'WI040',	'ALL DIRECTORS AND PRODUCERS INVOLVED IN MOVIES, STAGE, RADIO, TELEVISION AND MUSICAL PRODUCTIONS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(8,		'WI041',	'ALL DIRECTORS AND PRODUCERS INVOLVED IN MOVIES, STAGE, RADIO, TELEVISION AND MUSICAL PRODUCTIONS - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(9,		'WI050',	'MANAGEMENT AND TECHNICAL CONSULTANTS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(10,	'WI051',	'MANAGEMENT AND TECHNICAL CONSULTANTS - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(11,	'WI060',	'BUSINESS AND BOOKKEEPING AGENTS AND AGENCIES - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(12,	'WI061',	'BUSINESS AND BOOKKEEPING AGENTS AND AGENCIES -IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(13,	'WI070',	'INSURANCE AGENTS AND INSURANCE ADJUSTERS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(14,	'WI071',	'INSURANCE AGENTS AND INSURANCE ADJUSTERS - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(15,	'WI080',	'OTHER RECIPIENTS OF TALENT FEES - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(16,	'WI081',	'OTHER RECIPIENTS OF TALENT FEES - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(17,	'WI090',	'FEES OF DIRECTORS WHO ARE NOT EMPLOYEES OF THE COMPANY - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(18,	'WI091',	'FEES OF DIRECTORS WHO ARE NOT EMPLOYEES OF THE COMPANY - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(19,	'WI100',	'RENTALS: ON GROSS RENTAL OR LEASE FOR THE CONTINUED USE OR POSSESSION OF PERSONAL PROPERTY IN EXCESS OF TEN THOUSAND PESOS (P 10,000) ANNUALLY AND REAL PROPERTY USED IN BUSINESS WHICH THE PAYOR OR OBLIGOR HAS NOT TAKEN TITLE OR IS NOT TAKING TITLE, OR IN WHICH HAS NO EQUITY; POLES, SATELLITES , TRANSMISSION FACILITIES AND BILLBOARDS',		5,	5,	'No change'),
	(20,	'WI110',	'CINEMATOGRAPHIC FILM RENTALS AND OTHER PAYMENTS TO RESIDENT INDIVIDUALS AND CORPORATE CINEMATOGRAPHIC FILM OWNERS, LESSORS OR DISTRIBUTORS',		5,	5,	'No change'),
	(21,	'WI120',	'INCOME PAYMENTS TO CERTAIN CONTRACTORS',		2,	2,	'No change'),
	(22,	'WI130',	'INCOME DISTRIBUTION TO THE BENEFICIARIES OF ESTATES AND TRUSTS',		15,	15,	'No change'),
	(23,	'WI139',	'GROSS COMMISSIONS OR SERVICE FEES OF CUSTOMS, INSURANCE, STOCK, IMMIGRATION AND COMMERCIAL BROKERS, FEES OF AGENTS OF PROFESSIONAL ENTERTAINERS AND REAL ESTATE SERVICE PRACTITIONERS (RESPS), (I.E. REAL ESTATE CONSULTANTS, REAL ESTATE APPRAISERS AND REAL ESTATE BROKERS) - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		0,	5,	'New tax code'),
	(24,	'WI140',	'GROSS COMMISSIONS OR SERVICE FEES OF CUSTOMS, INSURANCE, STOCK, IMMIGRATION AND COMMERCIAL BROKERS, FEES OF AGENTS OF PROFESSIONAL ENTERTAINERS AND REAL ESTATE SERVICE PRACTITIONERS (RESPS), (I.E. REAL ESTATE CONSULTANTS, REAL ESTATE APPRAISERS AND REAL ESTATE BROKERS) - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		10,	10,	'No change'),
	(25,	'WI150',	'PROFESSIONAL FEES PAID TO MEDICAL PRACTITIONERS (INCLUDES DOCTORS OF MEDICINE, DOCTORS OF VETERINARY SCIENCE AND DENTISTS) BY HOSPITALS AND CLINICS OR PAID DIRECTLY BY HEALTH MAINTENANCE ORGANIZATIONS (HMOS) AND/OR SIMILAR ESTABLISHMENTS - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		15,	10,	'Change in wtax rate'),
	(26,	'WI151',	'PROFESSIONAL FEES PAID TO MEDICAL PRACTITIONERS (INCLUDES DOCTORS OF MEDICINE, DOCTORS OF VETERINARY SCIENCE AND DENTISTS) BY HOSPITALS AND CLINICS OR PAID DIRECTLY BY HEALTH MAINTENANCE ORGANIZATIONS (HMOS) AND/OR SIMILAR ESTABLISHMENTS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(27,	'WI152',	'PAYMENT BY THE GENERAL PROFESSIONAL PARTNERSHIPS (GPPS) TO ITS PARTNERS - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 720,000',		10,	10,	'No change'),
	(28,	'WI153',	'PAYMENT BY THE GENERAL PROFESSIONAL PARTNERSHIPS (GPPS) TO ITS PARTNERS - IF GROSS INCOME EXCEEDS P 720,000',		15,	15,	'No change'),
	(29,	'WI156',	'INCOME PAYMENTS MADE BY CREDIT CARD COMPANIES',		0.5,	0.5,	'No change'),
	(30,	'WI157',	'INCOME PAYMENTS MADE BY THE GOVERNMENT AND GOVERNMENT-OWNED AND CONTROLLED CORPORATIONS (GOCCS) TO ITS LOCAL/RESIDENT SUPPLIERS OF SERVICES OTHER THAN THOSE COVERED BY OTHER RATES OF WITHHOLDING TAX',		2,	2,	'No change'),
	(31,	'WI158',	'INCOME PAYMENT MADE BY TOP WITHHOLDING AGENTS TO THEIR LOCAL/RESIDENT SUPPLIER OF GOODS OTHER THAN THOSE COVERED BY OTHER RATES OF WITHHOLDING TAX',		1,	1,	'No change'),
	(32,	'WI159',	'ADDITIONAL INCOME PAYMENTS TO GOVERNMENT PERSONNEL FROM IMPORTERS, SHIPPING AND AIRLINE COMPANIES OR THEIR AGENTS FOR OVERTIME SERVICES',		15,	15,	'No change'),
	(33,	'WI160',	'INCOME PAYMENT MADE BY TOP WITHHOLDING AGENTS TO THEIR LOCAL/RESIDENT SUPPLIER OF SERVICES OTHER THAN THOSE COVERED BY OTHER RATES OF WITHHOLDING TAX',		2,	2,	'No change'),
	(34,	'WI515',	'COMMISSIONS, REBATES, DISCOUNTS AND OTHER SIMILAR CONSIDERATIONS PAID/GRANTED TO INDEPENDENT AND/OR EXCLUSIVE SALES REPRESENTATIVES AND MARKETING AGENTS AND SUB-AGENTS OF COMPANIES, INCLUDING MULTI-LEVEL MARKETING COMPANIES - IF GROSS INCOME FOR THE CURRENT YEAR DID NOT EXCEED P 3M',		10,	5,	'Change in wtax rate'),
	(35,	'WI516',	'COMMISSIONS, REBATES, DISCOUNTS AND OTHER SIMILAR CONSIDERATIONS PAID/GRANTED TO INDEPENDENT AND/OR EXCLUSIVE SALES REPRESENTATIVES AND MARKETING AGENTS AND SUB-AGENTS OF COMPANIES, INCLUDING MULTI-LEVEL MARKETING COMPANIES - IF GROSS INCOME IS MORE THAN P 3M OR VAT REGISTERED REGARDLESS OF AMOUNT',		0,	10,	'New tax code'),
	(36,	'WI530',	'GROSS PAYMENTS TO EMBALMERS BY FUNERAL PARLORS',		1,	1,	'No change'),
	(37,	'WI535',	'PAYMENTS MADE BY PRE-NEED COMPANIES TO FUNERAL PARLORS',		1,	1,	'No change'),
	(38,	'WI540',	'TOLLING FEES PAID TO REFINERIES',		5,	5,	'No change'),
	(39,	'WI610',	'INCOME PAYMENTS MADE TO SUPPLIERS OF AGRICULTURAL PRODUCTS IN EXCESS OF CUMULATIVE AMOUNT OF P 300,000 WITHIN THE SAME TAXABLE YEAR',		1,	1,	'No change'),
	(40,	'WI630',	'INCOME PAYMENTS ON PURCHASES OF MINERALS, MINERAL PRODUCTS AND QUARRY RESOURCES, SUCH AS BUT NOT LIMITED TO SILVER, GOLD, MARBLE, GRANITE, GRAVEL, SAND, BOULDERS AND OTHER MINERAL PRODUCTS EXCEPT PURCHASES BY BANGKO SENTRAL NG PILIPINAS',		1,	5,	'Change in wtax rate'),
	(41,	'WI632',	'INCOME PAYMENTS ON PURCHASES OF MINERALS, MINERAL PRODUCTS AND QUARRY RESOURCES BY BANGKO SENTRAL NG PILIPINAS (BSP) FROM GOLD MINERS/SUPPLIERS UNDER PD 1899, AS AMENDED BY RA NO. 7076',		0,	1,	'New tax code'),
	(42,	'WI640',	'INCOME PAYMENTS MADE BY THE GOVERNMENT AND GOVERNMENT-OWNED AND CONTROLLED CORPORATIONS (GOCCS) TO ITS LOCAL/RESIDENT SUPPLIERS OF GOODS OTHER THAN THOSE COVERED BY OTHER RATES OF WITHHOLDING TAX',		1,	1,	'No change'),
	(43,	'WI650',	'ON GROSS AMOUNT OF REFUND GIVEN BY MERALCO TO CUSTOMERS WITH ACTIVE CONTRACTS AS CLASSIFIED BY MERALCO',		25,	25,	'No change'),
	(44,	'WI651',	'ON GROSS AMOUNT OF REFUND GIVEN BY MERALCO TO CUSTOMERS WITH TERMINATED CONTRACTS AS CLASSIFIED BY MERALCO',		32,	32,	'No change'),
	(45,	'WI660',	'ON GROSS AMOUNT OF INTEREST ON THE REFUND OF METER DEPOSIT WHETHER PAID DIRECTLY TO THE CUSTOMERS OR APPLIED AGAINST CUSTOMERS BILLINGS OF RESIDENTIAL AND GENERAL SERVICE CUSTOMERS WHOSE MONTHLY ELECTRICITY CONSUMPTION EXCEEDS 200 KWH AS CLASSIFIED BY MERALCO',		0,	10,	'New tax code'),
	(46,	'WI661',	'ON GROSS AMOUNT OF INTEREST ON THE REFUND OF METER DEPOSIT WHETHER PAID DIRECTLY TO THE CUSTOMERS OR APPLIED AGAINST CUSTOMERS BILLINGS OF NON-RESIDENTIAL CUSTOMERS WHOSE MONTHLY ELECTRICITY CONSUMPTION EXCEEDS 200 KWH AS CLASSIFIED BY MERALCO',		0,	10,	'New tax code'),
	(47,	'WI662',	'ON GROSS AMOUNT OF INTEREST ON THE REFUND OF METER DEPOSIT WHETHER PAID DIRECTLY TO THE CUSTOMERS OR APPLIED AGAINST CUSTOMERS BILLINGS OF RESIDENTIAL AND GENERAL SERVICE CUSTOMERS WHOSE MONTHLY ELECTRICITY CONSUMPTION EXCEEDS 200 KWH AS CLASSIFIED BY OTHER ELECTRIC DISTRIBUTION UTILITIES (DU)',		0,	10,	'New tax code'),
	(48,	'WI663',	'ON GROSS AMOUNT OF INTEREST ON THE REFUND OF METER DEPOSIT WHETHER PAID DIRECTLY TO THE CUSTOMERS OR APPLIED AGAINST CUSTOMERS ILLINGS OF NON-RESIDENTIAL CUSTOMERS WHOSE MONTHLY ELECTRICITY CONSUMPTION EXCEEDS 200 KWH AS CLASSIFIED BY OTHER ELECTRIC DISTRIBUTION UTILITIES (DU)',		0,	20,	'New tax code'),
	(49,	'WI680',	'INCOME PAYMENTS MADE BY POLITICAL PARTIES AND CANDIDATES OF LOCAL AND NATIONAL ELECTIONS ON ALL THEIR PURCHASES OF GOODS AND SERVICES RELATED TO CAMPAIGN EXPENDITURES, AND INCOME PAYMENTS MADE BY INDIVIDUALS OR JURIDICAL PERSONS FOR THEIR PURCHASES OF GOODS AND SERVICES INTENDED TO BE GIVEN AS CAMPAIGN CONTRIBUTIONS TO POLITICAL PARTIES AND CANDIDATES',		0,	5,	'New tax code'),
	(50,	'WI710',	'INTEREST INCOME DERIVED FROM ANY OTHER DEBT INSTRUMENTS NOT WITHIN THE COVERAGE OF DEPOSIT SUBSTITUTES AND REVENUE REGULATIONS NO. 14-2012',		0,	20,	'New tax code'),
	(51,	'WI720',	'INCOME PAYMENTS ON LOCALLY PRODUCED RAW SUGAR',		0,	1,	'New tax code')
SET IDENTITY_INSERT tblAlphaNumericTaxCodes OFF
GO
