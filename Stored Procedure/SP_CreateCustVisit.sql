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
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 6/4/2018 6:54:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Add Customer Visit
-- =============================================
CREATE PROCEDURE SP_CreateCustVisit
	@CustomerID nchar(30),
	@BookingID nchar = null,
	@Description varchar(50) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO CUST_VISIT (CustomerID,[Date],BookingID,[Description])
			VALUES (@CustomerID,
					(SELECT b.[Date] from BOOKING b where b.CustomerID=@CustomerID and b.BookingID=@BookingID),
					@BookingID,
					@Description)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
