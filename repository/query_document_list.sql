/*
create procedure ListCreate(
@ListType varchar(100),
@ListName varchar(200),
@ListValue varchar(200),
@ListActive tinyint
)
as
begin
insert into List(
ListType,
ListName,
ListValue,
ListActive
)values(
@ListType,
@ListName,
@ListValue,
@ListActive
);
end;
*/
/*
exec ListCreate
'genero',
'masculino',
'M',
1
*/
--******************************************************
/*
create procedure ListRead(
@ListID int = NULL,
@ListActive tinyint = NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
ListID,
ListType,
ListName,
ListValue,
ListActive
from List
where (@ListID is NULL or ListID = @ListID)
and (@ListActive is NULL or ListActive = @ListActive)
order by ListType,ListName
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec ListRead NULL, NULL,1,1
--************************************************************
create procedure sp_ListCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from List);
end;

declare @valor bigint
exec sp_ListCountRead @valor out
select @valor
--************************************************************
/*create procedure ListUpdate(
@ListID int,
@ListType varchar(100),
@ListName varchar(200),
@ListValue varchar(200),
@ListActive tinyint
)
as
begin
update List
set ListType=@ListType,
ListName=@ListName,
ListValue=@ListValue,
ListActive=@ListActive
where ListID =@ListID;
end;
*/
/*
exec ListUpdate
1,
'generoxxxx',
'masculino',
'M',
1
*/
--****************************************************************
/*
create procedure ListDelete(
@ListID int
)
as
begin
delete from List
where ListID =@ListID;
end;
*/
--exec ListDelete 1

create table List(
ListID int identity(1,1),
ListType varchar(100),
ListName varchar(200),
ListValue varchar(200),
ListActive tinyint default 1
);
