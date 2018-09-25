SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Supplier with given ID
-- =============================================
create  PROCEDURE SP_CheckForSupplier 
	@SuppID nchar(10)
AS
BEGIN
	SELECT SupplierID 
	FROM [Supplier]
	WHERE SupplierID = @SuppID
END
GO