USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[test]    Script Date: 2018/05/04 12:50:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE SP_CheckForUser 
	-- Add the parameters for the stored procedure here
	@ID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT COUNT(*) as 'Exists'
	From CUSTOMER
	Where CustomerID = @ID

    -- Insert statements for procedure here
	SELECT * from CUSTOMER
END