CREATE OR ALTER PROC sp_Transports_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ALL')
	BEGIN
		SELECT *
		FROM Transports
	END
	ELSE IF (@Type_search = 'FLIGHT NUMBER')
	BEGIN
		SELECT *
		FROM Transports
		WHERE FlightNumber = @Value_search
	END
END