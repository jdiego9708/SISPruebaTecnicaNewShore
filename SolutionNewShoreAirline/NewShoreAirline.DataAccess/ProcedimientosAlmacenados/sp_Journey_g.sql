CREATE OR ALTER PROC sp_Journey_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ORIGIN')
	BEGIN
		SELECT *
		FROM Journeys
		WHERE Origin = @Value_search
	END
	ELSE IF (@Type_search = 'DESTINATION')
	BEGIN
		SELECT *
		FROM Journeys
		WHERE Destination = @Value_search
	END
	ELSE IF (@Type_search = 'FLIGHTS JOURNEY')
	BEGIN
		SELECT fl.*, de.*
		FROM Journeys jo
		INNER JOIN Details_journeys de ON jo.Id_journey = de.Id_journey
		INNER JOIN Flights fl ON de.Id_flight = fl.Id_flight
		WHERE jo.Id_journey = CONVERT(int, @Value_search)
	END
END