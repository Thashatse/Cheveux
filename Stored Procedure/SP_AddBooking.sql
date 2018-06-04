USE [CHEVEUX]
GO

/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 31 May 2018 7:20:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBooking]
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@ServiceID nchar(10),
	@Date date

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(SlotNo, CustomerID, StylistID, ServiceID, Date)
			VALUES(@Slot, @CustomerID, @StylistID, @ServiceID, @Date)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO

