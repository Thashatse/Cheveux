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
alter 
 PROCEDURE SP_GetOGBkngNoti
AS
BEGIN
	SELECT BookingID
	  ,[SlotNo], (select StartTime from TIMESLOT ts where ts.SlotNo = b.[SlotNo]) as StartTime
      ,[CustomerID], u.FirstName, u.LastName, u.UserName, u.PreferredCommunication, u.Email, u.ContactNo
      ,[StylistID], (Select FirstName from [user] where UserID = [StylistID]) as StylistName
      ,[Date]
      ,[NotificationReminder]
  FROM [BOOKING] b, [USER] u
  WHERE u.UserID = b.CustomerID
		AND [NotificationReminder] = 0
		AND [Date] between getdate() and DATEADD(DAY, 1, GETDATE())
END
GO
