USE [WebApi]
GO
/****** Object:  Table [dbo].[ApiLogs]    Script Date: 2024-12-24 16:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[MethodName] [nvarchar](200) NOT NULL,
	[MethodParams] [nvarchar](max) NULL,
	[RequestIp] [nvarchar](100) NULL,
	[ResponseCode] [nvarchar](100) NOT NULL,
	[ExecDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ApiLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2024-12-24 16:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[CategoryDescrip] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2024-12-24 16:56:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductDescrip] [nvarchar](1000) NULL,
	[Specifications] [nvarchar](max) NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Price] [money] NOT NULL,
	[Discount] [money] NULL,
	[Quantity] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryDescrip], [IsActive], [CreateDate]) VALUES (N'1ec4ebd4-e8b2-4f93-82b3-1f7109119879', N'Test Category 3', NULL, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryDescrip], [IsActive], [CreateDate]) VALUES (N'eca092ce-b655-426c-8185-da1ca6d0a4d9', N'Test Category 2', NULL, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryDescrip], [IsActive], [CreateDate]) VALUES (N'3f3a639f-4247-4dc8-a9d9-f18d9a240ffb', N'Test Category', NULL, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryDescrip], [IsActive], [CreateDate]) VALUES (N'c4ea8e79-d589-4439-9b09-fe4c38fa1d5d', N'Test Category 4', NULL, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Products] ([Id], [ProductName], [ProductDescrip], [Specifications], [CategoryId], [Price], [Discount], [Quantity], [IsActive], [CreateDate]) VALUES (N'4b2d6b48-d42c-46fa-a4ef-8c6adb5fa6a5', N'Test Product 4', N'Test product 4 description', NULL, N'eca092ce-b655-426c-8185-da1ca6d0a4d9', 100.0000, NULL, 10, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Products] ([Id], [ProductName], [ProductDescrip], [Specifications], [CategoryId], [Price], [Discount], [Quantity], [IsActive], [CreateDate]) VALUES (N'7ca8f761-22b0-4f02-bcda-90f6515ccf90', N'Test Product 3', N'Test product 3 description', NULL, N'eca092ce-b655-426c-8185-da1ca6d0a4d9', 80.0000, NULL, 5, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Products] ([Id], [ProductName], [ProductDescrip], [Specifications], [CategoryId], [Price], [Discount], [Quantity], [IsActive], [CreateDate]) VALUES (N'6fb725d5-9308-478c-94db-a2596b193447', N'Test Product', N'Test product description', NULL, N'3f3a639f-4247-4dc8-a9d9-f18d9a240ffb', 100.0000, 10.0000, 1, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
INSERT [dbo].[Products] ([Id], [ProductName], [ProductDescrip], [Specifications], [CategoryId], [Price], [Discount], [Quantity], [IsActive], [CreateDate]) VALUES (N'ce037aab-bf84-4f12-b6f9-d731537e0a5c', N'Test Product 2', N'Test product 2 description', NULL, N'3f3a639f-4247-4dc8-a9d9-f18d9a240ffb', 50.0000, 5.0000, 10, 1, CAST(N'2024-12-21T19:39:57.073' AS DateTime))
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_ProductsCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_ProductsCategories]
GO
