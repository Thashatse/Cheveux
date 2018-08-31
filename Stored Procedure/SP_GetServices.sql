USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/08/31 12:08:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SELECT Name, ProductID, [Type(A/N/B)] AS ServiceType, Price
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'S';
END
