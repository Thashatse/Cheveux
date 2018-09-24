SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a brand give the paramaters
-- =============================================
alter PROCEDURE SP_EditBrand 
	@BrandID nchar(10),
	@BrandName varchar(50), 
	@Type char(1)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[BRAND]
			SET [Name] = @BrandName,
				[Type(T/A)] = @Type
			WHERE BrandID = @BrandID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
