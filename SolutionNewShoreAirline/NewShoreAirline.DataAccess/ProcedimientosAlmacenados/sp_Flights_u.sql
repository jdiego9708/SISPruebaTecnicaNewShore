CREATE OR ALTER PROC sp_Flights_u
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