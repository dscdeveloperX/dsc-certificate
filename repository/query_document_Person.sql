/*
create alter procedure sp_PersonCreate(
@PersonID varchar(10),
@PersonSignatureImage varchar(200),
@PersonPhoto varchar(200),
@PersonEmail varchar(200),
@ProvinceID int,
@CityID int,
@PersonName varchar(200),
@PersonSurname varchar(200),
@PersonDateOfBirth datetime,
@PersonPhone varchar(15),
@GenderID varchar(3),
@MaritalStatusID varchar(3),
@PersonActive tinyint
)
as
begin
insert into Person(
PersonID,
PersonSignatureImage,
PersonPhoto,
PersonEmail,
ProvinceID,
CityID,
PersonName,
PersonSurname,
PersonDateOfBirth,
PersonPhone,
GenderID,
MaritalStatusID,
PersonActive
)values(
@PersonID,
@PersonSignatureImage,
@PersonPhoto,
@PersonEmail,
@ProvinceID,
@CityID,
@PersonName,
@PersonSurname,
@PersonDateOfBirth,
@PersonPhone,
@GenderID,
@MaritalStatusID,
@PersonActive
);
end;
*/

/*exec sp_PersonCreate
'0918723453',
'foto.jpg',
'firma.png',
'darwin@gmail.com',
10,
4,
'wilson',
'Correa',
'19770721',
'0995454646',
'f',
'sol',
1
*/

--**********************************************************************
/*
create alter procedure sp_PersonRead(
@PersonID varchar(10)=NULL,
@PersonActive tinyint=NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
PersonID,
PersonSignatureImage,
PersonPhoto,
PersonEmail,
ProvinceID,
CityID,
PersonName,
PersonSurname,
PersonDateOfBirth,
PersonPhone,
GenderID,
MaritalStatusID,
PersonActive
from  Person
where (@PersonID IS NULL or PersonID = @PersonID) and
(@PersonActive IS NULL or PersonActive = @PersonActive)
order by PersonName + ' ' + PersonSurname
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_PersonRead NULL, NULL,1,1
--**********************************************************************
/*
create alter procedure sp_PersonRead(
@PersonID varchar(10)=NULL,
@PersonActive tinyint=NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
p.PersonID,
p.PersonPhoto,
p.PersonSignatureImage,
p.CityID,
c.CityName,
p.ProvinceID,
pv.ProvinceName,
p.PersonName,
p.PersonSurname,
p.PersonDateOfBirth,
p.PersonPhone,
p.PersonEmail,
p.GenderID,
g.GenderDescription,
p.MaritalStatusID,
ms.MaritalStatusDescription,
p.PersonActive
from  Person p inner join Province pv on (p.ProvinceID =  pv.ProvinceID)
inner join City c on (p.CityID =  c.CityID)
inner join Gender g on (p.GenderID =  g.GenderID)
inner join MaritalStatus ms on (p.MaritalStatusID =  ms.MaritalStatusID)
where (@PersonID IS NULL or p.PersonID = @PersonID) and
(@PersonActive IS NULL or p.PersonActive = @PersonActive)
order by p.PersonName + ' ' + p.PersonSurname
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_PersonRead NULL, NULL,1,1
--****************************************************************
create alter procedure sp_PersonReadSearch(
@PersonName varchar(10)=null,
--@PersonID varchar(10)=NULL,
--@PersonActive tinyint=NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
p.PersonID,
p.PersonPhoto,
p.PersonSignatureImage,
p.CityID,
c.CityName,
p.ProvinceID,
pv.ProvinceName,
p.PersonName,
p.PersonSurname,
p.PersonDateOfBirth,
p.PersonPhone,
p.PersonEmail,
p.GenderID,
g.GenderDescription,
p.MaritalStatusID,
ms.MaritalStatusDescription,
p.PersonActive
from  Person p inner join Province pv on (p.ProvinceID =  pv.ProvinceID)
inner join City c on (p.CityID =  c.CityID)
inner join Gender g on (p.GenderID =  g.GenderID)
inner join MaritalStatus ms on (p.MaritalStatusID =  ms.MaritalStatusID)
where (@PersonName IS NULL or p.PersonName + ' ' + p.PersonSurname LIKE '%' + @PersonName + '%') --and
--(@PersonID IS NULL or p.PersonID = @PersonID) and
--(@PersonActive IS NULL or p.PersonActive = @PersonActive)
order by p.PersonName + ' ' + p.PersonSurname
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;

exec sp_PersonReadSearch 'rodo',1,100


create alter procedure sp_PersonCountRead(
@PersonName varchar(10)=null,
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Person p
where (@PersonName IS NULL or p.PersonName + ' ' + p.PersonSurname LIKE '%' + @PersonName + '%')
);
end;

/*
declare @valor bigint
exec sp_PersonCountRead 'rodo' , @valor out
select @valor
*/
--****************************************************************

create alter procedure sp_PersonUpdate(
@PersonID varchar(10),
--@PersonSignatureImage varchar(200),
--@PersonPhoto varchar(200),
@ProvinceID int,
@CityID int,
@PersonName varchar(200),
@PersonSurname varchar(200),
@PersonEmail varchar(200),
@PersonDateOfBirth datetime,
@PersonPhone varchar(15),
@GenderID varchar(3),
@MaritalStatusID varchar(3),
@PersonActive tinyint
)
as
begin
update Person
set ProvinceID=@ProvinceID,
--PersonSignatureImage =@PersonSignatureImage,
--PersonPhoto = @PersonPhoto,
PersonEmail = @PersonEmail,
CityID=@CityID,
PersonName=@PersonName,
PersonSurname=@PersonSurname,
PersonDateOfBirth=@PersonDateOfBirth,
PersonPhone=@PersonPhone,
GenderID=@GenderID,
MaritalStatusID=@MaritalStatusID,
PersonActive=@PersonActive
where PersonID = @PersonID;
end;

/*
exec sp_PersonUpdate
'0918723453',
'foto2.jpg',
'firma2.png',
'darwin2@gmail.com',
10,
4,
'wilson 222',
'Correa 222',
'19770721',
'0995454646',
'm',
'div',
1
*/
--****************************************************************
/*
create procedure sp_PersonDelete(
@PersonID varchar(10)
)
as
begin
delete from Person
where PersonID = @PersonID;
end;
*/
--exec sp_PersonDelete '0918723453'


--****************************************************************
--LISTA DE PERSONAS QUE PUEDEN SER EMPLEADOS EN CIERTA COMPANIA
create alter procedure sp_PersonEmployeeRead(
@CompanyID int
)
as
begin
select p.PersonID, p.PersonPhoto, p.PersonName, p.PersonSurname 
from person p
where not p.PersonID in (select e.PersonID 
from Employee e
where e.CompanyID = @CompanyID and e.EmployeeActive = 1) and
P.PersonActive = 1
order by (p.PersonSurname + '' + p.PersonName);
end;

exec sp_PersonEmployeeRead 1


