SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a brand give the paramaters
-- =============================================
alter PROCEDURE SP_NewBrand 
	@BrandID nchar(10),
	@BrandName varchar(50), 
	@Type char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[BRAND](BrandID, [Name], [Type(T/A)])
			Values(@BrandID, @BrandName, @Type)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
