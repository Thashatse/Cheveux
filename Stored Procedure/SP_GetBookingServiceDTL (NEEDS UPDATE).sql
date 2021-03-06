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
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServiceDTL]    Script Date: 6/4/2018 5:32:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Gets service details of customer booking
-- =============================================
CREATE PROCEDURE SP_GetBookingServiceDTL
	@BookingID nchar(10),
	@CustomerID nchar(30) = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT b.BookingID, p.[Name]AS[ServiceName] , p.ProductDescription AS [ServiceDescription]
	FROM BOOKING b, [SERVICE] s, PRODUCT p
	WHERE b.BookingID=@BookingID
	AND   b.CustomerID=@CustomerID
	AND   b.ServiceID=s.ServiceID
	AND   s.ServiceID = p.ProductID
END
