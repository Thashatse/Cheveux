USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStyles]    Script Date: 2018/08/13 09:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter  PROCEDURE [dbo].[SP_GetStyles]

AS
BEGIN
	SELECT *
	FROM STYLE
END
