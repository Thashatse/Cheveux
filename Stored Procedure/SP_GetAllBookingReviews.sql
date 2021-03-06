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
/****** Object:  StoredProcedure [dbo].[SP_GetAllBookingReviews]    Script Date: 9/29/2018 2:28:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetAllBookingReviews]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,

		   (select UserImage
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistImage],

		   (select (stylist.FirstName+' '+stylist.LastName) as sName
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistName],

		   (select UserImage
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerName]


	FROM REVIEW r
	WHERE primaryBookingID is not null
	and r.[Date] !< DATEADD(mm,-2,GETDATE())
	order by r.[Date] desc
	
END
