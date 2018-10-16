alter PROCEDURE SP_UpdateTreatment
	@treatmentID nchar(10),
	@qty int,
	@treatmentType varchar(50),
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

			UPDATE TREATMENT
			SET Qty = @qty,
				TreatmentType = @treatmentType,
				BrandID = @BrandID,
				SupplierID = @SupplierID
			Where TreatmentID = @treatmentID 
				
				UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price,
				[ProductType(T/A/S)] =@productType 
			Where ProductID = @treatmentID
	   
		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END





