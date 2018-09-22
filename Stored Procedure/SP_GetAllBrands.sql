-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns all brands in the database
-- =============================================
CREATE PROCEDURE SP_GetAllBrands
AS
BEGIN
	SELECT [BrandID]
      ,[Name]
      ,[Type(T/A)]
  FROM [CHEVEUX].[dbo].[BRAND]
END
GO
