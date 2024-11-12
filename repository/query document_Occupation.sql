/*
create procedure sp_OccupationCreate(
	@OccupationID varchar(200),
	@OccupationDescription varchar(200),
	@OccupationActive tinyint)
as
begin
insert into Occupation(
OccupationID,
OccupationDescription,
OccupationActive)values(
@OccupationID,
@OccupationDescription,
@OccupationActive);
end;
*/
/*
insert into Occupation (OccupationID, OccupationDescription,OccupationActive) values ('asistente-general', 'Asitente General', 1),
( 'senior-developer', 'Senior Developer', 1)
*/
--*****************************************************************
/*
create procedure sp_OccupationRead(
	@OccupationID varchar(200) = NULL,
	@OccupationActive tinyint=NULL,
	@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
OccupationID,
OccupationDescription,
OccupationActive
from Occupation
where (@OccupationID IS NULL or OccupationID = @OccupationID)
and (@OccupationActive IS NULL or OccupationActive = @OccupationActive)
order by OccupationDescription
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_OccupationRead NULL, NULL,1,3
--*************************************************************
/*
create procedure sp_OccupationCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Occupation);
end;
*/
/*
declare @valor bigint
exec sp_OccupationCountRead @valor out
select @valor
*/
--*************************************************************
/*
create procedure sp_OccupationUpdate(
	@OccupationID varchar(200),
	@OccupationDescription varchar(200),
	@OccupationActive tinyint)
as
begin
update Occupation
set OccupationDescription = @OccupationDescription,
OccupationActive = @OccupationActive
where OccupationID =@OccupationID;
end;
*/
--exec sp_OccupationUpdate 'senior-developer', 'PROGRAMADOR DE OFICINA', 1

--***********************************************************
/*
create procedure sp_OccupationDelete(
	@OccupationID varchar(200))
as
begin
delete
from Occupation
where OccupationID = @OccupationID;
end;
*/
--exec sp_OccupationDelete 'senior-developer'
