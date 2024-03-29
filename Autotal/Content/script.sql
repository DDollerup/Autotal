/****** Object:  Table [dbo].[Brand]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Color]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[OpenHours] [nvarchar](max) NULL,
	[GoogleMapsEmbed] [nvarchar](max) NULL,
	[InterestRate] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Image]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](255) NULL,
	[Alt] [nvarchar](255) NULL,
	[ProductID] [int] NULL,
	[SubpageID] [int] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](255) NULL,
	[BHP] [int] NULL,
	[Year] [int] NULL,
	[Km] [int] NULL,
	[Price] [float] NULL,
	[ColorID] [int] NOT NULL,
	[Smoker] [bit] NULL,
	[Inspected] [bit] NULL,
	[Description] [nvarchar](max) NULL,
	[Views] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subpage]    Script Date: 28-10-2017 20:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subpage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Subpage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([ID], [Name]) VALUES (1, N'VW')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (2, N'Opel')
SET IDENTITY_INSERT [dbo].[Brand] OFF
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([ID], [Name]) VALUES (1, N'Sølvgrå')
INSERT [dbo].[Color] ([ID], [Name]) VALUES (2, N'Blå')
SET IDENTITY_INSERT [dbo].[Color] OFF
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([ID], [Address], [OpenHours], [GoogleMapsEmbed], [InterestRate]) VALUES (1, N'AUTOTAL
Kannikegade 12
8000 Aarhus C
Tlf. 87 87 12 34
SALG@AUTOTAL.DK
CVR. 12123434', N'Man-tors kl. 10-18
Fredag kl. 10-19
Lørdag lukket
Søndag kl. 10-16
Online altid', N'<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2222.0658091292803!2d10.207918316074265!3d56.15596676829214!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x464c3f912f62ab41%3A0x48867ab4c702e53a!2sKannikegade+12%2C+8000+Aarhus+C!5e0!3m2!1sda!2sdk!4v1509090500529" width="600" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>
', 2.25)
SET IDENTITY_INSERT [dbo].[Contact] OFF
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([ID], [ImageURL], [Alt], [ProductID], [SubpageID]) VALUES (1, N'golf7.jpg', N'Golf', 1, 0)
INSERT [dbo].[Image] ([ID], [ImageURL], [Alt], [ProductID], [SubpageID]) VALUES (2, N'golf7tsi.jpg', N'Golf TSI', 0, 0)
INSERT [dbo].[Image] ([ID], [ImageURL], [Alt], [ProductID], [SubpageID]) VALUES (3, N'polo.jpg', N'Polo', 2, 0)
INSERT [dbo].[Image] ([ID], [ImageURL], [Alt], [ProductID], [SubpageID]) VALUES (4, N'polo2.jpg', N'Polo', 0, 0)
INSERT [dbo].[Image] ([ID], [ImageURL], [Alt], [ProductID], [SubpageID]) VALUES (5, N'golf7tdi.jpg', N'Golf TDI', 0, 0)
SET IDENTITY_INSERT [dbo].[Image] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [BrandID], [Model], [BHP], [Year], [Km], [Price], [ColorID], [Smoker], [Inspected], [Description], [Views]) VALUES (1, 1, N'Polo', 74, 2014, 124000, 165000, 1, 0, 1, N'akjædajsdsadæaskd', 5002)
INSERT [dbo].[Product] ([ID], [BrandID], [Model], [BHP], [Year], [Km], [Price], [ColorID], [Smoker], [Inspected], [Description], [Views]) VALUES (2, 1, N'Golf', 110, 2013, 123000, 200000, 2, 0, 1, N'akjædajsdsadæaskd', 257)
INSERT [dbo].[Product] ([ID], [BrandID], [Model], [BHP], [Year], [Km], [Price], [ColorID], [Smoker], [Inspected], [Description], [Views]) VALUES (3, 2, N'Astra', 98, 2011, 223000, 90000, 2, 1, 0, N'adlkfæsalkdsalka', 1525)
INSERT [dbo].[Product] ([ID], [BrandID], [Model], [BHP], [Year], [Km], [Price], [ColorID], [Smoker], [Inspected], [Description], [Views]) VALUES (4, 2, N'Karl', 74, 2014, 97000, 75000, 1, 0, 1, N'askdamcc,c,', 259)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Subpage] ON 

INSERT [dbo].[Subpage] ([ID], [Title], [Content]) VALUES (1, N'Forside', N'Content Content')
SET IDENTITY_INSERT [dbo].[Subpage] OFF
