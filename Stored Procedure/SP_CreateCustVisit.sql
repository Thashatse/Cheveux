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
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 8/26/2018 6:37:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Add Customer Visit
-- =============================================

alter PROCEDURE [dbo].[SP_CreateCustVisit]
	@CustomerID nchar(30),
	@Date datetime,
	@PrimaryBookingID nchar(10),
	@Description varchar(50)=null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO CUST_VISIT (CustomerID,[Date],BookingID,[Description])
			VALUES (@CustomerID,
					@Date,
					@PrimaryBookingID,
					@Description)		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
