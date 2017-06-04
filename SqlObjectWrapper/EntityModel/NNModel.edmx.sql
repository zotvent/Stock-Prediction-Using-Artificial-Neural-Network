
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/17/2017 23:45:51
-- Generated from EDMX file: D:\VS\2015\Projects\NeuralNetworkProject\SqlObjectWrapper\EntityModel\NNModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NeuronNetworkDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StockStockHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StocksHistory] DROP CONSTRAINT [FK_StockStockHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_PredictedStockHistoryStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PredictedStockHistories] DROP CONSTRAINT [FK_PredictedStockHistoryStock];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StocksHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StocksHistory];
GO
IF OBJECT_ID(N'[dbo].[Stocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stocks];
GO
IF OBJECT_ID(N'[dbo].[PredictedStockHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PredictedStockHistories];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StocksHistory'
CREATE TABLE [dbo].[StocksHistory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [High] float  NOT NULL,
    [Low] float  NOT NULL,
    [Open] float  NOT NULL,
    [Close] float  NOT NULL,
    [Volume] float  NOT NULL,
    [StockId] int  NOT NULL
);
GO

-- Creating table 'Stocks'
CREATE TABLE [dbo].[Stocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Ticker] nvarchar(max)  NOT NULL,
    [Data] varbinary(max)  NOT NULL
);
GO

-- Creating table 'PredictedStockHistories'
CREATE TABLE [dbo].[PredictedStockHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Close] float  NOT NULL,
    [StockId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StocksHistory'
ALTER TABLE [dbo].[StocksHistory]
ADD CONSTRAINT [PK_StocksHistory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [PK_Stocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PredictedStockHistories'
ALTER TABLE [dbo].[PredictedStockHistories]
ADD CONSTRAINT [PK_PredictedStockHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StockId] in table 'StocksHistory'
ALTER TABLE [dbo].[StocksHistory]
ADD CONSTRAINT [FK_StockStockHistory]
    FOREIGN KEY ([StockId])
    REFERENCES [dbo].[Stocks]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StockStockHistory'
CREATE INDEX [IX_FK_StockStockHistory]
ON [dbo].[StocksHistory]
    ([StockId]);
GO

-- Creating foreign key on [StockId] in table 'PredictedStockHistories'
ALTER TABLE [dbo].[PredictedStockHistories]
ADD CONSTRAINT [FK_PredictedStockHistoryStock]
    FOREIGN KEY ([StockId])
    REFERENCES [dbo].[Stocks]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PredictedStockHistoryStock'
CREATE INDEX [IX_FK_PredictedStockHistoryStock]
ON [dbo].[PredictedStockHistories]
    ([StockId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------