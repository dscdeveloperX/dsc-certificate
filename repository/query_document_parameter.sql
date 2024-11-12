
/*
create procedure sp_ParameterCreate(
@CompanyID varchar(13),
@ParameterName varchar(100),
@ParameterValue varchar(500),
@ParameterType varchar(100),
@ParameterActive tinyint
)
as
begin
insert into Parameter(
CompanyID,
ParameterName,
ParameterValue,
ParameterType,
ParameterActive
)values(
@CompanyID,
@ParameterName,
@ParameterValue,
@ParameterType,
@ParameterActive
);
end;
*/
/*exec sp_ParameterCreate
'000000000123',
'Divorciado',
'div',
'genero',
1
*/
--********************************************************************
/*
create alter procedure sp_ParameterRead(
@ParameterID int=null,
@ParameterActive tinyint=null,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
ParameterID,
CompanyID,
ParameterName,
ParameterValue,
ParameterType,
ParameterActive
from Parameter
where (@ParameterID IS NULL or ParameterID=@ParameterID) and
(@ParameterActive IS NULL or ParameterActive =@ParameterActive)
order by ParameterType,ParameterName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_ParameterRead NULL, NULL, 1, 2
--********************************************************************
/*
create procedure sp_ParameterCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Parameter);
end;
*/
/*
declare @valor bigint
exec sp_ParameterCountRead @valor out
select @valor
*/
--********************************************************************
/*
create alter procedure sp_ParameterCompanyRead(
@CompanyID varchar(13)=null,
@ParameterActive tinyint=null,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
ParameterID,
CompanyID,
ParameterName,
ParameterValue,
ParameterType,
ParameterActive
from Parameter
where (@CompanyID IS NULL or CompanyID=@CompanyID) and
(@ParameterActive IS NULL or ParameterActive =@ParameterActive)
order by ParameterType,ParameterName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_ParameterCompanyRead NULL, NULL,1, 3

--**********************************************************************
/*
create procedure sp_ParameterUpdate(
@ParameterID int,
@CompanyID varchar(13),
@ParameterName varchar(100),
@ParameterValue varchar(500),
@ParameterType varchar(100),
@ParameterActive tinyint
)
as
begin
update Parameter
set CompanyID=@CompanyID,
ParameterName = @ParameterName,
ParameterValue=@ParameterValue,
ParameterType=@ParameterType,
ParameterActive=@ParameterActive
where ParameterID= @ParameterID;
end;
*/
/*
exec sp_ParameterUpdate
1,
'000000000123',
'Divorciado',
'div',
'generoxxxxxx',
1
*/
--******************************************************************
/*
create procedure sp_ParameterDelete(
@ParameterID int
)
as
begin
delete from Parameter
where ParameterID= @ParameterID;
end;
*/
--exec sp_ParameterDelete 1


/*
create table Parameter(
ParameterID int identity(1,1),
CompanyID varchar(13),
ParameterName varchar(100),
ParameterValue varchar(500),
ParameterType varchar(100),
ParameterActive tinyint default 1
);
*/