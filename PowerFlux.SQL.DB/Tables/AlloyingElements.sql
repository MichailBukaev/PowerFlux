create table [dbo].[AlloyingElements]
(
  [Id] int not null primary key identity(1, 1), 
  [Name] nvarchar(200) not null,
  Symbol nvarchar(10) not null,
  PartialTransformationToFerroalloyEquation nvarchar(max) default(0),
  PartialTransformationToKernelEquation nvarchar(max) default(0),
  PartialTransformationToGasEquation nvarchar(max) default(0),
  PartialTransformationToSlagEquation nvarchar(max) default(0),
)
