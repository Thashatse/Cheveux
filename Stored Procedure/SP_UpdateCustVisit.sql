/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustVisit]    Script Date: 6/4/2018 6:54:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Updates Existing Customer Visit.
-- =============================================
create PROCEDURE SP_UpdateCustVisit
	@CustomerID nchar(30),
	@BookingID nchar(10),
	@Description varchar(max)
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			UPDATE CUST_VISIT
			SET	   [Description] = Convert(varchar(50),@Description)
			WHERE CustomerID=@CustomerID
			AND	  BookingID=@BookingID

			UPDATE BOOKING
			SET	   Comment = @Description
			WHERE  primaryBookingID = @BookingID
			AND    CustomerID = @CustomerID

		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH 
END
