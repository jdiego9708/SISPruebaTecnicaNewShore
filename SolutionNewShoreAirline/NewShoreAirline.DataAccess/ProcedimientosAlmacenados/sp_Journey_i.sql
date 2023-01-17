CREATE OR ALTER PROC sp_Journey_i
@Id_journey int output,
@Origin varchar(50),
@Destination varchar(50),
@Price decimal(19, 2)
AS
BEGIN
	INSERT INTO Journeys
	VALUES (@Origin, @Destination, @Price)

	SET @Id_journey = SCOPE_IDENTITY();
END