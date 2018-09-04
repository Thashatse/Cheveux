SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Product with given ID
-- =============================================
alter  PROCEDURE SP_CheckForProduct 
	@ProductID nchar(10)
AS
BEGIN
	SELECT ProductID 
	FROM PRODUCT
	WHERE ProductID = @ProductID
END
GO
