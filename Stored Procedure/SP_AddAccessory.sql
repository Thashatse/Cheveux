USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 8/14/2018 2:23:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE PROCEDURE SP_AddAccessory
	@accessoryID nchar(10),
	@colour varchar(50),
	@qty int,
	@BrandID nchar(10) 
 


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT ACCESSORY(AccessoryID, Colour, Qty, BrandID)
	 VALUES(@accessoryID, @colour, @qty, @BrandID)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
