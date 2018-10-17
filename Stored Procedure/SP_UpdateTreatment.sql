alter PROCEDURE SP_UpdateTreatment
	@treatmentID nchar(10), 
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@SupplierID nchar(10)

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE TREATMENT
			SET SupplierID = @SupplierID
			Where TreatmentID = @treatmentID 
				
			UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price
			Where ProductID = @treatmentID
	   
		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END





