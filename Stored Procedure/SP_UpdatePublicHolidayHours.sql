SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the public holiday hours in the Bussiness Table
-- =============================================
create PROCEDURE SP_UpdatePublicHolidayHours
	@start time(7),
	@end time(7),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET PublicHolStart = @start,
				PublicHolEnd = @end
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
