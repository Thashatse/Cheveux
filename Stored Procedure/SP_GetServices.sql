USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/07/26 15:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SELECT Name, ProductID, [Type(A/N/B)] AS ServiceType
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'S';
END
