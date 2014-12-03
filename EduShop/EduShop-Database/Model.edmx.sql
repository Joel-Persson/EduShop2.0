
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/07/2014 15:04:45
-- Generated from EDMX file: C:\Users\Andreas\Documents\Webb utveckling\LIA\Edument\EduShop\EduShop-Unsecure\EduShop-Database\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EduShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_UserOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderRow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderRowSet] DROP CONSTRAINT [FK_OrderOrderRow];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrderRow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderRowSet] DROP CONSTRAINT [FK_ProductOrderRow];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductReview]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReviewSet] DROP CONSTRAINT [FK_ProductReview];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[OrderRowSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderRowSet];
GO
IF OBJECT_ID(N'[dbo].[ReviewSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReviewSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [ShortDescription] nvarchar(max)  NOT NULL,
    [ImgUrl] nvarchar(max)  NOT NULL,
    [AverageRating] float  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'OrderRowSet'
CREATE TABLE [dbo].[OrderRowSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] bigint  NOT NULL,
    [OrderId] int  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'ReviewSet'
CREATE TABLE [dbo].[ReviewSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Rating] float  NOT NULL,
    [DateAdded] datetime  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderRowSet'
ALTER TABLE [dbo].[OrderRowSet]
ADD CONSTRAINT [PK_OrderRowSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReviewSet'
ALTER TABLE [dbo].[ReviewSet]
ADD CONSTRAINT [PK_ReviewSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_UserOrder]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrder'
CREATE INDEX [IX_FK_UserOrder]
ON [dbo].[OrderSet]
    ([UserId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderRowSet'
ALTER TABLE [dbo].[OrderRowSet]
ADD CONSTRAINT [FK_OrderOrderRow]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderRow'
CREATE INDEX [IX_FK_OrderOrderRow]
ON [dbo].[OrderRowSet]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderRowSet'
ALTER TABLE [dbo].[OrderRowSet]
ADD CONSTRAINT [FK_ProductOrderRow]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrderRow'
CREATE INDEX [IX_FK_ProductOrderRow]
ON [dbo].[OrderRowSet]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'ReviewSet'
ALTER TABLE [dbo].[ReviewSet]
ADD CONSTRAINT [FK_ProductReview]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductReview'
CREATE INDEX [IX_FK_ProductReview]
ON [dbo].[ReviewSet]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------