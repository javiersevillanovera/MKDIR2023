USE [MKDIR]
GO
CREATE SCHEMA [MasterData] AUTHORIZATION [dbo]
GO

CREATE TABLE [MasterData].[GenderType]
(
 [GenderTypeID] int IDENTITY (1, 1) NOT NULL ,
 [name]         varchar(100) NOT NULL ,


 CONSTRAINT [PK_GenderType] PRIMARY KEY CLUSTERED ([GenderTypeID] ASC)
);
GO

CREATE TABLE [MasterData].[MaritalStatus]
(
 [MaritalStatusID] int IDENTITY (1, 1) NOT NULL ,
 [Name]            varchar(100) NOT NULL ,


 CONSTRAINT [PK_MaritalStatus] PRIMARY KEY CLUSTERED ([MaritalStatusID] ASC)
);
GO

CREATE TABLE [MasterData].[Currency]
(
 [CurrencyID] int IDENTITY (1, 1) NOT NULL ,
 [Code]       varchar(50) NOT NULL ,
 [Name]       varchar(100) NOT NULL ,
 [IsActive]   bit NOT NULL ,


 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC)
);
GO

CREATE TABLE [MasterData].[Country]
(
 [CountryID]  int IDENTITY (1, 1) NOT NULL ,
 [Name]       varchar(50) NOT NULL ,
 [CurrencyID] int NOT NULL ,
 [IsActive]   bit NOT NULL ,


 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC),
 CONSTRAINT [FK_Country_Currency] FOREIGN KEY ([CurrencyID])  REFERENCES [MasterData].[Currency]([CurrencyID])
);
GO

CREATE TABLE [MasterData].[Department]
(
 [DepartmentID] int IDENTITY (1, 1) NOT NULL ,
 [Code]         varchar(50) NOT NULL ,
 [Name]         varchar(100) NOT NULL ,
 [CountryID]    int NOT NULL ,
 [IsActive]     bit NOT NULL ,


 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
 CONSTRAINT [FK_Department_Country] FOREIGN KEY ([CountryID])  REFERENCES [MasterData].[Country]([CountryID])
);
GO

CREATE NONCLUSTERED INDEX [fkIdx_CurrencyID] ON [MasterData].[Country] 
 (
  [CurrencyID] ASC
 )

GO


CREATE NONCLUSTERED INDEX [fkIdx_CountryID] ON [MasterData].[Department] 
 (
  [CountryID] ASC
 )

GO

