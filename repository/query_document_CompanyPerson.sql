/*
create procedure sp_CompanyPersonCreate(
@CompanyID varchar(13),
@PersonID varchar(10),
@PersonActive tinyint
)
as 
begin 
insert into CompanyPerson( 
CompanyID ,
PersonID ,
PersonActive)values(
@CompanyID ,
@PersonID ,
@PersonActive
);
end;
*/
/*
exec sp_CompanyPersonCreate
'0918723453001',
'0918723453',
1
*/
--******************************************************************
/*
create procedure sp_CompanyPersonRead(
@CompanyID varchar(13)=NULL,
@PersonActive tinyint=NULL
)
as 
begin 
select  
CompanyPersonID,
CompanyID,
PersonID ,
PersonActive 
from CompanyPerson;
end;
*/
--exec sp_CompanyPersonRead null, null
--**********************************************************************
/*
create procedure sp_CompanyPersonUpdate(
@CompanyPersonID int,
@CompanyID varchar(13),
@PersonID varchar(10),
@PersonActive tinyint
)
as 
begin 
update CompanyPerson
set CompanyID=@CompanyID,
PersonID=@PersonID,
PersonActive=@PersonActive
where CompanyPersonID=@CompanyPersonID;
end;
*/
/*
exec sp_CompanyPersonUpdate
1,
'0918723453001',
'0918723000',
1
*/
--*****************************************************************
/*
create procedure sp_CompanyPersonDelete(
@CompanyPersonID int
)
as 
begin 
delete from CompanyPerson
where CompanyPersonID=@CompanyPersonID;
end;
*/
--exec sp_CompanyPersonDelete 1
select * from Company
select * from Person
/*
create table CompanyPerson(
CompanyPersonID int identity(1,1) not null,
CompanyID varchar(13) not null,
PersonID varchar(10) not null,
PersonActive tinyint default 1
);
*/