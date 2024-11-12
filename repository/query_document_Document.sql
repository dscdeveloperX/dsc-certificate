
select * from DocumentGroup
select * from document
/*-MES No 09 DEL 01 AL 30 DE SEPTIEMBRE DEL 2023
MES No 08 DEL 01 AL 31 DE AGOSTO DEL 2023*/
--FALTA DOCUMENT CODE
/*

create alter procedure sp_DocumentCreate(
@DocumentGroupID int,
@DocumentType varchar(200),
@PersonID varchar(10),
@DocumentXmlID int,
@DocumentEmailSend varchar(200),
@DocumentEmailSendState tinyint,
@DocumentDateEmailSend datetime,
@DocumentDateCreation datetime,
@DocumentActive tinyint
)
as
begin
insert into Document(
DocumentGroupID,
DocumentType,
PersonID,
DocumentXmlID,
DocumentEmailSend,
DocumentEmailSendState,
DocumentDateEmailSend,
DocumentDateCreation,
DocumentActive)
values(
@DocumentGroupID,
@DocumentType,
@PersonID,
@DocumentXmlID,
@DocumentEmailSend,
@DocumentEmailSendState,
@DocumentDateEmailSend,
@DocumentDateCreation,
@DocumentActive
);
end;
*/

select * from documentgroup
where DocumentGroupDate = '20230901'

create alter procedure sp_DocumentInsertXml(
   @xmlData XML
           )
		   AS
begin
insert into Document(
DocumentGroupID,
DocumentType,
PersonID,
DocumentCode,
DocumentEmailSend,
DocumentEmailSendState,
DocumentDateCreation,
DocumentActive)
SELECT
x.value('DocumentGroupID[1]','int') AS DocumentGroupID,
x.value('DocumentType[1]','varchar(200)') AS DocumentType,
x.value('PersonID[1]','varchar(10)') AS PersonID,
x.value('DocumentCode[1]','varchar(200)') AS DocumentCode,
x.value('DocumentEmailSend[1]','varchar(200)') AS DocumentEmailSend,
x.value('DocumentEmailSendState[1]','tinyint') AS DocumentEmailSendState,
x.value('DocumentDateCreation[1]','datetime') AS DocumentDateCreation,
x.value('DocumentActive[1]','tinyint') AS DocumentActive
FROM @xmlData.nodes('//documento') XmlData(x);
end;

--/******************************************************************


create alter procedure sp_DocumentSelectXml(
@xmlData XML
)
AS
begin
select 
d.PersonID, 
p.PersonName,
d.DocumentEmailSend,
p.PersonSurname, 
dg.DocumentGroupDescription, 
d.DocumentCode
from Document d inner join DocumentGroup dg on (d.DocumentGroupID = dg.DocumentGroupID)
inner join Person p on (d.PersonID = p.PersonID)
where d.DocumentID in(
SELECT
x.value('DocumentID[1]','int') AS DocumentID
FROM @xmlData.nodes('//item') XmlData(x)
)
end;


declare @xmlData5 xml = '<?xml version="1.0" standalone="yes"?><root><item><DocumentID>33</DocumentID></item><item><DocumentID>34</DocumentID></item></root>';
EXEC sp_DocumentSelectXml @xmlData5


create alter procedure sp_DocumentDeleteXml(
@xmlData XML,
@DocumentGroupID int
)
AS
begin
--elimino
delete from Document
where DocumentID in(
SELECT
x.value('DocumentID[1]','int') AS DocumentID
FROM @xmlData.nodes('//item') XmlData(x)
);
--si despues de la eliminacion no existen registro en el grupo tambien eliminar grupo
declare @docCount int; 
set @docCount = (
select count(1) 
from DocumentGroup dg inner join Document d on (dg.DocumentGroupID = d.DocumentGroupID)
where dg.DocumentGroupID = @DocumentGroupID);
if @docCount = 0
begin
delete
from DocumentGroup 
where DocumentGroupID = @DocumentGroupID;
end;
end;



create alter procedure sp_DocumentUpdateXml(
@xmlData XML
)
AS
begin
UPDATE Document
SET DocumentDateEmailSend = (T.Item.value('@DocumentDateEmailSend', 'datetime')),
DocumentEmailSendState = (T.Item.value('@DocumentEmailSendState', 'tinyint'))
FROM Document d join @xmlData.nodes('/root/item') as T(Item) on (d.DocumentID = T.Item.value('@DocumentID', 'int'));
end;

declare @xmlData1 xml = '<?xml version="1.0" standalone="yes"?><root><item DocumentID="10" DocumentDateEmailSend="20231015" DocumentEmailSendState="1" /></root>';

exec sp_DocumentUpdateXml @xmlData1

select * from document
/*
exec sp_DocumentCreate
1,
'CERT-269',
'0918723453',
'175',
'darwin@gmail.com',
1,
'19771025',
'19770721',
1
*/
--******************************************************************
/*
create alter procedure sp_DocumentRead(
@DocumentID int = null,
@DocumentActive tinyint = null,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
DocumentID,
DocumentGroupID,
DocumentType,
PersonID,
DocumentCode,
DocumentXmlID,
DocumentEmailSend,
DocumentEmailSendState,
DocumentDateEmailSend,
DocumentDateCreation,
DocumentActive
from Document
where (@DocumentID IS NULL OR DocumentID = @DocumentID) and
(@DocumentActive IS NULL OR DocumentActive = @DocumentActive)
order by DocumentID
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;
*/
--exec sp_DocumentRead null , null,1,1

