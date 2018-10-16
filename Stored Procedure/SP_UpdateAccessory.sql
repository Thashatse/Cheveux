alter PROCEDURE SP_UpdateAccessory
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

			UPDATE ACCESSORY
			SET    Colour=@colour,
				   Qty=@qty,
				   BrandID=@BrandID,
				SupplierID = @SupplierID
			where AccessoryID = @accessoryID
				   
			UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price,
				[ProductType(T/A/S)] =@productType 
			Where ProductID = @accessoryID

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END

	


