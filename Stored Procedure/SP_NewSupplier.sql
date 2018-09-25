SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a supplier give the paramaters
-- =============================================
alter PROCEDURE SP_NewSupplier 
	@SuppID nchar(10),
	@SuppName nchar(50),
	@ContactName nchar(50), 
	@ContactNo nchar(10), 
	@AddressL1 nchar(100), 
	@AddressL2 nchar(100), 
	@Suburb nchar(100), 
	@City nchar(100), 
	@ContactEmail varchar(50)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Supplier](SupplierID, SupplierName, ContactName, ContactNo, AddressLine1, AddressLine2, Suburb, City, ContactEmail)
			Values(@SuppID, @SuppName, @ContactName, @ContactNo, @AddressL1, @AddressL2, @Suburb, @City, @ContactEmail)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
