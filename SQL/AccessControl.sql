USE [MKDIR]
GO
CREATE SCHEMA [AccessControl] AUTHORIZATION [dbo]
GO


CREATE TABLE [AccessControl].[BusinessAction]
(
 [BusinessActionID] int IDENTITY (1, 1) NOT NULL ,
 [Name]             varchar(100) NOT NULL ,
 [Icon]             varchar(100) NOT NULL ,
 [IsActive]         bit NOT NULL ,


 CONSTRAINT [PK_BusinessAction] PRIMARY KEY CLUSTERED ([BusinessActionID] ASC)
);
GO

CREATE TABLE [AccessControl].[BusinessModule]
(
 [BusinessModuleID] int IDENTITY (1, 1) NOT NULL ,
 [Sequence]         int NOT NULL ,
 [Name]             varchar(100) NOT NULL ,
 [Icon]             varchar(100) NOT NULL ,
 [IsOperator]       bit NOT NULL ,
 [IsActive]         bit NOT NULL ,


 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([BusinessModuleID] ASC)
);
GO


CREATE TABLE [AccessControl].[BusinessTransaction]
(
 [BusinessTransactionID] int IDENTITY (1, 1) NOT NULL ,
 [Sequence]              int NOT NULL ,
 [Name]                  varchar(100) NOT NULL ,
 [BusinessModuleID]      int NOT NULL ,
 [Icon]                  varchar(100) NOT NULL ,
 [URLPath]               varchar(100) NOT NULL ,
 [IsActive]              bit NOT NULL ,


 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([BusinessTransactionID] ASC),
 CONSTRAINT [FK_BusinessTransaction_BusinessModule] FOREIGN KEY ([BusinessModuleID])  REFERENCES [AccessControl].[BusinessModule]([BusinessModuleID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessModuleID] ON [AccessControl].[BusinessTransaction] 
 (
  [BusinessModuleID] ASC
 )

GO

CREATE TABLE [AccessControl].[BusinessUser]
(
 [BusinessUserID] int IDENTITY (1, 1) NOT NULL ,
 [Email]          varchar(50) NOT NULL ,
 [FirstName]      varchar(50) NOT NULL ,
 [SureName]       varchar(50) NOT NULL ,
 [IsOperator]     bit NOT NULL ,
 [IsActive]       bit NOT NULL ,


 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([BusinessUserID] ASC)
);
GO

CREATE TABLE [AccessControl].[BusinessUserPass]
(
 [BusinessUserID] int NOT NULL ,
 [Pass]           varchar(50) NOT NULL ,


 CONSTRAINT [PK_BusinessUserPass] PRIMARY KEY CLUSTERED ([BusinessUserID] ASC),
 CONSTRAINT [FK_BusinessUserPass_BusinessUser] FOREIGN KEY ([BusinessUserID])  REFERENCES [AccessControl].[BusinessUser]([BusinessUserID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessUserID] ON [AccessControl].[BusinessUserPass] 
 (
  [BusinessUserID] ASC
 )

GO

CREATE TABLE [AccessControl].[CompanyProfile]
(
 [CompanyProfileID] int IDENTITY (1, 1) NOT NULL ,
 [Name]             varchar(100) NOT NULL ,
 [CompanyID]        int NOT NULL ,
 [IsActive]         bit NOT NULL ,


 CONSTRAINT [PK_CompanyProfileID] PRIMARY KEY CLUSTERED ([CompanyProfileID] ASC),
 CONSTRAINT [FK_CompanyProfile_Company] FOREIGN KEY ([CompanyID])  REFERENCES [MasterData].[Company]([CompanyID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_CompanyID] ON [AccessControl].[CompanyProfile] 
 (
  [CompanyID] ASC
 )

GO

CREATE TABLE [AccessControl].[TransactionAction]
(
 [BusinessTransactionID] int NOT NULL ,
 [BusinessActionID]      int NOT NULL ,


 CONSTRAINT [PK_TransactionAction] PRIMARY KEY CLUSTERED ([BusinessTransactionID] ASC, [BusinessActionID] ASC),
 CONSTRAINT [FK_TransactionAction_BusinessAction] FOREIGN KEY ([BusinessActionID])  REFERENCES [AccessControl].[BusinessAction]([BusinessActionID]),
 CONSTRAINT [FK_TransactionAction_BusinessTransaction] FOREIGN KEY ([BusinessTransactionID])  REFERENCES [AccessControl].[BusinessTransaction]([BusinessTransactionID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessActionID] ON [AccessControl].[TransactionAction] 
 (
  [BusinessActionID] ASC
 )

GO

CREATE TABLE [AccessControl].[CompanyProfileTransactionAction]
(
 [CompanyProfileID]      int NOT NULL ,
 [BusinessTransactionID] int NOT NULL ,
 [BusinessActionID]      int NOT NULL ,


 CONSTRAINT [PK_CompanyProfileTransactionAction] PRIMARY KEY CLUSTERED ([CompanyProfileID] ASC, [BusinessTransactionID] ASC, [BusinessActionID] ASC),
 CONSTRAINT [FK_CompanyProfileTransactionAction_CompanyProfile] FOREIGN KEY ([CompanyProfileID])  REFERENCES [AccessControl].[CompanyProfile]([CompanyProfileID]),
 CONSTRAINT [FK_CompanyProfileTransactionAction_TransactionAction] FOREIGN KEY ([BusinessTransactionID], [BusinessActionID])  REFERENCES [AccessControl].[TransactionAction]([BusinessTransactionID], [BusinessActionID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessTransactionID_BusinessActionID] ON [AccessControl].[CompanyProfileTransactionAction] 
 (
  [BusinessTransactionID] ASC, 
  [BusinessActionID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_CompanyProfileID] ON [AccessControl].[CompanyProfileTransactionAction] 
 (
  [CompanyProfileID] ASC
 )

GO

CREATE TABLE [AccessControl].[OperatorProfile]
(
 [OperatorProfileID] int IDENTITY (1, 1) NOT NULL ,
 [Name]              varchar(100) NOT NULL ,
 [OperatorID]        int NOT NULL ,
 [IsActive]          bit NOT NULL ,


 CONSTRAINT [PK_OperatorProfileID] PRIMARY KEY CLUSTERED ([OperatorProfileID] ASC),
 CONSTRAINT [FK_OperatorProfile_Operator] FOREIGN KEY ([OperatorID])  REFERENCES [MasterData].[Operator]([OperatorID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_OperatorID] ON [AccessControl].[OperatorProfile] 
 (
  [OperatorID] ASC
 )

GO



CREATE NONCLUSTERED INDEX [fkIdx_BusinessTransactionID] ON [AccessControl].[TransactionAction] 
 (
  [BusinessTransactionID] ASC
 )

GO

CREATE TABLE [AccessControl].[OperatorProfileTransactionAction]
(
 [OperatorProfileID]     int NOT NULL ,
 [BusinessTransactionID] int NOT NULL ,
 [BusinessActionID]      int NOT NULL ,


 CONSTRAINT [PK_OperatorProfileTransactionAction] PRIMARY KEY CLUSTERED ([OperatorProfileID] ASC, [BusinessTransactionID] ASC, [BusinessActionID] ASC),
 CONSTRAINT [FK_OperatorProfileTransactionAction_OperatorProfile] FOREIGN KEY ([OperatorProfileID])  REFERENCES [AccessControl].[OperatorProfile]([OperatorProfileID]),
 CONSTRAINT [FK_OperatorProfileTransactionAction_TransactionAction] FOREIGN KEY ([BusinessTransactionID], [BusinessActionID])  REFERENCES [AccessControl].[TransactionAction]([BusinessTransactionID], [BusinessActionID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessTransactionID_BusinessActionID] ON [AccessControl].[OperatorProfileTransactionAction] 
 (
  [BusinessTransactionID] ASC, 
  [BusinessActionID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_OperatorProfileID] ON [AccessControl].[OperatorProfileTransactionAction] 
 (
  [OperatorProfileID] ASC
 )

GO

CREATE TABLE [AccessControl].[RecoverPassword]
(
 [RequestID]      int IDENTITY (1, 1) NOT NULL ,
 [BusinessUserID] int NOT NULL ,
 [RecoverCode]    char(4) NOT NULL ,
 [IsActive]       bit NOT NULL ,
 [RequestDate]    datetime NOT NULL ,


 CONSTRAINT [PK_recoverpassword] PRIMARY KEY CLUSTERED ([RequestID] ASC),
 CONSTRAINT [FK_967] FOREIGN KEY ([BusinessUserID])  REFERENCES [AccessControl].[BusinessUser]([BusinessUserID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_968] ON [AccessControl].[RecoverPassword] 
 (
  [BusinessUserID] ASC
 )

GO



CREATE TABLE [AccessControl].[UserCompanyProfile]
(
 [BusinessUserID]   int NOT NULL ,
 [StoreID]          int NOT NULL ,
 [CompanyProfileID] int NOT NULL ,


 CONSTRAINT [PK_UserControlProfile] PRIMARY KEY CLUSTERED ([BusinessUserID] ASC, [StoreID] ASC),
 CONSTRAINT [FK_UserCompanyProfile_BusinessUser] FOREIGN KEY ([BusinessUserID])  REFERENCES [AccessControl].[BusinessUser]([BusinessUserID]),
 CONSTRAINT [FK_UserCompanyProfile_CompanyProfile] FOREIGN KEY ([CompanyProfileID])  REFERENCES [AccessControl].[CompanyProfile]([CompanyProfileID]),
 CONSTRAINT [FK_UserCompanyProfile_Sotre] FOREIGN KEY ([StoreID])  REFERENCES [MasterData].[Store]([StoreID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessUserID] ON [AccessControl].[UserCompanyProfile] 
 (
  [BusinessUserID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_CompanyProfileID] ON [AccessControl].[UserCompanyProfile] 
 (
  [CompanyProfileID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_UserCompanyProfile_Store] ON [AccessControl].[UserCompanyProfile] 
 (
  [StoreID] ASC
 )

GO

CREATE TABLE [AccessControl].[UserOperatorProfile]
(
 [BusinessUserID]    int NOT NULL ,
 [OperatorProfileID] int NOT NULL ,


 CONSTRAINT [PK_UserOperatorProfile] PRIMARY KEY CLUSTERED ([BusinessUserID] ASC, [OperatorProfileID] ASC),
 CONSTRAINT [FK_UserOperatorProfile_BusinessUser] FOREIGN KEY ([BusinessUserID])  REFERENCES [AccessControl].[BusinessUser]([BusinessUserID]),
 CONSTRAINT [FK_UserOperatorProfile_OperatorProfile] FOREIGN KEY ([OperatorProfileID])  REFERENCES [AccessControl].[OperatorProfile]([OperatorProfileID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_BusinessUserID] ON [AccessControl].[UserOperatorProfile] 
 (
  [BusinessUserID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_OperatorProfileID] ON [AccessControl].[UserOperatorProfile] 
 (
  [OperatorProfileID] ASC
 )

GO
