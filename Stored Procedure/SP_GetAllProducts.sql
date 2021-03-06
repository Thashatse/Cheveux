USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProducts]    Script Date: 2018/07/03 12:51:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
-- =============================================
-- Description:	Gets All Products in the Product Table
-- =============================================
alter PROCEDURE [dbo].[SP_GetAllProducts] 
AS
BEGIN
	SELECT *
	FROM [CHEVEUX].[dbo].[PRODUCT]
	order by [ProductType(T/A/S)]
END
