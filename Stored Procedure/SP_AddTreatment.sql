USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddAccessory]    Script Date: 8/14/2018 3:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE PROCEDURE SP_TREATMENT
	@treatmentID nchar(10),
	@qty int,
	@treatmentType varchar(50),
	@BrandID nchar(10) 
 


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
END