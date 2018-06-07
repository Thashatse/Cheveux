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
/****** Object:  StoredProcedure [dbo].[SP_GetMyNextCustomer]    Script Date: 6/7/2018 7:31:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 07.06.2018
-- Description:	Shows the stylist appointments of ONLY customers that have been checked-in (arrived for their appointment)
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetMyNextCustomer]
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
	AND	  ts.SlotNo=b.SlotNo
	AND   b.CustomerID=u.UserID
	AND   b.ServiceID=s.ServiceID
	AND   s.ServiceID=p.ProductID
	AND   b.[Date] = @Date
	AND   b.Arrived='Y'
	
    ORDER BY ts.StartTime,ts.EndTime

END
