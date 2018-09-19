SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Order with given ID
-- =============================================
create  PROCEDURE SP_CheckForOrder
	@OrderID nchar(10)
AS
BEGIN
	SELECT OrderID 
	FROM [Order]
	WHERE OrderID = @OrderID
END
GO