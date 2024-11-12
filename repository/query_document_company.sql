/*
create procedure sp_CompanyCreate(
@CompanyRuc varchar(13),
@ProvinceID int,
@CityID int,
@CompanyName varchar(200),
@CompanyAddress varchar(200),
@CompanyPhone varchar(100),
@CompanyActive tinyint 
)
as 
begin
insert into Company(
CompanyRuc,
ProvinceID,
CityID,
CompanyName,
CompanyAddress,
CompanyPhone,
CompanyActive)values(
@CompanyRuc,
@ProvinceID,
@CityID,
@CompanyName,
@CompanyAddress,
@CompanyPhone,
@CompanyActive
);
end;
*/
/*
exec sp_CompanyCreate
'1000000000001',
10,
4,
'INFOMERCA S. A.',
'Chimborazo 418 y clemente ballen',
'04233555',
1; 
*/
/*
exec sp_CompanyCreate
'2000000000001',
10,
4,
'MULTIPLISERVIC S. A.',
'Chimborazo 418 y clemente ballen',
'04233555',
1; 
*/
--****************************************************
/*
create alter procedure sp_CompanyRead(
@CompanyID int=NULL,
@CompanyActive tinyint=NULL,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
CompanyID,
CompanyRuc,
ProvinceID,
CityID,
CompanyName,
CompanyAddress,
CompanyPhone,
CompanyActive
from Company
where (@CompanyID IS NULL or CompanyID = @CompanyID)
and (@CompanyActive IS NULL or CompanyActive = @CompanyActive)
order by CompanyName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_CompanyRead NULL,NULL,1,1

--*************************************************************************

create alter procedure sp_CompanyReadFull(
@CompanyID int=NULL,
@CompanyActive tinyint=NULL,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
c.CompanyID,
c.CompanyRuc,
c.ProvinceID,
p.ProvinceName,
c.CityID,
ct.CityName,
c.CompanyName,
c.CompanyAddress,
c.CompanyPhone,
c.CompanyPhoto,
c.CompanyUrlVerification,
c.CompanyCodeQrVerification,
c.CompanyActive
from Company c inner join Province p on (c.ProvinceID = p.ProvinceID)
inner join City ct on (c.CityID = ct.CityID)
where (@CompanyID IS NULL or CompanyID = @CompanyID)
and (@CompanyActive IS NULL or CompanyActive = @CompanyActive)
order by CompanyName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;

exec sp_CompanyReadFull NULL,NULL,1,100
--*************************************************************************
/*
--NO EXISTE DEPARTAMENTO
create drop procedure sp_CompanyDepartmentRead(
@CompanyID int=NULL,
@CompanyActive tinyint=NULL,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
c.CompanyID,
c.CompanyRuc,
c.CompanyName,
c.CompanyActive,
d.DepartmentID,
d.DepartmentName,
d.DepartmentActive
from Company c inner join Department d on (c.CompanyID =  d.CompanyID)
where (@CompanyID IS NULL or c.CompanyID = @CompanyID)
and (@CompanyActive IS NULL or c.CompanyActive = @CompanyActive)
order by c.CompanyName, d.DepartmentName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_CompanyDepartmentRead '1000000000001', null,1,1
--*************************************************************************
/*
create procedure sp_CompanyCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Company);
end;
*/
/*
declare @valor bigint
exec sp_CompanyCountRead @valor out
select @valor
*/
--*************************************************************************
/*
create ALTER procedure sp_CompanyUpdate(
@CompanyID int,
@CompanyRuc varchar(13),
@ProvinceID int,
@CityID int,
@CompanyName varchar(200),
@CompanyAddress varchar(200),
@CompanyPhone varchar(100),
@CompanyActive tinyint 
)
as 
begin
update Company
set 
CompanyRuc=@CompanyRuc,
ProvinceID = @ProvinceID,
CityID=@CityID,
CompanyName=@CompanyName,
CompanyAddress=@CompanyAddress,
CompanyPhone=@CompanyPhone,
CompanyActive=@CompanyActive
where CompanyID = @CompanyID;
end;
*/
/*
exec sp_CompanyUpdate
1,
'1000000000001',
10,
4,
'INFOMERCA S. A.222',
'Chimborazo 418 y clemente ballen',
'04233555',
1; 
*/

--**************************************************
/*
create alter procedure sp_CompanyDelete(
@CompanyID int
)
as 
begin
delete
from Company
where  CompanyID = @CompanyID;
end;
*/
--exec sp_CompanyDelete 1