CREATE TABLE [MasterData].[City]
(
 [CityID]       int IDENTITY (1, 1) NOT NULL ,
 [Code]         varchar(50) NOT NULL ,
 [Name]         varchar(100) NOT NULL ,
 [DepartmentID] int NOT NULL ,
 [IsActive]     bit NOT NULL ,


 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC),
 CONSTRAINT [FK_City_Department] FOREIGN KEY ([DepartmentID])  REFERENCES [MasterData].[Department]([DepartmentID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_DepartmentID] ON [MasterData].[City] 
 (
  [DepartmentID] ASC
 )

GO


CREATE TABLE [MasterData].[Operator]
(
 [OperatorID] int IDENTITY (1, 1) NOT NULL ,
 [Code]       varchar(50) NOT NULL ,
 [Name]       varchar(100) NOT NULL ,
 [CountryID]  int NOT NULL ,
 [IsActive]   bit NOT NULL ,


 CONSTRAINT [PK_OperatorID] PRIMARY KEY CLUSTERED ([OperatorID] ASC),
 CONSTRAINT [FK_Operator_Country] FOREIGN KEY ([CountryID])  REFERENCES [MasterData].[Country]([CountryID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_CountryID] ON [MasterData].[Operator] 
 (
  [CountryID] ASC
 )

GO

CREATE TABLE [MasterData].[SequenceControl]
(
 [SequenceControlID] int IDENTITY (1, 1) NOT NULL ,
 [OperatorID]        int NOT NULL ,
 [ObjectName]        varchar(50) NOT NULL ,
 [Accuracy]          smallint NOT NULL ,
 [NextValue]         int NOT NULL ,


 CONSTRAINT [PK_SequenceControl] PRIMARY KEY CLUSTERED ([SequenceControlID] ASC),
 CONSTRAINT [FK_SequenceControl_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[SequenceControl] 
 (
  [OperatorID] ASC
 )

GO

CREATE TABLE [MasterData].[SuplierCategory]
(
 [SuplierCategoryID] int IDENTITY (1, 1) NOT NULL ,
 [Name]              varchar(100) NOT NULL ,
 [OperatorID]        int NOT NULL ,


 CONSTRAINT [PK_SuplierCategory] PRIMARY KEY CLUSTERED ([SuplierCategoryID] ASC),
 CONSTRAINT [FK_SuplierCategory_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[SuplierCategory] 
 (
  [OperatorID] ASC
 )

GO


CREATE TABLE [MasterData].[IdentificationType]
(
 [IdentificationTypeID] int IDENTITY (1, 1) NOT NULL ,
 [Code]                 varchar(500) NOT NULL ,
 [Name]                 varchar(100) NOT NULL ,
 [OperatorID]           int NOT NULL ,
 [IsActive]             bit NOT NULL ,


 CONSTRAINT [PK_IdentificationType] PRIMARY KEY CLUSTERED ([IdentificationTypeID] ASC),
 CONSTRAINT [FK_IdentificationType_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[IdentificationType] 
 (
  [OperatorID] ASC
 )

GO

CREATE TABLE [MasterData].[PersonType]
(
 [PersonTypeID] int IDENTITY (1, 1) NOT NULL ,
 [Name]         varchar(100) NOT NULL ,
 [OperatorID]   int NOT NULL ,
 [IsActive]     bit NOT NULL ,


 CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED ([PersonTypeID] ASC),
 CONSTRAINT [FK_PersonType_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[PersonType] 
 (
  [OperatorID] ASC
 )

GO

CREATE TABLE [MasterData].[DocumentType]
(
 [DocumentTypeID] int IDENTITY (1, 1) NOT NULL ,
 [Name]           varchar(50) NOT NULL ,
 [OperatorID]     int NOT NULL ,


 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC),
 CONSTRAINT [FK_DocumentType_OPerator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[DocumentType] 
 (
  [OperatorID] ASC
 )

GO

CREATE TABLE [MasterData].[BillingStatus]
(
 [BillingStatusID] int IDENTITY (1, 1) NOT NULL ,
 [DocumentTypeID]  int NOT NULL ,
 [Name]            varchar(100) NOT NULL ,


 CONSTRAINT [PK_BillingStatus] PRIMARY KEY CLUSTERED ([BillingStatusID] ASC),
 CONSTRAINT [FK_BillingStatus_DocumentType] FOREIGN KEY ([DocumentTypeID])  REFERENCES [MasterData].[DocumentType]([DocumentTypeID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_DocumentTypeID] ON [MasterData].[BillingStatus] 
 (
  [DocumentTypeID] ASC
 )

GO


CREATE TABLE [MasterData].[DeliveryStatus]
(
 [DeliveryStatusID] int IDENTITY (1, 1) NOT NULL ,
 [DocumentTypeID]   int NOT NULL ,
 [Name]             varchar(100) NOT NULL ,


 CONSTRAINT [PK_DeliveryStatus] PRIMARY KEY CLUSTERED ([DeliveryStatusID] ASC),
 CONSTRAINT [FK_DeliveryStatus_DocumentType] FOREIGN KEY ([DocumentTypeID])  REFERENCES [MasterData].[DocumentType]([DocumentTypeID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_DocumentTypeID] ON [MasterData].[DeliveryStatus] 
 (
  [DocumentTypeID] ASC
 )

GO


CREATE TABLE [MasterData].[DocumentStatus]
(
 [DocumentStatusID] int IDENTITY (1, 1) NOT NULL ,
 [DocumentTypeID]   int NOT NULL ,
 [Name]             varchar(100) NOT NULL ,


 CONSTRAINT [PK_DocumentStatus] PRIMARY KEY CLUSTERED ([DocumentStatusID] ASC),
 CONSTRAINT [FK_DocumentStatus_DocumentType] FOREIGN KEY ([DocumentTypeID])  REFERENCES [MasterData].[DocumentType]([DocumentTypeID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_DocumentTypeID] ON [MasterData].[DocumentStatus] 
 (
  [DocumentTypeID] ASC
 )

GO

CREATE TABLE [MasterData].[PaymentStatus]
(
 [PaymentStatusID] int IDENTITY (1, 1) NOT NULL ,
 [DocumentTypeID]  int NOT NULL ,
 [Name]            varchar(100) NOT NULL ,


 CONSTRAINT [PK_PaymentStatus] PRIMARY KEY CLUSTERED ([PaymentStatusID] ASC),
 CONSTRAINT [FK_PaymentStatus_DocumentType] FOREIGN KEY ([DocumentTypeID])  REFERENCES [MasterData].[DocumentType]([DocumentTypeID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_DocumentTypeID] ON [MasterData].[PaymentStatus] 
 (
  [DocumentTypeID] ASC
 )

GO


CREATE TABLE [MasterData].[Company]
(
 [CompanyID]            int IDENTITY (1, 1) NOT NULL ,
 [OperatorID]           int NOT NULL ,
 [Code]                 varchar(60) NOT NULL ,
 [PersonTypeID]         int NOT NULL ,
 [IdentificationTypeID] int NOT NULL ,
 [FiscalID]             varchar(50) NOT NULL ,
 [SecoundFiscalID]      varchar(50) NOT NULL ,
 [Name]                 varchar(100) NOT NULL ,
 [Address]              varchar(100) NOT NULL ,
 [Phone1]               varchar(50) NOT NULL ,
 [Phone2]               varchar(50) NOT NULL ,
 [Email]                varchar(50) NOT NULL ,
 [IsActive]             bit NOT NULL ,


 CONSTRAINT [PK_CompanyID] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
 CONSTRAINT [FK_Company_IdentificationType] FOREIGN KEY ([IdentificationTypeID])  REFERENCES [MasterData].[IdentificationType]([IdentificationTypeID]),
 CONSTRAINT [FK_Company_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID]),
 CONSTRAINT [FK_Company_PersonType] FOREIGN KEY ([PersonTypeID])  REFERENCES [MasterData].[PersonType]([PersonTypeID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_IdentificationTypeID] ON [MasterData].[Company] 
 (
  [IdentificationTypeID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [MasterData].[Company] 
 (
  [OperatorID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_PersonTypeID] ON [MasterData].[Company] 
 (
  [PersonTypeID] ASC
 )

GO


CREATE TABLE [MasterData].[Store]
(
 [StoreID]      int IDENTITY (1, 1) NOT NULL ,
 [CompanyID]    int NOT NULL ,
 [Code]         varchar(50) NOT NULL ,
 [Name]         varchar(100) NOT NULL ,
 [CountryID]    int NOT NULL ,
 [DepartmentID] int NOT NULL ,
 [CityID]       int NOT NULL ,
 [Address]      varchar(50) NOT NULL ,
 [Phone1]       varchar(50) NOT NULL ,
 [Phone2]       varchar(50) NOT NULL ,
 [Email]        varchar(50) NOT NULL ,
 [Logo]         varchar(500) NOT NULL ,
 [TimeZone]     varchar(50) NOT NULL ,
 [IsActive]     bit NOT NULL ,


 CONSTRAINT [PK_StoreID] PRIMARY KEY CLUSTERED ([StoreID] ASC),
 CONSTRAINT [FK_Store_City] FOREIGN KEY ([CityID])  REFERENCES [MasterData].[City]([CityID]),
 CONSTRAINT [FK_Store_Company] FOREIGN KEY ([CompanyID])  REFERENCES [MasterData].[Company]([CompanyID]),
 CONSTRAINT [FK_Store_Country] FOREIGN KEY ([CountryID])  REFERENCES [MasterData].[Country]([CountryID]),
 CONSTRAINT [FK_Store_Department] FOREIGN KEY ([DepartmentID])  REFERENCES [MasterData].[Department]([DepartmentID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_CityID] ON [MasterData].[Store] 
 (
  [CityID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_CompanyID] ON [MasterData].[Store] 
 (
  [CompanyID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_CoountryID] ON [MasterData].[Store] 
 (
  [CountryID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_DepartmentID] ON [MasterData].[Store] 
 (
  [DepartmentID] ASC
 )

GO

