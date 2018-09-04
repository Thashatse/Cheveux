SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate reg no in the Bussiness Table
-- =============================================
alter PROCEDURE SP_UpdateVateRegNo 
	@VatRegno nchar(10),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET VatRegNo = @VatRegno
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
