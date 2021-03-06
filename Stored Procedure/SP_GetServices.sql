USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/09/03 13:35:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SELECT Name, ProductID, [Type(A/N/B)] AS ServiceType, Price, ProductDescription, Active
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'S';
END
