CREATE OR ALTER PROC sp_Flights_g
@Type_search varchar(50),
@Value_search varchar(50)
AS
BEGIN
	IF (@Type_search = 'ID TRANSPORT')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE tr.Id_transport = CONVERT(int, @Value_search)
	END
	ELSE IF (@Type_search = 'ORIGIN')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE Origin = @Value_search
	END
	ELSE IF (@Type_search = 'DESTINATION')
	BEGIN
		SELECT *
		FROM Flights fl
		INNER JOIN Transports tr ON fl.Id_transport = tr.Id_transport
		WHERE Destination = @Value_search
	END
END