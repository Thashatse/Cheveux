SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the addres in the Bussiness Table
-- =============================================
create PROCEDURE SP_UpdateAddress
	@addline1 varchar(max),
	@addline2 varchar(max),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET AddressLine1 = @addline1,
				AddressLine2 = @addline2
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
