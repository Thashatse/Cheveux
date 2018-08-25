USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTimeSlots]    Script Date: 2018/07/25 16:51:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[SP_GetTimeSlots] 

AS
BEGIN
	SELECT SlotNo, StartTime
	FROM TIMESLOT
	Order by StartTime
END
