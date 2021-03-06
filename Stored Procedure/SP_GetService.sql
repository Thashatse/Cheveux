USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetService]    Script Date: 2018/09/03 13:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetService]
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Name], ProductDescription, Price, NoOfSlots, [Type(A/N/B)] AS ServiceType
	FROM SERVICE, PRODUCT
	WHERE @ServiceID = ProductID AND ProductID = ServiceID AND [ProductType(T/A/S)] = 'S'
END
