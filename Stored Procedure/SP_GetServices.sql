USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 04 Jun 2018 10:22:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Name
	FROM PRODUCT
	WHERE ProductType = 'S';
END
