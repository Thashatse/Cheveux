-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- Description:	check for a matching exisiting Review with given ID
-- =============================================
CREATE PROCEDURE SP_CheckForReview
	@reviewID nchar(10)
AS
BEGIN
	SELECT ReviewID
	FROM REVIEW
	WHERE ReviewID = @reviewID

END
GO
