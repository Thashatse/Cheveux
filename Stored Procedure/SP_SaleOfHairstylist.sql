
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Create date: <Create Date,,>
-- Description:	Sales for a hairstylist
-- =============================================
CREATE PROCEDURE SP_SaleOfHairstylist
	-- Add the parameters for the stored procedure here
	@StylistID nchar(30)
	
AS
BEGIN

Select * 
From SALE, BOOKING
Where SALE.BookingID = BOOKING.BookingID
And BOOKING.StylistID = @StylistID




END
GO
