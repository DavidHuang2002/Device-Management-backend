USE [DeviceManagementDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Device](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Type] VARCHAR(50) NOT NULL,
    [Status] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(100) NULL,
    [AddedDate] DATE NOT NULL,
    [LastCheckInTime] DATETIME NULL,
    [Notes] VARCHAR(250) NULL,
    [AzureDeviceId] VARCHAR(100) NULL,
    [AzureDeviceKey] VARCHAR(100) NULL,
    [AzureConnectionString] VARCHAR(250) NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO