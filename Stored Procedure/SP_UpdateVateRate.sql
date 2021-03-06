USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateVateRate]    Script Date: 2018/06/21 12:36:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate Rate in the Bussiness Table
-- =============================================
alter PROCEDURE [dbo].[SP_UpdateVateRate] 
	@VatRat int,
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET [Vat%] = @VatRat
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
