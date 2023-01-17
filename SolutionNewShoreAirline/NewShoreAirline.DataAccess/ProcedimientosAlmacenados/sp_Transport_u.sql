CREATE OR ALTER PROC sp_Transport_u
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