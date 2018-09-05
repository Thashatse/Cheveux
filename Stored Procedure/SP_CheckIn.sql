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
/****** Object:  StoredProcedure [dbo].[SP_CheckIn]    Script Date: 6/3/2018 4:46:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	S.Maqabangqa
-- Create date: 03.06.2018
-- Description:	Updates booking arrived to "Y (YES)".
-- =============================================
alter PROCEDURE [dbo].[SP_CheckIn]
	@BookingID nchar(10)
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			
			UPDATE BOOKING
			SET Arrived='Y'
			WHERE BOOKING.BookingID=@BookingID
				or BOOKING.primaryBookingID = @BookingID
			AND BOOKING.Arrived='N'
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF(@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION
	END CATCH
END
