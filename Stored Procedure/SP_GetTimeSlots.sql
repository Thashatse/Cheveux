USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTimeSlots]    Script Date: 2018/07/25 16:51:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetTimeSlots] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT SlotNo, StartTime
	FROM TIMESLOT

END
