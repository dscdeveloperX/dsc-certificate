/*
create alter procedure sp_ProvinceCreate(
	@ProvinceName varchar(50),
	@ProvinceActive tinyint)
as
begin
insert into Province(
ProvinceName,
ProvinceActive)values(
@ProvinceName,
@ProvinceActive);
end;
*/

/*SET IDENTITY_INSERT Province ON;
insert into Province (ProvinceID, ProvinceName,ProvinceActive) values (1, 'Azuay', 1),
( 2, 'Bolívar', 1),
( 3, 'Cañar', 1),
( 4, 'Carchi', 1),
( 5, 'Chimborazo', 1),
( 6, 'Cotopaxi', 1),
( 7, 'El Oro', 1),
( 8, 'Esmeraldas', 1),
( 9, 'Galápagos', 1),
( 10, 'Guayas', 1),
( 11, 'Imbabura', 1),
( 12, 'Loja', 1),
( 13, 'Los Ríos', 1),
( 14, 'Manabí', 1),
( 15, 'Morona Santiago', 1),
( 16, 'Napo', 1),
( 17, 'Orellana', 1),
( 18, 'Pastaza', 1),
( 19, 'Pichincha', 1),
( 20, 'Santa Elena', 1),
( 21, 'Santo Domingo de los Tsáchilas', 1),
( 22, 'Sucumbíos', 1),
( 23, 'Tungurahua', 1),
( 24, 'Zamora Chinchipe', 1);
SET IDENTITY_INSERT Province OFF;
*/
--*****************************************************************
/*
create procedure sp_ProvinceRead(
	@ProvinceID int = NULL,
	@ProvinceActive tinyint=NULL,
	@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
ProvinceID,
ProvinceName,
ProvinceActive
from Province
where (@ProvinceID IS NULL or ProvinceID = @ProvinceID)
and (@ProvinceActive IS NULL or ProvinceActive = @ProvinceActive)
order by ProvinceName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_ProvinceRead NULL, NULL,1,100
--*************************************************************
/*
create procedure sp_ProvinceCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Province);
end;
*/
/*
declare @valor bigint
exec sp_ProvinceCountRead @valor out
select @valor
*/
--*************************************************************
/*
create alter procedure sp_ProvinceCityRead(
	@ProvinceID int = NULL,
	@ProvinceActive tinyint=NULL,
	@Page int,
	@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
c.CityID,
p.ProvinceID,
p.ProvinceName,
c.CityName,
c.CityActive,
p.ProvinceActive
from Province p inner join City c on (p.ProvinceID = c.ProvinceID)
where (@ProvinceID IS NULL or p.ProvinceID = @ProvinceID)
and (@ProvinceActive IS NULL or p.ProvinceActive = @ProvinceActive)
order by p.ProvinceName, c.CityName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_ProvinceCityRead null, null, 1,100

--*************************************************************
/*
create alter procedure sp_ProvinceUpdate(
	@ProvinceID int,
	@ProvinceName varchar(50),
	@ProvinceActive tinyint)
as
begin
update Province
set ProvinceName = @ProvinceName,
ProvinceActive = @ProvinceActive
where ProvinceID =@ProvinceID;
end;
*/
--exec sp_ProvinceUpdate 1, 'Azuay', 1

--***********************************************************
/*
create procedure sp_ProvinceDelete(
	@ProvinceID int)
as
begin
delete
from Province
where ProvinceID = @ProvinceID;
end;
*/
--exec sp_ProvinceDelete 24
