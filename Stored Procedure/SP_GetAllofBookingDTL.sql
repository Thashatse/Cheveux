-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Gets all details of booking
-- =============================================
CREATE PROCEDURE SP_GetAllofBookingDTL
	@BookingID nchar(10),
	@CustomerID nchar(30) null
AS
BEGIN
	Select  	BookingID,
			U.UserID,CONCAT(U.FirstName,' ',U.LastName)AS[CustomerName] ,
			P.[Name]AS[ServiceName], P.ProductDescription AS [ServiceDescription], P.Price,
			B.[Date], TS.StartTime, TS.EndTime   
			     
	From 		BOOKING B, TIMESLOT TS, [User] U, PRODUCT P

	Where 		BookingID = @BookingID 
	AND 		B.CustomerID=@CustomerID
	AND 		B.SlotNo = TS.SlotNo 
	AND 		B.StylistID = U.UserID 
	AND 		B.ServiceID = P.ProductID  
END
GO
