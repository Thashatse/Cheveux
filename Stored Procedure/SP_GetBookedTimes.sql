USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[SP_GetBookedTimes] 
	@UserID nchar(30),
	@Date datetime
AS
BEGIN
	SELECT SlotNo
	FROM BOOKING
	WHERE StylistID = @UserID AND [Date] = @Date AND Available = 'N'
END
