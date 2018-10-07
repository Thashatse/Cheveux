SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE SP_UpdateFeaturedContact
	@FeatureID nchar(10),
	@ItemID varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Home_Page]
			SET ItemID = @ItemID
			WHERE FeatureID = @FeatureID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO