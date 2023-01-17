CREATE TABLE [dbo].[Details_journeys](
	[Id_journey] [int] NOT NULL,
	[Id_flight] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flights]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flights](
	[Id_flight] [int] IDENTITY(1,1) NOT NULL,
	[Id_transport] [int] NOT NULL,
	[Origin] [varchar](50) NOT NULL,
	[Destination] [varchar](50) NOT NULL,
	[Price] [decimal](19, 2) NOT NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
	[Id_flight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Journeys]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Journeys](
	[Id_journey] [int] IDENTITY(1,1) NOT NULL,
	[Origin] [varchar](50) NOT NULL,
	[Destination] [varchar](50) NOT NULL,
	[Price] [decimal](19, 2) NOT NULL,
 CONSTRAINT [PK_Journeys] PRIMARY KEY CLUSTERED 
(
	[Id_journey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transports]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transports](
	[Id_transport] [int] IDENTITY(1,1) NOT NULL,
	[FlightCarrier] [varchar](50) NULL,
	[FlightNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Transports] PRIMARY KEY CLUSTERED 
(
	[Id_transport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Details_journeys]  WITH CHECK ADD  CONSTRAINT [FK_Details_journeys_Flights] FOREIGN KEY([Id_flight])
REFERENCES [dbo].[Flights] ([Id_flight])
GO
ALTER TABLE [dbo].[Details_journeys] CHECK CONSTRAINT [FK_Details_journeys_Flights]
GO
ALTER TABLE [dbo].[Details_journeys]  WITH CHECK ADD  CONSTRAINT [FK_Details_journeys_Journeys] FOREIGN KEY([Id_journey])
REFERENCES [dbo].[Journeys] ([Id_journey])
GO
ALTER TABLE [dbo].[Details_journeys] CHECK CONSTRAINT [FK_Details_journeys_Journeys]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Flights_Transports] FOREIGN KEY([Id_transport])
REFERENCES [dbo].[Transports] ([Id_transport])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Flights_Transports]
GO
/****** Object:  StoredProcedure [dbo].[sp_Details_journeys_i]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Details_journeys_i]
@Id_journey int,
@Id_flight int
AS
BEGIN
	INSERT INTO Details_journeys
	VALUES (@Id_journey, @Id_flight)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Flights_g]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Flights_g]
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ID TRANSPORT')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE tr.Id_transport = CONVERT(int, @Value_search)
	END
	ELSE IF (@Type_search = 'ORIGIN')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE Origin = @Value_search
	END
	ELSE IF (@Type_search = 'DESTINATION')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE Destination = @Value_search
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Flights_i]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Flights_i]
@Id_flight int output,
@Id_transport int,
@Origin varchar(50),
@Destination varchar(50),
@Price decimal(19,2)
AS
BEGIN
	INSERT INTO Flights
	VALUES (@Id_transport, @Origin, @Destination, @Price)

	SET @Id_flight = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Flights_u]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Flights_u]
@Id_flight int,
@Id_transport int,
@Origin varchar(50),
@Destination varchar(50),
@Price decimal(19,2)
AS
BEGIN
	UPDATE Flights SET
	Id_transport = @Id_transport, 
	Origin = @Origin, 
	Destination = @Destination, 
	Price = @Price
	WHERE Id_flight = @Id_flight
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Journey_g]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Journey_g]
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'ORIGIN')
	BEGIN
		SELECT *
		FROM Journeys
		WHERE Origin = @Texto_busqueda
	END
	else IF (@Tipo_busqueda = 'DESTINATION')
	BEGIN
		SELECT *
		FROM Journeys
		WHERE Destination = @Texto_busqueda
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Journey_i]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Journey_i]
@IdJourney int output,
@Origin varchar(50),
@Destination varchar(50),
@Price decimal(19, 2)
AS
BEGIN
	INSERT INTO Journeys
	VALUES (@Origin, @Destination, @Price)

	SET @IdJourney = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Journey_u]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Journey_u]
@Id_journey int,
@Origin varchar(50),
@Destination varchar(50),
@Price decimal(19, 2)
AS
BEGIN
	UPDATE Journeys SET
	Origin = @Origin, 
	Destination = @Destination, 
	Price = @Price
	WHERE Id_journey = @Id_journey
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transport_i]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Transport_i]
@Id_transport int output,
@FlightCarrier varchar(50),
@FlightNumber varchar(50)
AS
BEGIN
	INSERT INTO Transports
	VALUES (@FlightCarrier, @FlightNumber)

	SET @Id_transport = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transport_u]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Transport_u]
@Id_transport int,
@FlightCarrier varchar(50),
@FlightNumber varchar(50)
AS
BEGIN
	UPDATE Transports SET
	FlightCarrier = @FlightCarrier, 
	FlightNumber = @FlightNumber
	WHERE Id_transport = @Id_transport;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transports_g]    Script Date: 16/01/2023 11:42:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_Transports_g]
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'ALL')
	BEGIN
		SELECT *
		FROM Transports
	END
	ELSE IF (@Tipo_busqueda = 'FLIGHT NUMBER')
	BEGIN
		SELECT *
		FROM Transports
		WHERE FlightNumber = @Texto_busqueda
	END
END
GO
USE [master]
GO
ALTER DATABASE [NewShoreAirlineBD] SET  READ_WRITE 
GO
