CREATE FUNCTION F_Efikasnost_Sluzbenika
(
	@cena_automobila int
) 
RETURNS @resTable TABLE
(
	ime varchar(30),
	prezime varchar(30),
	datumZaposlenja date
)
AS
BEGIN
	INSERT @resTable
	SELECT DISTINCT ime = Sluzbeniks.Ime, prezime = Sluzbeniks.Prezime, datumZaposlenja = Sluzbeniks.DatumZaposlenja
	from Sluzbeniks
	inner join Pregovaras on Sluzbeniks.Id = Pregovaras.SluzbenikId
	inner join Automobils on Pregovaras.KupacId = Automobils.KupacId
	where Automobils.Cena > @cena_automobila
	RETURN
END;

select * from F_Efikasnost_Sluzbenika(15000);
