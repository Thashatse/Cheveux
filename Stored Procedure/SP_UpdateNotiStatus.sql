USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sets the Notification Reminder colum of the booking table for a spacific booking
-- =============================================
alter PROCEDURE SP_UpdateNotiStatus
	@BookingID nchar(10),
	@NotiStatus bit
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			UPDATE BOOKING
			SET	   NotificationReminder = @NotiStatus
			WHERE BookingID = @BookingID
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH 
END
