CREATE OR ALTER PROC sp_Transport_i
@Id_transport int output,
@FlightCarrier varchar(50),
@FlightNumber varchar(50)
AS
BEGIN
	INSERT INTO Transports
	VALUES (@FlightCarrier, @FlightNumber)

	SET @Id_transport = SCOPE_IDENTITY();
END