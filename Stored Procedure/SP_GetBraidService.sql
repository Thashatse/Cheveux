
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE SP_GetBraidService
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT s.Description AS styleDesc, w.Description AS widthDes, l.Description AS lengthDesc
	FROM SERVICE ser, PRODUCT p, STYLE s, WIDTH w, LENGTH l, BRAID_SERVICE bs
	WHERE @ServiceID = p.ProductID AND p.ProductID = ser.ServiceID AND [ProductType(T/A/S)] = 'S'
		AND ser.ServiceID = bs.ServiceID AND bs.StyleID = s.StyleID AND bs.WidthID = w.WidthID
		AND bs.LengthID = l.LengthID
END
GO
