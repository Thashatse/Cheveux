USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddTreatment]    Script Date: 2018/08/29 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Inserting of a product
ALTER PROCEDURE [dbo].[SP_AddTreatment]
	@treatmentID nchar(10),
	@qty int,
	@treatmentType varchar(50),
	@BrandID nchar(10) ,
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@active char(1)

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT TREATMENT(TreatmentID,Qty,TreatmentType,BrandID)
	 VALUES(@treatmentID, @qty, @treatmentType, @BrandID)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT PRODUCT(ProductID,Name,ProductDescription, Price,[ProductType(T/A/S)], Active)
	 VALUES(@treatmentID, @Name,@ProductDescription, @Price,@productType, 'Y' )
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
END 
