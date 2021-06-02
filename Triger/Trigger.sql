CREATE TRIGGER T_Sluzbenik_Unos
ON AutoKompanijas
AFTER INSERT
AS
declare @ime varchar(30);
declare @prezime varchar(30);
declare @datumZaposlenja date;
set @ime='Marko';
set @prezime='Markovic';
select @datumZaposlenja=i.DatumOsnivanja from inserted i;
insert into Sluzbeniks
(Ime,Prezime,DatumZaposlenja)
values(@ime,@prezime,@datumZaposlenja);