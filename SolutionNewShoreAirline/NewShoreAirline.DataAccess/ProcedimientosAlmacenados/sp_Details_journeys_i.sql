CREATE OR ALTER PROC sp_Details_journeys_i
@Id_journey int,
@Id_flight int
AS
BEGIN
	INSERT INTO Details_journeys
	VALUES (@Id_journey, @Id_flight)
END