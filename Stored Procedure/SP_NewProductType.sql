SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a Product Type give the paramaters
-- =============================================
alter PROCEDURE SP_NewProductType
	@typeID nchar(10),
	@typeName varchar(50), 
	@ProdOrSer char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[ProductType](TypeID, [Name], [Product/Service])
			Values(@typeID, @typeName, @ProdOrSer)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
