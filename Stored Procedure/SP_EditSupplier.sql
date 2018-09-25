SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a supplier give the paramaters
-- =============================================
alter PROCEDURE SP_EditSupplier 
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
			UPDATE [CHEVEUX].[dbo].[Supplier]
			SET SupplierName = @SuppName,
				ContactName = @ContactName,
				ContactNo = @ContactNo,
				AddressLine1 = @AddressL1, 
				AddressLine2 = @AddressL2, 
				Suburb = @Suburb, 
				City = @City,
				ContactEmail = @ContactEmail
			WHERE SupplierID = @SuppID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
