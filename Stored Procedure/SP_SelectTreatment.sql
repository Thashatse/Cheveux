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
alter PROCEDURE SP_SelectTreatment
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
	  
	  
	FROM [CHEVEUX].[dbo].[PRODUCT], [CHEVEUX].[dbo].[TREATMENT], BRAND, [CHEVEUX].[dbo].Supplier
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND [CHEVEUX].[dbo].[Supplier].[SupplierID] = [CHEVEUX].[dbo].[TREATMENT].[SupplierID] AND ProductID = @productID
	order by [ProductType(T/A/S)]
END
GO
