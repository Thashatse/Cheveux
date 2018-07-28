USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 2018/07/24 13:10:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_AddBooking]
	@BookingID nchar(10),
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@ServiceID nchar(10),
	@Date date

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(BookingID, SlotNo, CustomerID, StylistID, ServiceID, [Date], Arrived, Available)
			VALUES(@BookingID, @Slot, @CustomerID, @StylistID, @ServiceID, @Date, 'N', 'N')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
