USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProducts]    Script Date: 8/10/2018 8:01:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Update/editing of products
-- =============================================
alter  PROCEDURE [dbo].[SP_UpdateProducts]
	@productID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@active char(1),
	@productImage nvarchar(50)


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

	UPDATE PRODUCT
	SET Name = @name,
	    ProductDescription = @productDescription,
		Price = @price,
		[ProductType(T/A/S)] =@productType ,
		Active = @active,
		ProductImage = @productImage

	WHERE ProductID = @productID
	



		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 

END

