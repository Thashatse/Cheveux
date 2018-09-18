-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the details of a supplier given the ID
-- =============================================
ALTER PROCEDURE getSupplierDetails
	@SuppID nchar(10)
AS
BEGIN
	SELECT [SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail]
  FROM [CHEVEUX].[dbo].[Supplier]
  WHERE [SupplierID] = @SuppID
END
GO
