-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
alter PROCEDURE SP_UpdateBookingReview
	@reviewID nchar(10),
	@bookingID nchar(10),
	@date datetime,
	@time time(7),
	@rating float = null,
	@comment varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			UPDATE REVIEW
			SET [Date] = @date,
				[Time]= @time,
				Rating = @rating,
				Comment = @comment
			WHERE ReviewID=@reviewID
			AND primaryBookingID=@bookingID
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
