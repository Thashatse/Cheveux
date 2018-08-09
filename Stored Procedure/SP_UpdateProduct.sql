USE [CHEVEUX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Update/editing of products
-- =============================================
CREATE  PROCEDURE SP_UpdateProducts
	@productID nchar(10),
	@name varchar(max) = null,
	@productDescription varchar(max) = null,
	@price money = null,
	@productType char(1) = null,
	@active char(1) = null,
	@productImage nvarchar(50)= null


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

	UPDATE PRODUCT
	SET Name = @name,
	    ProductDescription = @productDescription,
		Price = @price,
		[ProductType(T/A/S)] = 'T',
		Active = @active,
		ProductImage = @productImage

	WHERE ProductID = @productID
	AND (select [ProductType(T/A/S)]
	      from PRODUCT
		  Where ProductDescription = @productDescription
		  AND   Price = @price
		  AND   Name = @name) = 'A'

   COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 

END

