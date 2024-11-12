/*
create procedure sp_RoleCreate(
@RoleID varchar(10),
@RoleName varchar(50),
@RoleActive tinyint
)
as
begin
insert into Role(
RoleID,
RoleName,
RoleActive
)values(
@RoleID,
@RoleName,
@RoleActive
);
end;
*/
/*exec sp_RoleCreate 'SADM',
'Super Administrador',
1
exec sp_RoleCreate 'ADM',
'Administrador',
1
exec sp_RoleCreate 'USER',
'Usuario',
1
*/
--******************************************************************
/*
create procedure sp_RoleRead(
@RoleID varchar(10) = NULL,
@RoleActive tinyint = NULL,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
RoleID,
RoleName,
RoleActive
from Role
Where (@RoleID IS NULL or RoleID = @RoleID)
and (@RoleActive IS NULL or RoleActive = @RoleActive)
order by RoleID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_RoleRead NULL, 1,1,1
--exec sp_RoleRead NULL, NULL,1,1

--******************************************************************
create procedure sp_RoleCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from Role);
end;

declare @valor bigint
exec sp_RoleCountRead @valor out
select @valor

--******************************************************************
/*
create procedure sp_RoleUpdate(
@RoleID varchar(10),
@RoleName varchar(50),
@RoleActive tinyint
)
as
begin
update Role
set RoleID = @RoleID,
RoleName = @RoleName,
RoleActive = @RoleActive 
where RoleID = @RoleID;
end;
*/
--exec sp_RoleUpdate 'ADM', 'Administrador', 1
--exec sp_RoleSelect '-',255

--******************************************************************
/*
create procedure sp_RoleDelete(
@RoleID varchar(10)
)
as
begin
delete from Role
where RoleID = @RoleID;
end;
*/
--exec sp_RoleDelete 'USER'