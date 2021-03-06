USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddService]    Script Date: 2018/08/28 13:42:54 ******/
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
	@Type char(1)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO PRODUCT(ProductID, [Name], ProductDescription, Price, [ProductType(T/A/S)], Active)
			VALUES(@ProductID, @Name, @Description, @Price, 'S', 'Y')
			INSERT INTO SERVICE(ServiceID, NoOfSlots, [Type(A/N/B)])
			VALUES(@ProductID, @Slots, @Type)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
