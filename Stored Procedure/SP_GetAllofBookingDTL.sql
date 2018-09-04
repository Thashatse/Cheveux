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
/****** Object:  StoredProcedure [dbo].[SP_GetAllofBookingDTL]    Script Date: 6/4/2018 11:36:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Gets all details of booking
-- =============================================
alter PROCEDURE SP_GetAllofBookingDTL
	@BookingID nchar(10),
	@CustomerID nchar(30) null
AS
BEGIN
	Select  	cv.BookingID,
			cv.CustomerID,CONCAT(U.FirstName,' ',U.LastName)AS[CustomerName],
			B.[Date], TS.StartTime, TS.EndTime, b.Comment 
			     
	From 		BOOKING B, TIMESLOT TS, [User] U, CUST_VISIT cv

	Where 		cv.BookingID = @BookingID 
	AND 		cv.CustomerID=@CustomerID
	AND			B.BookingID=cv.BookingID
	AND			B.CustomerID=cv.CustomerID
	AND 		B.SlotNo = TS.SlotNo 
	AND 		B.StylistID = U.UserID 
END
