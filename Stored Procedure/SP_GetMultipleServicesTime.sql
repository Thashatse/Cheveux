/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2002)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMultipleServicesTime]    Script Date: 8/26/2018 5:17:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gets the start time and the end time for the bookings 
--              which have multiple services (service > 1 ) 
-- =============================================
alter PROCEDURE SP_GetMultipleServicesTime
	@primaryBookingID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	
		(SELECT  t.StartTime 
		FROM BOOKING b, TIMESLOT t
		WHERE b.BookingID=@primaryBookingID
				AND b.primaryBookingID=@primaryBookingID
				AND b.SlotNo=t.SlotNo)AS[StartTime],


		(SELECT MAX(t.EndTime) 
		FROM BOOKING b, TIMESLOT t
		WHERE b.primaryBookingID=@primaryBookingID
			AND b.BookingID <> @primaryBookingID
			AND b.SlotNo = t.SlotNo)AS[EndTime]

END