--**********************************************************************
--******************************************************************

create alter procedure sp_DocumentAdminRead(
@DocumentGroupID int,
@Page int,
@Quantity int
)
as
begin
declare @start int;
set @start = (@Page-1) * @Quantity;
select
d.DocumentID,
d.DocumentGroupID,
d.DocumentType,
d.PersonID,
p.PersonName,
p.PersonSurname,
d.DocumentCode,
--DocumentXmlID,
d.DocumentEmailSend,
d.DocumentEmailSendState,
d.DocumentDateEmailSend,
d.DocumentDateCreation
--DocumentActive
from Document d inner join Person p on(d.PersonID = p.PersonID)
where d.DocumentGroupID = @DocumentGroupID
order by p.PersonSurname + ' ' + p.PersonName , d.DocumentDateCreation
OFFSET @Start ROWS
FETCH NEXT @Quantity ROWS ONLY;
end;

--exec sp_DocumentAdminRead 12,1,100

--**********************************************************************

--******************************************************************

create ALTER procedure sp_DocumentUserRead(
@CompanyID int,
@DocumentType varchar(200),
@DocumentGroupDateYear int
)
as
begin
select
d.DocumentID,
d.DocumentGroupID,
dg.DocumentGroupDescription,
dg.DocumentGroupDate,
d.DocumentType,
d.DocumentCode,
d.DocumentDateCreation
from Document d inner join DocumentGroup dg on(d.DocumentGroupID = dg.DocumentGroupID)
where d.DocumentActive = 1 and 
dg.CompanyID = @CompanyID and
d.DocumentType = @DocumentType and
DATEPART(YEAR, DocumentGroupDate) = @DocumentGroupDateYear
order by d.DocumentType, dg.DocumentGroupDate desc, d.DocumentCode desc
end;

--exec sp_DocumentUserRead 1, 'CERT-619', 2023

--**********************************************************************


create alter procedure sp_DocumentCountRead(
@DocumentGroupID int,
@RowsCount bigint out
)
as
begin;
set @RowsCount = (
select count(1) from Document
where DocumentGroupID = @DocumentGroupID
);
end;

/*
declare @valor bigint
exec sp_DocumentCountRead @valor out
select @valor
*/
--**********************************************************************
/*
create alter procedure sp_DocumentUpdate(
@DocumentID int,
@DocumentGroupID int,
@DocumentType varchar(200),
@PersonID varchar(10),
@DocumentXmlID int,
@DocumentEmailSend varchar(200),
@DocumentEmailSendState tinyint,
@DocumentDateEmailSend datetime,
@DocumentDateCreation datetime,
@DocumentActive tinyint
)
as
begin
update Document
set DocumentGroupID=@DocumentGroupID,
DocumentType=@DocumentType,
PersonID=@PersonID,
DocumentXmlID =@DocumentXmlID,
DocumentEmailSend=@DocumentEmailSend,
DocumentEmailSendState =@DocumentEmailSendState,
DocumentDateEmailSend=@DocumentDateEmailSend,
DocumentDateCreation=@DocumentDateCreation,
DocumentActive=@DocumentActive
where DocumentID = @DocumentID;
end;
*/
/*
exec sp_DocumentUpdate
1,
1,
'CERT-269',
'0918723453',
'175',
'darwin222@gmail.com',
1,
'19771025',
'19770721',
1
*/
--*************************************************************
/*
create procedure sp_DocumentDelete(
@DocumentID int
)
as
begin
delete from Document
where DocumentID = @DocumentID;
end;
*/
--exec sp_DocumentDelete 1

/*
create table Document(
DocumentID int identity(1,1) not null,
DocumentGroupID int,
DocumentType varchar(200),
DocumentCode as DocumentType + '.' + CONVERT(varchar(10),DocumentGroupID) + '_' + CONVERT(varchar(10),DocumentID),
PersonID varchar(10),
DocumentDateCreation datetime default getdate(),
DocumentActive tinyint default 1
);
*/