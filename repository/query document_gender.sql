/*
create procedure sp_GenderCreate(
	@GenderID varchar(200),
	@GenderDescription varchar(200),
	@GenderActive tinyint)
as
begin
insert into Gender(
GenderID,
GenderDescription,
GenderActive)values(
@GenderID,
@GenderDescription,
@GenderActive);
end;
*/
/*
insert into Gender (GenderID, GenderDescription,GenderActive) values ('M', 'Masculino', 1),
( 'F', 'Femino', 1);
*/
--*****************************************************************
/*
create procedure sp_GenderRead(
	@GenderID varchar(200) = NULL,
	@GenderActive tinyint=NULL,
	@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
GenderID,
GenderDescription,
GenderActive
from Gender
where (@GenderID IS NULL or GenderID = @GenderID)
and (@GenderActive IS NULL or GenderActive = @GenderActive)
order by GenderDescription
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_GenderRead NULL, NULL,1,1
--*************************************************************
/*
create procedure sp_GenderCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Gender);
end;
*/
/*
declare @valor bigint
exec sp_GenderCountRead @valor out
select @valor
*/
--*************************************************************
/*
create procedure sp_GenderUpdate(
	@GenderID varchar(200),
	@GenderDescription varchar(200),
	@GenderActive tinyint)
as
begin
update Gender
set GenderDescription = @GenderDescription,
GenderActive = @GenderActive
where GenderID =@GenderID;
end;
*/
--exec sp_GenderUpdate 'G', 'GGGG', 1

--***********************************************************
/*
create procedure sp_GenderDelete(
	@GenderID varchar(200))
as
begin
delete
from Gender
where GenderID = @GenderID;
end;
*/
--exec sp_GenderDelete 'G'
