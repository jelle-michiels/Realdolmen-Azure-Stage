CREATE TABLE IMPORT_PROTIME_AFWEZIGHEDEN(
	EMPLID nvarchar(255) NULL,
	ABS_DATE nvarchar(255) NULL,
	DURATION nvarchar(255) NULL,
	ABSENCE_CODE nvarchar(255) NULL,
	IMPORT_DATE datetime NULL,
	EMPNR_PROTIME nvarchar(255) NULL
);

CREATE PROCEDURE SP_TRUNCATEIMPORTTABLE AS SELECT * FROM [dbo].[IMPORT_PROTIME_AFWEZIGHEDEN];

CREATE PROCEDURE SP_SYNCAFWEZIGHEID AS SELECT * FROM [dbo].[IMPORT_PROTIME_AFWEZIGHEDEN]