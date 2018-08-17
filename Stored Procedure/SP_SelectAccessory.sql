-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_SelectAccessory
	@productID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [ProductID]
      ,[PRODUCT].[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[ProductImage]
	  ,BRAND.[BrandID]
	  ,BRAND.Name
	
	 FROM [CHEVEUX].[dbo].[PRODUCT], ACCESSORY, BRAND, Supplier s
	WHERE ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND s.SupplierID = ACCESSORY.SupplierID And ProductID= @productID 
	order by [ProductType(T/A/S)]
END
GO

