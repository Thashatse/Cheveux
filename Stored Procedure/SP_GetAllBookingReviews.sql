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
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
	WHERE primaryBookingID is not null
END
