
/*

create alter procedure sp_EmployeeCreate(
@CompanyID int,
@PersonID varchar(10),
@EmployeeDateEntry datetime,
@EmployeeDateExit datetime,
@EmployeeReason varchar(500),
@EmployeeActive tinyint )
as 
begin
insert into Employee(
CompanyID,
PersonID,
EmployeeDateEntry,
EmployeeDateExit,
EmployeeReason,
EmployeeActive) values (
@CompanyID,
@PersonID,
@EmployeeDateEntry,
@EmployeeDateExit,
@EmployeeReason,
@EmployeeActive
);
end;
*/

/*
exec sp_EmployeeCreate
1,
'0941573131',
'19670825',
null,
null,
1;
*/
--***********************************************
/*
create alter procedure sp_EmployeeRead(
@EmployeeID int = null,
@EmployeeActive tinyint = null ,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
EmployeeID,
CompanyID,
PersonID,
EmployeeDateEntry,
EmployeeDateExit,
EmployeeReason,
EmployeeActive
from Employee
where (@EmployeeID IS NULL or EmployeeID = @EmployeeID) and
(@EmployeeActive IS NULL or EmployeeActive = @EmployeeActive)
order by EmployeeID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY; select * from Employee
end;
*/
--exec sp_EmployeeRead null, null,1,1

--************************************************************************
create alter procedure sp_EmployeeRead(
@EmployeeID int = null,
@EmployeeActive tinyint = null ,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
e.EmployeeID,
e.CompanyID,
c.CompanyName,
e.PersonID,
p.PersonName,
p.PersonSurname,
e.EmployeeDateEntry,
e.EmployeeDateExit,
e.EmployeeReason,
e.EmployeeActive
from Employee e inner join Company c on (e.CompanyID =  c.CompanyID)
inner join Person p on (e.PersonID = p.PersonID)
where (@EmployeeID IS NULL or EmployeeID = @EmployeeID) and
(@EmployeeActive IS NULL or EmployeeActive = @EmployeeActive)
order by EmployeeID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY; select * from Employee
end;
*/
--exec sp_EmployeeRead null, null,1,1
--***********************************************************************
create alter procedure sp_EmployeeReadSearch(
@PersonName varchar(10)=null,
@CompanyID int = null,
--@EmployeeActive tinyint = null ,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
p.PersonPhoto,
e.EmployeeID,
e.CompanyID,
c.CompanyName,
e.PersonID,
p.PersonName,
p.PersonSurname,
e.EmployeeDateEntry,
e.EmployeeDateExit,
e.EmployeeReason,
e.EmployeeActive
from Employee e inner join Company c on (e.CompanyID =  c.CompanyID)
inner join Person p on (e.PersonID = p.PersonID)
where (@CompanyID IS NULL or e.CompanyID = @CompanyID) and
(@PersonName IS NULL or p.PersonName + ' ' + p.PersonSurname LIKE '%' + @PersonName + '%')
--(@EmployeeActive IS NULL or EmployeeActive = @EmployeeActive)
order by p.PersonName + ' ' + p.PersonSurname asc, e.EmployeeDateEntry desc
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;

--exec sp_EmployeeReadSearch '',1,1,100

--*************************************************************************
create procedure sp_EmployeePersonIDRead(
@CompanyID int = null
)
as 
begin
select 
e.PersonID
from Employee e
where e.CompanyID = @CompanyID and
e.EmployeeActive = 1;
end;

--exec sp_EmployeePersonIDRead 1


--*************************************************************************
create procedure sp_EmployeeRolPagoRead(
@CompanyID int = null
)
as 
begin
select 
e.EmployeeID,
e.CompanyID,
e.PersonID,
p.PersonName,
p.PersonSurname,
p.PersonSignatureImage,
p.PersonPhoto,
p.PersonEmail,
e.EmployeeDateEntry,
c.CompanyRuc,
c.CompanyName,
c.CompanyAddress,
c.CompanyPhone,
c.CompanyPhoto,
c.CompanyUrlVerification,
c.CompanyCodeQrVerification
from Employee e inner join Person p on (e.PersonID =  p.PersonID)
inner join Company c on (e.CompanyID =  c.CompanyID)
where e.CompanyID = @CompanyID and
e.EmployeeActive = 1;
end;

exec sp_EmployeeRolPagoRead  1


--***********************************************************************

--***********************************************************************
create alter procedure sp_EmployeeCountRead(
@PersonName varchar(10)=null,
@CompanyID int = null,
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) 
from Employee e inner join Company c on (e.CompanyID =  c.CompanyID)
inner join Person p on (e.PersonID = p.PersonID)
where (@CompanyID IS NULL or e.CompanyID = @CompanyID) and
(@PersonName IS NULL or p.PersonName + ' ' + p.PersonSurname LIKE '%' + @PersonName + '%')
);
end;

declare @valor bigint
exec sp_EmployeeCountRead 'rr',1, @valor out
select @valor
--***********************************************************************

create alter procedure sp_EmployeeUpdate(
@EmployeeID int,
@CompanyID int,
@PersonID varchar(10),
--@EmployeeDateEntry datetime,
@EmployeeDateExit datetime,
@EmployeeReason varchar(500),
@EmployeeActive tinyint)
as 
begin
declare @Active tinyint;
set @Active = 0;--por default 
if @EmployeeActive = 1 --si lo activa
begin
--pregunto si ya existe este empleado activado
exec sp_EmployeeActiveChecked @CompanyID,@PersonID, @Active out;
end;
if @Active = 0 
begin
update Employee
set 
--CompanyID = @CompanyID,
--PersonID = @PersonID,
--EmployeeDateEntry=@EmployeeDateEntry,
EmployeeDateExit=@EmployeeDateExit,
EmployeeReason =@EmployeeReason,
EmployeeActive=@EmployeeActive
where EmployeeID = @EmployeeID;
end;

end;

/*
exec sp_EmployeeUpdate
1,
1,
'0918723453',
'19770725',
'esta presona salio por hijo de su mama',
1;
*/

--***********************************************************************
/*
create alter procedure sp_EmployeeDelete(
@CompanyID int,
@EmployeeID int
)
as 
begin
delete from Employee
where CompanyID = @CompanyID and 
EmployeeID = @EmployeeID;
end;
*/

--exec sp_EmployeeDelete 1

--***************************************************
/*select * from Person
select * from Company
select * from Employee*/


--****************************************************************
--LISTA DE PERSONAS QUE PUEDEN SER EMPLEADOS EN CIERTA COMPANIA
create alter procedure sp_EmployeeActiveChecked(
@CompanyID int,
@PersonID varchar(10),
@Active int out
)
as
begin
set @Active = (
select count(1)
from Employee e
where e.CompanyID = @CompanyID and
e.PersonID = @PersonID and 
e.EmployeeActive = 1)
end;
declare @Active tinyint;
exec sp_EmployeeActiveChecked 1, '0918723453', @Active out
select @a
--0646546544
--0918723453