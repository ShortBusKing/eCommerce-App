USE [eCommerce]
GO
/****** Object:  Table [dbo].[DeliveryMethods]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


DROP TABLE  [dbo].[OrderItems]
GO 

DROP TABLE [dbo].[Order]
GO
DROP TABLE [dbo].[DeliveryMethods]
GO
DROP TABLE [dbo].Products
GO

DROP TABLE [dbo].[ProductBrands]
GO
DROP TABLE [dbo].[ProductTypes]
GO


CREATE TABLE [dbo].[DeliveryMethods](
	[Id] [int] IDENTITY (1,1) NOT NULL,
	[ShortName] [varchar](128) NULL,
	[DeliveryTime] [datetime2](7) NULL,
	[Description] [varchar](2000) NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_DeliveryMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY (1,1) NOT NULL,
	[BuyerEmail] [varchar](256) NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ShipToAddress_FirstName] [varchar](128) NULL,
	[ShipToAddress_LastName] [varchar](128) NULL,
	[ShipToAddress_Street] [varchar](128) NULL,
	[ShipToAddress_City] [varchar](128) NULL,
	[ShipToAddress_State] [varchar](128) NULL,
	[ShipToAddress_Zipcode] [varchar](128) NULL,
	[DeliveryMethodId] [int] NULL,
	[Subtotal] [money] NOT NULL,
	[Status] [varchar](128) NOT NULL,
	[PaymentIntentId] [text] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY (1,1) NOT NULL,
	[ItemOrdered_ProductItemId] [int] NULL,
	[ItemOrdered_ProductName] [varchar](128) NULL,
	[ItemOrdered_PictureUrl] [varchar](max) NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBrands]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductBrands](
	[Id] [int] IDENTITY (1,1)NOT NULL,
	[Name] [varchar](128) NULL,
 CONSTRAINT [PK_ProductBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY (1,1)NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](2000) NOT NULL,
	[Price] [money] NOT NULL,
	[PictureUrl] [varchar](max) NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[ProductBrandId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 12/21/2020 6:21:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductTypes](
	[Id] [int] IDENTITY (1,1)NOT NULL,
	[Name] [varchar](128) NULL,
 CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_DeliveryMethods_DeliveryMethodId] FOREIGN KEY([DeliveryMethodId])
REFERENCES [dbo].[DeliveryMethods] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_DeliveryMethods_DeliveryMethodId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Order_OrderId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductBrands_ProductBrandId] FOREIGN KEY([ProductBrandId])
REFERENCES [dbo].[ProductBrands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductBrands_ProductBrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductTypes_ProductTypeId] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductTypes_ProductTypeId]
GO
