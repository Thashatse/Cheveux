CREATE PROCEDURE SP_TopCustomr 
	@CustomerID nchar(30),
	@startDate dateTime,
	@endDate dateTime

	AS
BEGIN
	
SELECT DISTINCT [CustomerID]
	FROM CUST_VISIT
	WHERE Date=@startDate AND Date=@endDate

END
GO
