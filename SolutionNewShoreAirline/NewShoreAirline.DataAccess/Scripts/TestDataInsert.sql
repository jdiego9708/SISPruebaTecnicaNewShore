SET IDENTITY_INSERT [dbo].[Transports] ON 
GO
INSERT [dbo].[Transports] ([Id_transport], [FlightCarrier], [FlightNumber]) VALUES (1, N'Armando', N'BC564')
GO
INSERT [dbo].[Transports] ([Id_transport], [FlightCarrier], [FlightNumber]) VALUES (2, N'Julio', N'RP987')
GO
INSERT [dbo].[Transports] ([Id_transport], [FlightCarrier], [FlightNumber]) VALUES (3, N'Luis', N'RD456')
GO
SET IDENTITY_INSERT [dbo].[Transports] OFF
GO
SET IDENTITY_INSERT [dbo].[Flights] ON 
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (2, 2, N'MZL', N'BOG', CAST(200000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (3, 1, N'BOG', N'MZL', CAST(190000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (4, 3, N'PEI', N'MED', CAST(110000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (5, 3, N'MED', N'CTG', CAST(150000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (6, 2, N'CTG', N'PEI', CAST(240000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (7, 3, N'MZL', N'BOG', CAST(300000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (8, 1, N'BOG', N'PEI', CAST(200000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (9, 1, N'BOG', N'MZL', CAST(180000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Flights] ([Id_flight], [Id_transport], [Origin], [Destination], [Price]) VALUES (11, 2, N'BOG', N'CTG', CAST(160000.00 AS Decimal(19, 2)))
GO
SET IDENTITY_INSERT [dbo].[Flights] OFF
GO
SET IDENTITY_INSERT [dbo].[Journeys] ON 
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (1, N'MZL', N'BOG', CAST(200000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (2, N'BOG', N'MZL', CAST(190000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (3, N'PEI', N'CTG', CAST(260000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (4, N'CTG', N'PEI', CAST(240000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (5, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (6, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (7, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (8, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (9, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (10, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
INSERT [dbo].[Journeys] ([Id_journey], [Origin], [Destination], [Price]) VALUES (11, N'MZL', N'CTG', CAST(360000.00 AS Decimal(19, 2)))
GO
SET IDENTITY_INSERT [dbo].[Journeys] OFF
GO
