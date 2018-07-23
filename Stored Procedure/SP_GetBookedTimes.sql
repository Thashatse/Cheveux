USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookedTimes]    Script Date: 23 Jul 2018 6:06:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetBookedTimes] 
	@UserID nchar(30),
	@Date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SlotNo
	FROM BOOKING
	WHERE @UserID = StylistID AND @Date = [Date] AND Available = 'N'
END
