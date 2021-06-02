CREATE PROCEDURE P_AutoKompanija
AS
DECLARE P_Cursor CURSOR
FOR 
SELECT AutoKompanijas.Naziv
FROM AutoKompanijas inner join AutoSalons on AutoKompanijas.Id = AutoSalons.AutoKompanijaId
inner join Automobils on AutoSalons.Id = Automobils.AutoSalonId

DECLARE
@naziv VARCHAR(30);

BEGIN
	OPEN P_Cursor;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			PRINT @naziv;
			FETCH NEXT FROM P_Cursor
			INTO @naziv;
		END;
		CLOSE P_Cursor;
		DEALLOCATE P_Cursor;
END;exec P_AutoKompanija;