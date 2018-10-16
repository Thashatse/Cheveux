alter PROCEDURE SP_UpdateAccessory
    @accessoryID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@SupplierID nchar(10)
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE ACCESSORY
			SET    SupplierID = @SupplierID
			where AccessoryID = @accessoryID
				   
			UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price
			Where ProductID = @accessoryID

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END

	


