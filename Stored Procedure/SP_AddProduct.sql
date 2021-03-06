USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 8/10/2018 8:14:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
alter  PROCEDURE [dbo].[SP_AddProduct]
	@productID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@Price money,
	@productType char(1),
	@active char(1),
	@productImage nvarchar(50) 


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT PRODUCT(ProductID, Name, ProductDescription, Price, [ProductType(T/A/S)], Active, ProductImage)
	 VALUES (@productID, @name, @productDescription, @Price,@productType ,@active , @productImage)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
