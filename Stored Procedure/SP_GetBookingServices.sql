USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServices]    Script Date: 2018/10/16 11:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns services for a specific booking
-- =============================================
ALTER  PROCEDURE [dbo].[SP_GetBookingServices]
	@BookingID nchar(10)
AS
BEGIN
	SELECT bs.[BookingID], bs.[ServiceID], p.[Name], p.Price, p.ProductDescription, p.[ProductType(T/A/S)]
	FROM BookingService bs, PRODUCT p
	where BookingID = @BookingID
		AND bs.[ServiceID] = p.ProductID
END
