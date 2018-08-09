

CREATE PROCEDURE SP_Products

@productID nchar(10)

AS
BEGIN
	
SELECT *
FROM PRODUCT
 Where ProductID = 'Pr1101'



END
GO
