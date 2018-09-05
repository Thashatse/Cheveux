USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 2018/07/10 12:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 27.05.2018
-- Description:	Gets a specific employees bookings for the day.
-- =============================================
alter PROCEDURE [dbo].[SP_GetEmpAgenda]
	@EmployeeID nchar(30),
	@Date datetime = null,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  	b.BookingID,b.primaryBookingID AS [PrimaryID],
				u.[UserID],

				ts.StartTime, ts.EndTime

				,u.[FirstName]AS[CustomerFName],
			
				(SELECT u.UserID
				 FROM [User] u
				 WHERE u.UserID=@EmployeeID)  as [EmpID],

				 (SELECT u.FirstName
				 FROM [User] u
				 WHERE u.UserID=@EmployeeID) AS [EmpFName],

				 b.Arrived, b.[Date]
				, b.Comment
	FROM BOOKING b, TIMESLOT ts, [User] u

	WHERE b.StylistID=@EmployeeID
	AND ts.SlotNo=b.SlotNo
	AND b.CustomerID=u.UserID
	AND b.[Date] = @Date
	AND (Select PaymentType
		From Sale
		Where b.BookingID = SaleID) is null
	AND (b.Arrived='N' OR b.Arrived='Y')
	AND b.BookingID=b.primaryBookingID
	
	/*ORDER BY ts.StartTime,ts.EndTime*/
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC,
		  
		  (CASE 
		  WHEN @sortBy is null and @sortDir IS NULL 
		  THEN CONVERT(time(7),ts.StartTime)
		  END) ASC
END
