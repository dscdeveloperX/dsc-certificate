/*
create procedure sp_DepartmentCreate(
@CompanyID varchar(13),
@DepartmentName varchar(200),
@DepartmentActive tinyint
)
as
begin
insert into Department(
CompanyID,
DepartmentName,
DepartmentActive
)values(
@CompanyID,
@DepartmentName,
@DepartmentActive
);
end;
*/
/*
exec sp_DepartmentCreate
'1000000000001',
'department-one',
1;

exec sp_DepartmentCreate
'1000000000001',
'department-dos',
1;
exec sp_DepartmentCreate
'2000000000001',
'department-B-uno',
1;

*/
--*******************************************************************
/*
create procedure sp_DepartmentRead(
@DepartmentID int=NULL,
@DepartmentActive tinyint=NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
DepartmentID,
CompanyID,
DepartmentName,
DepartmentActive
from Department
where (@DepartmentID IS NULL or DepartmentID = @DepartmentID) and
(@DepartmentActive IS NULL or DepartmentActive = @DepartmentActive)
order by DepartmentName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_DepartmentRead NULL,NULL,1,1
--***********************************************************
/*
create procedure sp_DepartmentCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Department);
end;
*/
/*
declare @valor bigint
exec sp_DepartmentCountRead @valor out
select @valor
*/
--***********************************************************
/*
create procedure sp_DepartmentUpdate(
@DepartmentID int,
@CompanyID varchar(13),
@DepartmentName varchar(200),
@DepartmentActive tinyint
)
as
begin
update Department
set CompanyID =@CompanyID,
DepartmentName=@DepartmentName,
DepartmentActive=@DepartmentActive
where DepartmentID = @DepartmentID;
end;
*/
/*
exec sp_DepartmentUpdate
1,
'1000000000001',
'department-one22222',
1;
*/

--**********************************************************************
/*
create procedure sp_DepartmentDelete(
@DepartmentID int
)
as
begin
delete from Department
where DepartmentID = @DepartmentID;
end;
*/
--exec sp_DepartmentDelete 1

/*
create table Department(
DepartmentID int identity(1,1) not null,
CompanyID varchar(13) not null,
DepartmentName varchar(200) not null,
DepartmentActive tinyint default 1
);
*/