CREATE OR ALTER PROC sp_Journey_u
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