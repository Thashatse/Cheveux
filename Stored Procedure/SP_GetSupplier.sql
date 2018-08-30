
CREATE PROCEDURE SP_Get_Supplier
	@supplierID nchar(10)


AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT *
	FROM Supplier
	WHERE SupplierID = '001'
END
GO
