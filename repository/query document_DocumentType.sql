/*
create procedure sp_DocumentTypeCreate(
	@DocumentTypeID varchar(200),
	@DocumentTypeDescription varchar(200),
	@DocumentTypeActive tinyint)
as
begin
insert into DocumentType(
DocumentTypeID,
DocumentTypeDescription,
DocumentTypeActive)values(
@DocumentTypeID,
@DocumentTypeDescription,
@DocumentTypeActive);
end;
*/
/*
insert into DocumentType (DocumentTypeID, DocumentTypeDescription,DocumentTypeActive) 
values ('CERT-619', 'Rol de pago', 1)
exec sp_DocumentTypeCreate 'CERT-410', 'Horas extras', 1;
*/
--*****************************************************************

/*
create procedure sp_DocumentTypeRead(
	@DocumentTypeID varchar(200) = NULL,
	@DocumentTypeActive tinyint=NULL,
	@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select 
DocumentTypeID,
DocumentTypeDescription,
DocumentTypeActive
from DocumentType
where (@DocumentTypeID IS NULL or DocumentTypeID = @DocumentTypeID)
and (@DocumentTypeActive IS NULL or DocumentTypeActive = @DocumentTypeActive)
order by DocumentTypeDescription
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_DocumentTypeRead NULL, NULL,1,3
--*************************************************************
/*
create procedure sp_DocumentTypeCountRead(
@RowsCount bigint out
)
as
begin;
set @RowsCount = (select count(1) from DocumentType);
end;
*/
/*
declare @valor bigint
exec sp_DocumentTypeCountRead @valor out
select @valor
*/
--*************************************************************
/*
create procedure sp_DocumentTypeUpdate(
	@DocumentTypeID varchar(200),
	@DocumentTypeDescription varchar(200),
	@DocumentTypeActive tinyint)
as
begin
update DocumentType
set DocumentTypeDescription = @DocumentTypeDescription,
DocumentTypeActive = @DocumentTypeActive
where DocumentTypeID =@DocumentTypeID;
end;
*/
--exec sp_DocumentTypeUpdate 'CERT-777', 'XXXX', 1

--***********************************************************
/*
create procedure sp_DocumentTypeDelete(
	@DocumentTypeID varchar(200))
as
begin
delete
from DocumentType
where DocumentTypeID = @DocumentTypeID;
end;
*/
--exec sp_DocumentTypeDelete 'CERT-777'

select * from DocumentType