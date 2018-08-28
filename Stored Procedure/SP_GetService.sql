
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_GetService
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Name], ProductDescription, Price, NoOfSlots
	FROM SERVICE, PRODUCT
	WHERE @ServiceID = ProductID AND ProductID = ServiceID AND [ProductType(T/A/S)] = 'S'
END
GO
