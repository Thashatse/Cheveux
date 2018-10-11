SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a Product Type given the paramaters
-- =============================================
alter PROCEDURE SP_EditProductType 
	@typeID nchar(10),
	@typeName varchar(50), 
	@ProdOrSer char(1)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[ProductType]
			SET [Name] = @typeName,
				[Product/Service] = @ProdOrSer
			WHERE TypeID = @typeID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
