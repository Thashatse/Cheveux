-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description: Get Out Going Booking Notifications: gets the details nessasry to send booking notifications for bookint taking place tommorow
-- =============================================
CREATE PROCEDURE SP_GetOGBkngNoti
AS
BEGIN
	SELECT BookingID
	  ,[SlotNo], (select StartTime from TIMESLOT ts where ts.SlotNo = b.[SlotNo])
      ,[CustomerID], u.FirstName, u.UserName, u.PreferredCommunication, u.Email, u.ContactNo
      ,[StylistID], (Select FirstName from [user] where UserID = [StylistID]) as StylistName
      ,[ServiceID], p.Name, p.Price
      ,[Date]
      ,[NotificationReminder]
  FROM [BOOKING] b, [USER] u, PRODUCT p
  WHERE u.UserID = b.CustomerID
		AND P.ProductID = b.ServiceID
		AND [NotificationReminder] = 0
		AND [Date] between getdate() and DATEADD(DAY, 2, GETDATE())
END
GO
