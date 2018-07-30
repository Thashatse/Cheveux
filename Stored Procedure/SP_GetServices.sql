USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/07/24 12:54:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Name, ProductID
	FROM PRODUCT
	WHERE [ProductType(T/A/S)] = 'S';
END
