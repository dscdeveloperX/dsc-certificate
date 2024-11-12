/*
create procedure sp_MaritalStatusCreate(
	@MaritalStatusID varchar(200),
	@MaritalStatusDescription varchar(200),
	@MaritalStatusActive tinyint)
as
begin
insert into MaritalStatus(
MaritalStatusID,
MaritalStatusDescription,
MaritalStatusActive)values(
@MaritalStatusID,
@MaritalStatusDescription,
@MaritalStatusActive);
end;
*/
/*
insert into MaritalStatus (MaritalStatusID, MaritalStatusDescription,MaritalStatusActive) values ('SOL', 'Soltero', 1),
( 'CAS', 'Casado', 1),
( 'DIV', 'Divorciado', 1),
( 'VIU', 'Viudo', 1),
( 'UDH', 'Unión de Hecho', 1);
*/
--*****************************************************************
/*
create procedure sp_MaritalStatusRead(
	@MaritalStatusID varchar(200) = NULL,
	@MaritalStatusActive tinyint=NULL,
	@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
MaritalStatusID,
MaritalStatusDescription,
MaritalStatusActive
from MaritalStatus
where (@MaritalStatusID IS NULL or MaritalStatusID = @MaritalStatusID)
and (@MaritalStatusActive IS NULL or MaritalStatusActive = @MaritalStatusActive)
order by MaritalStatusDescription
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_MaritalStatusRead NULL, NULL,1,3
--*************************************************************
/*
create procedure sp_MaritalStatusCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from MaritalStatus);
end;
*/
/*
declare @valor bigint
exec sp_MaritalStatusCountRead @valor out
select @valor
*/
--*************************************************************
/*
create procedure sp_MaritalStatusUpdate(
	@MaritalStatusID varchar(200),
	@MaritalStatusDescription varchar(200),
	@MaritalStatusActive tinyint)
as
begin
update MaritalStatus
set MaritalStatusDescription = @MaritalStatusDescription,
MaritalStatusActive = @MaritalStatusActive
where MaritalStatusID =@MaritalStatusID;
end;
*/
--exec sp_MaritalStatusUpdate 'SOL', 'Soltero', 1

--***********************************************************
/*
create procedure sp_MaritalStatusDelete(
	@MaritalStatusID varchar(200))
as
begin
delete
from MaritalStatus
where MaritalStatusID = @MaritalStatusID;
end;
*/
--exec sp_MaritalStatusDelete 'G'
