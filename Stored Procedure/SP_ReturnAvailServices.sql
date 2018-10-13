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
-- =============================================
CREATE PROCEDURE SP_ReturnAvailServices
	@slots int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT  s.ServiceID, p.[Name],p.ProductDescription,p.Price,
			s.NoOfSlots,s.[Type(A/N/B)]
	FROM [SERVICE] s, PRODUCT p
	WHERE s.ServiceID=p.ProductID
	and p.Active ='Y'
	and s.NoOfSlots=@slots

END
GO
