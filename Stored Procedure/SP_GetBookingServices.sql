SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns services for a specific booking
-- =============================================
alter  PROCEDURE SP_GetBookingServices
	@BookingID nchar(10)
AS
BEGIN
	SELECT bs.[BookingID], bs.[ServiceID], p.[Name], p.Price, p.ProductDescription
	FROM BookingService bs, PRODUCT p
	where BookingID = @BookingID
		AND bs.[ServiceID] = p.ProductID
END
GO
