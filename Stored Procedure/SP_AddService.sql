USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddService]    Script Date: 2018/08/13 10:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_AddService] 
	@ProductID nchar(10),
	@Name varchar(MAX),
	@Description varchar(MAX),
	@Price money,
	@Slots int,
	@Type char(1),
	@LengthID nchar(10) = null,
	@StyleID nchar(10) = null,
	@WidthID nchar(10) = null

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO PRODUCT(ProductID, [Name], ProductDescription, Price)
			VALUES(@ProductID, @Name, @Description, @Price)
			INSERT INTO SERVICE(ServiceID, NoOfSlots, [Type(A/N/B)])
			VALUES(@ProductID, @Slots, @Type)
			INSERT INTO BRAID_SERVICE(ServiceID, StyleID, LengthID, WidthID)
			VALUES(@ProductID, @StyleID, @LengthID, @WidthID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
