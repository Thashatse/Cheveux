USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SelectTreatment]    Script Date: 2018/09/27 12:49:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_SelectTreatment]
	@productID nchar(10)
	AS
BEGIN
	  SELECT [ProductID]
      ,PRODUCT.[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
	  ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.[Name]	AS [BrandName]
	  ,[TREATMENT].[SupplierID]
	  ,Supplier.SupplierName
	  ,TREATMENT.Qty
	  ,TreatmentType
	  
	FROM [CHEVEUX].[dbo].[PRODUCT], [CHEVEUX].[dbo].[TREATMENT], BRAND, [CHEVEUX].[dbo].Supplier
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND [CHEVEUX].[dbo].[Supplier].[SupplierID] = [CHEVEUX].[dbo].[TREATMENT].[SupplierID] AND ProductID = @productID
	AND TREATMENT.SupplierID = Supplier.SupplierID
	order by [ProductType(T/A/S)]
END


