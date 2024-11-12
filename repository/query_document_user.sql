
create or alter procedure sp_UserCreate(
@UserName varchar(20),
@RoleID varchar(10),
@UserAlias varchar(100),
@UserPassword varchar(500),
@UserRef varchar(13),
@UserActive tinyint = 1
)
as
begin
insert into [User](
UserName,
RoleID,
UserAlias,
UserPassword,
UserRef,
UserActive
)values(
@UserName,
@RoleID,
@UserAlias,
@UserPassword,
@UserRef,
@UserActive
);
end;
/*
exec sp_UserCreate 
'darwinrsc',
'SADM',
'Darwin Sanchez',
'S@n200420052012',
0,
1
*/
--***********************************************************

create or alter procedure sp_UserRead(
@UserID int=NULL,
@UserActive tinyint = NULL,
@Page int,
@Quantity int
)
as
begin;
declare @start int;
set @start = (@Page-1) * @Quantity;
select
UserID,
UserName,
RoleID,
UserAlias,
UserPassword,
UserRef,
UserActive
from [User]
where (@UserID IS NULL or UserID = @UserID)
and (@UserActive IS NULL or UserActive = @UserActive )
order by UserName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;

--exec sp_UserRead NULL,NULL,1,1
--*************************************************************

create or alter procedure sp_UserCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from [User]);
end;

/*
declare @valor bigint
exec sp_UserCountRead @valor out
select @valor
*/
--*************************************************************

CREATE OR ALTER PROCEDURE sp_UserLogin (
@UserName varchar(20),
@UserPassword varchar(500),
@Rol varchar(10) out
)   
as
BEGIN
select @Rol = RoleID 
from [User]
where UserName = @UserName and
UserPassword = @UserPassword and
UserActive = 1
order by RoleID
OFFSET 0 ROWS
FETCH FIRST 1 ROWS ONLY;
END;
--select * from [User]
declare @rol1 varchar(10)
exec sp_UserLogin 'darwinrsc','S@n200420052012', @rol1 out
select @rol1
--*************************************************************


--************************************************************

/*create or alter procedure sp_UserUpdate(
@UserID int,
@UserName varchar(20),
@RoleID varchar(10),
@UserAlias varchar(100),
@UserPassword varchar(16),
@UserRef varchar(13),
@UserActive tinyint = 1
)
as
begin;
update [User]
set
UserName=@UserName,
RoleID=@RoleID,
UserAlias=@UserAlias,
UserPassword=@UserPassword,
UserRef=@UserRef,
UserActive=@UserActive
where UserID = @UserID;
end;
*/
/*exec sp_UserUpdate
3,
'darwinrsc',
'SADM',
'Darwin Sanchez',
'123456',
1,
1
*/
--***********************************************************
/*
create procedure sp_UserDelete(
@UserID int
)
as
begin;
delete
from [User]
where UserID = @UserID;
end;
*/
--exec sp_UserDelete 0






create or alter proc [dbo].UserGeneratePassword
   @len int = 8,
   @min tinyint = 48,
   @range tinyint = 74,
   @exclude varchar(50) = '0:;<=>?@O[]`^\/',
   @output varchar(50) output
as
   declare @char char
   set @output = ''
   while @len > 0 begin
     select @char = char(round(rand() * @range + @min, 0))
     if charindex(@char, @exclude) = 0 begin
       set @output += @char
       set @len = @len - 1
     end

   end;

declare @newpwd varchar(20)
-- all values between ASCII code 48 - 122 excluding defaults
exec [dbo].UserGeneratePassword @range=42, @output=@newpwd out
select @newpwd

select * from Company
select * from Employee
select * from [User]

delete from [User] where UserID = 4
update [User] 
set UserActive = 0
where UserID = 4;

--set @cedulaRuc = '1000000000001';
--set @userPassword2 = 'teamodsc19773';--es generada desde el back y es recibida como parametro de entrada

CREATE or alter Procedure sp_UserAutoGenerate(
@UserPassword varchar(500) = '12345678',
@CedulaRuc varchar(13)
)
as
begin
--activa o crea un nuevo usuario con un nuevo password
--crear empleado
--preguntar si existe usuario con cedula de empleado
declare @UserRolID varchar(10);--in
declare @UserAlias varchar(100);
--asignamos un rol dependiente del username ingresado
if LEN(@CedulaRuc) > 10
begin
SET @UserRolID = 'ADM'
--sabemos si es usuario asignamos este alias
set @UserAlias = (SELECT SUBSTRING(CompanyName,1,200) FROM Company WHERE CompanyRuc = @CedulaRuc and CompanyActive = 1);
end
else
begin
SET @UserRolID = 'USER'
--sabemos si es usuario asignamos este alias
set @UserAlias = (SELECT SUBSTRING( p.PersonSurname + ' ' +  p.PersonName,1,200) FROM  Employee e inner join Person p on (e.PersonID = p.PersonID) where e.EmployeeID = CAST(@CedulaRuc as int) and PersonActive = 1);
end


if (select count(1) from [User] where UserRef = @CedulaRuc) = 1
begin
--ya existe usuario
--si usuairo esta desactivado
if (select count(1) from [User] where UserRef = @CedulaRuc and UserActive = 0) = 1
begin
--activo usuario
update [User]
set UserActive = 1,
UserPassword = @UserPassword
where UserName = @CedulaRuc;
end
end
else
begin
--creo un usuario
exec sp_UserCreate 
@CedulaRuc,
@UserRolID,
@UserAlias,
@UserPassword,
@CedulaRuc,
1;
end
end;
