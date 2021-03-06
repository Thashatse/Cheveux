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
/****** Object:  StoredProcedure [dbo].[SP_ViewCustVisit]    Script Date: 6/4/2018 6:55:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	View Customer Visit Record
-- =============================================
alter PROCEDURE SP_ViewCustVisit
	@CustomerID nchar(30),
	@BookingID nchar(10)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM CUST_VISIT c 
	WHERE c.CustomerID=@CustomerID AND c.BookingID=@BookingID
END
