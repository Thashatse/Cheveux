USE [CHEVEUX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE PROCEDURE SP_AddProduct
	@productID nchar(10),
	@name varchar(50) = null,
	@productDescription varchar(max) = null,
	@Price money = null,
	@productType char(1) = null,
	@active char(1) = null,
	@productImage nvarchar(50) = null


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT PRODUCT(ProductID, Name, ProductDescription, Price, [ProductType(T/A/S)], Active,ProductImage)
	 VALUES (@productID,@name, @productDescription, @Price, 'T', 'Y', @productImage)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END

