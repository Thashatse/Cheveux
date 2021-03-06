USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 2018/09/26 13:35:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddBooking]
	@BookingID nchar(10),
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@Date date,
	@primaryBookingID nchar(10),
	@Comment varchar(max),
	@Arrived char(1)

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(BookingID, SlotNo, CustomerID, StylistID, [Date], Arrived, Available, Comment, NotificationReminder, primaryBookingID)
			VALUES(@BookingID, @Slot, @CustomerID, @StylistID, @Date, @Arrived, 'N', @Comment, 0, @primaryBookingID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
