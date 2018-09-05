USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all Product types in Product table of database
-- =============================================
alter PROCEDURE [dbo].[SP_GetProductTypes]
AS
BEGIN
	Select DISTINCT [ProductType(T/A/S)]
	From [CHEVEUX].[dbo].[PRODUCT]
END
