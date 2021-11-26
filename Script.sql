CREATE DATABASE [HRManagement]

GO

USE [HRManagement]

GO

CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DepartmentId] ASC)
)

GO

INSERT INTO [dbo].[Department]([Name])
VALUES 
('Sales'),
('Product')

GO

CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](50) NOT NULL,
CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([StatusId] ASC)
)

GO

INSERT INTO [dbo].[Status]([Name])
VALUES 
('Approved'),
('Pending'),
('Disabled')

GO

CREATE TABLE [dbo].[HumanResource](
	[HumanResourceId] [INT] IDENTITY(1,1) NOT NULL,
	[FirstName] [NVARCHAR](50) NOT NULL,
	[Surname] [NVARCHAR](50) NOT NULL,
	[Email] [NVARCHAR](200) NOT NULL,
	[DOB] [DATE] NULL,
	[DepartmentId] [INT] NOT NULL,
	[StatusId] [INT] NOT NULL,
	[EmployeeNumber] [INT] NOT NULL,
	[AddedOn] [datetime2] NOT NULL,
	[ModifiedOn] [datetime2] NULL,
	[DeletedOn] [datetime2] NULL,
CONSTRAINT [PK_HumanResource] PRIMARY KEY CLUSTERED ([HumanResourceId] ASC),
CONSTRAINT [FK_HumanResource_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status]([StatusId]),
CONSTRAINT [FK_HumanResource_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department]([DepartmentId]),
CONSTRAINT [UC_Email] UNIQUE (Email),
CONSTRAINT [UC_EmployeeNumber] UNIQUE (EmployeeNumber)
)

GO

CREATE INDEX IX_Department
ON [dbo].[HumanResource]([DepartmentId])

GO

CREATE INDEX IX_Status
ON [dbo].[HumanResource]([StatusId])

GO

CREATE INDEX IX_FirstName
ON [dbo].[HumanResource]([FirstName])

GO

-- Optionally, add dummy data

INSERT INTO [dbo].[HumanResource]([FirstName],[Surname],[Email],[DOB],[DepartmentId],[StatusId],[EmployeeNumber],[AddedOn])
VALUES 
('John','Doe','john.doe@hotmail.com','1990-05-25',1,1,1,GETUTCDATE()),
('Jane','Doe','jane.doe@hotmail.com','2000-11-25',2,1,2,GETUTCDATE())

GO