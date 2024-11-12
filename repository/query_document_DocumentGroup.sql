
create alter procedure sp_DocumentGroupCreate(
@DocumentGroupID int out,
@CompanyID int,
@DocumentGroupType varchar(200),
@DocumentGroupDate datetime,
@DocumentGroupDescription varchar(500),
@DocumentGroupActive tinyint
)
as 
begin
insert into DocumentGroup(
CompanyID,
DocumentGroupType,
DocumentGroupDate,
DocumentGroupDescription,
DocumentGroupActive)values(
@CompanyID,
@DocumentGroupType,
@DocumentGroupDate,
@DocumentGroupDescription,
@DocumentGroupActive
);
set @DocumentGroupID = @@IDENTITY;
end;
select * from DocumentType
/*
DECLARE @DocumentGroupID int 
exec sp_DocumentGroupCreate
@DocumentGroupID out,
1,
'CER-219',
'20220321',
'Rol de agosto 2022 hasta julio 2020',
1
SELECT @DocumentGroupID
*/
--****************************************************************

create alter procedure sp_DocumentGroupCompanyRead(
@CompanyID int=NULL,
@DocumentGroupType varchar(200),
@DocumentGroupDateYear int,
@DocumentGroupActive tinyint=NULL,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
dg.DocumentGroupID,
dg.CompanyID,
c.CompanyName,
dg.DocumentGroupType,
dg.DocumentGroupDate,
dg.DocumentGroupDescription,
dg.DocumentGroupActive
from DocumentGroup dg inner join Company c on (dg.CompanyID = c.CompanyID)
where (@CompanyID IS NULL or dg.CompanyID = @CompanyID) AND
dg.DocumentGroupType = @DocumentGroupType and
DATEPART(YEAR,dg.DocumentGroupDate) = @DocumentGroupDateYear and
(@DocumentGroupActive IS NULL OR dg.DocumentGroupActive = @DocumentGroupActive)
order by dg.DocumentGroupID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;


--exec sp_DocumentGroupCompanyRead 1,'CERT-619',2023, null,1,1000
--**********************************************************************
--
--MES No 09 DEL 01 AL 30 DE SEPTIEMBRE DEL 2023
SELECT  *from DocumentGroup
SELECT * FROM Document
/*
create procedure sp_DocumentGroupRead(
@DocumentGroupID int = null,
@DocumentGroupActive tinyint=NULL,
@Page int,
@Quantity int
)
as 
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
DocumentGroupID,
CompanyID,
DocumentGroupType,
DocumentGroupDate,
DocumentGroupDescription,
DocumentGroupActive
from DocumentGroup
where (@DocumentGroupID IS NULL or DocumentGroupID = @DocumentGroupID) AND
(@DocumentGroupActive IS NULL OR DocumentGroupActive = @DocumentGroupActive)
order by DocumentGroupID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/

--exec sp_DocumentGroupRead null, null, 1, 1
--**********************************************************************
/*
create procedure sp_DocumentGroupCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from DocumentGroup);
end;
*/
/*
declare @valor bigint
exec sp_DocumentGroupCountRead @valor out
select @valor
*/
--***********************************************************************
/*
create procedure sp_DocumentGroupUpdate(
@DocumentGroupID int,
@CompanyID int,
@DocumentGroupType varchar(200),
@DocumentGroupDate datetime,
@DocumentGroupDescription varchar(500),
@DocumentGroupActive tinyint
)
as 
begin
update DocumentGroup
set CompanyID=@CompanyID,
DocumentGroupType=@DocumentGroupType,
DocumentGroupDate=@DocumentGroupDate,
DocumentGroupDescription=@DocumentGroupDescription,
DocumentGroupActive=@DocumentGroupActive
where DocumentGroupID = @DocumentGroupID;
end;
*/
/*
exec sp_DocumentGroupUpdate
1,
1,
'CER-XXX',
'19770721',
'Rol de agosto 2021 hasta julio 2023',
1
*/
--**********************************************************************
/*
create procedure sp_DocumentGroupDelete(
@DocumentGroupID int
)
as 
begin
delete from DocumentGroup
where DocumentGroupID = @DocumentGroupID;
end;
*/
--exec sp_DocumentGroupDelete 1
