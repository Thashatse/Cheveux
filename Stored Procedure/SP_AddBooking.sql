USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 05 Jun 2018 8:40:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddBooking]
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@ServiceID nchar(10),
	@Date date,
	@Comment varchar(MAX)

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(SlotNo, CustomerID, StylistID, ServiceID, [Date], Comment)
			VALUES(@Slot, @CustomerID, @StylistID, @ServiceID, @Date, @Comment)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
