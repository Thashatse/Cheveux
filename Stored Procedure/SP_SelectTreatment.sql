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
CREATE PROCEDURE SP_SelectTreatment
	@productID nchar(10)
	AS
BEGIN
	  SELECT [ProductID]
      ,Product.Name
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
	  ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.Name
	  
	  
	FROM [CHEVEUX].[dbo].[PRODUCT], TREATMENT, BRAND, Supplier s
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND s.SupplierID = TREATMENT.SupplierID AND ProductID = @productID
	order by [ProductType(T/A/S)]
END
GO
