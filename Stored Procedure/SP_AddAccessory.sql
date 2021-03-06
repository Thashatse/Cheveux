USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddAccessory]    Script Date: 2018/08/29 12:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
ALTER PROCEDURE [dbo].[SP_AddAccessory]
	@accessoryID nchar(10),
	@colour varchar(50),
	@qty int,
	@BrandID nchar(10) ,
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@SupplierID nchar(10)
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT ACCESSORY(AccessoryID, Colour, Qty, BrandID, SupplierID)
	 VALUES(@accessoryID, @colour, @qty, @BrandID, @SupplierID)

     INSERT PRODUCT(ProductID,[Name], ProductDescription,Price, [ProductType(T/A/S)], Active)
	 VALUES(@accessoryID, @name, @productDescription,@price, @productType, 'Y' )
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH


END
