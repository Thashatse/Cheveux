USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SelectAccessory]    Script Date: 2018/09/27 12:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_SelectAccessory]
	@productID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [CHEVEUX].[dbo].[PRODUCT].[ProductID]
      ,[CHEVEUX].[dbo].[PRODUCT].[Name]
      ,[CHEVEUX].[dbo].[PRODUCT].[ProductDescription]
      ,[CHEVEUX].[dbo].[PRODUCT].[Price]
      ,[CHEVEUX].[dbo].[PRODUCT].[ProductType(T/A/S)]
	  ,[CHEVEUX].[dbo].ACCESSORY.Colour
	  ,BRAND.[BrandID]
	  ,BRAND.[Name] AS [BrandName]
	  ,[ACCESSORY].SupplierID 
	  ,Supplier.SupplierName
	  ,ACCESSORY.Qty
	
	 FROM [CHEVEUX].[dbo].[PRODUCT], [CHEVEUX].[dbo].[ACCESSORY], BRAND, [CHEVEUX].[dbo].[Supplier]
	WHERE [CHEVEUX].[dbo].[PRODUCT].ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND [CHEVEUX].[dbo].[Supplier].[SupplierID] = [CHEVEUX].[dbo].[ACCESSORY].SupplierID And [CHEVEUX].[dbo].[PRODUCT].ProductID = @productID
END


