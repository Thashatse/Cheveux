USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTotalCusts]    Script Date: 2018/08/24 16:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets the total number of registerd customers
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetTotalCusts]
AS
BEGIN
	SELECT COUNT(*)
	FROM [USER]
	where UserType = 'C'
			AND Active = 'T'
END
