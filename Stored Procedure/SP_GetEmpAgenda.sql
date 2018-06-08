USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 5/28/2018 6:55:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 27.05.2018
-- Description:	Gets a specific employees agenda for the day.
-- =============================================
CREATE PROCEDURE SP_GetEmpAgenda
	@EmployeeID nchar(30),
	@Date datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  	b.BookingID,
			u.[UserID],
			ts.StartTime, ts.EndTime
			, u.[FirstName]AS[CustomerFName],
			
			(SELECT u.FirstName
			 FROM [User] u
			 WHERE u.UserID=@EmployeeID) AS [EmpFName],

			p.[Name]AS[ServiceName], b.Arrived, b.[Date]

	FROM BOOKING b, TIMESLOT ts, [User] u,[SERVICE] s, PRODUCT p

	WHERE b.StylistID=@EmployeeID
	AND ts.SlotNo=b.SlotNo
	AND b.CustomerID=u.UserID
	AND b.ServiceID=s.ServiceID
	AND s.ServiceID=p.ProductID
	AND b.[Date] = @Date
	/*AND (b.Arrived='N' OR b.Arrived IS NULL)*/
	
	ORDER BY ts.StartTime,ts.EndTime
END
