SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns services for a specific booking
-- =============================================
CREATE PROCEDURE SP_GetBookingServices
	@BookingID nchar(10)
AS
BEGIN
	SELECT [BookingID], [ServiceID]
	FROM [CHEVEUX].[dbo].[BookingService]
	where BookingID = @BookingID
END
GO
