SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekday hours in the Bussiness Table
-- =============================================
create PROCEDURE SP_UpdateWeekdayHours
	@start time(7),
	@end time(7),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET WeekdayStart = @start,
				WeekdayEnd = @end
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
