/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ProductID]
      ,[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
  FROM [CHEVEUX].[dbo].[PRODUCT]