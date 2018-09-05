USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTopCustomers]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
 
GO
alter PROCEDURE SP_GetTopCustomers
	@startDate datetime,
	@endDate datetime
AS
BEGIN
select COUNT(CustomerID) as BookCount, CustomerID,
		(Select [USER].FirstName + ' '+ [USER].LastName 
		from [USER] 
		where [USER].UserID =  BOOKING.CustomerID) as custFullName
from BOOKING
where BOOKING.BookingID = BOOKING.primaryBookingID
AND   BOOKING.Date BETWEEN @startDate and @endDate
group by CustomerID
order by BookCount desc
END
GO
