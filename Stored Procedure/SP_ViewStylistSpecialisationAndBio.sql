SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	given an emplyeeID (Stylist) it returns that employee specialization
-- =============================================
Alter PROCEDURE SP_ViewStylistSpecialisation 
	@EmployeeID nchar(30)
AS
BEGIN
	Select EmployeeID, ServiceID, [Name], ProductDescription, Price, [ProductType(T/A/S)], ProductImage, 
	(Select Bio from EMPLOYEE where EmployeeID = @EmployeeID) as StylistBio
	From STYLIST_SERVICE, PRODUCT
	Where ProductID = ServiceID
		And Active = 'Y'
		AND EmployeeID = @EmployeeID
END
GO
