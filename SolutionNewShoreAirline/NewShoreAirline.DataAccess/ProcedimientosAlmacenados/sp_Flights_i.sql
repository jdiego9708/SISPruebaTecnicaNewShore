CREATE OR ALTER PROC sp_Flights_i
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