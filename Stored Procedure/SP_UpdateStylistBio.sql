USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateWeekendHours]    Script Date: 2018/08/06 12:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekend hours in the Bussiness Table
-- =============================================
alter  PROCEDURE [dbo].[SP_UpdateStylistBio]
	@bioUpdate varchar(MAX),
	@empID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE EMPLOYEE
			SET Bio = @bioUpdate
			Where EmployeeID = @empID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
