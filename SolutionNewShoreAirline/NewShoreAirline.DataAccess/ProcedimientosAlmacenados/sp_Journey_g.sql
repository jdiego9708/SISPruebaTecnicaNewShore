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
	else IF (@Type_search = 'DESTINATION')
	BEGIN
		SELECT *
		FROM Journeys
		WHERE Destination = @Value_search
	END
END