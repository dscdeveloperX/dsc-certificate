--CREATE DATABASE certificate_db
--USE certificate_db
/*
create table Role(
RoleID varchar(10) not null,
RoleName varchar(50) unique,
RoleActive tinyint default 1
);
alter table Role
add constraint pk_Role primary key (RoleID);
*/
select * from [User]

create table [User](
UserID int identity(1,1) not null,
UserName varchar(20) unique,
RoleID varchar(10) not null,
UserAlias varchar(100),
UserPassword varchar(500),
UserEmployeeRef int null,
--UserCompanyRef varchar(13) null,
UserActive tinyint default 1
);
alter table [User]
add constraint pk_User primary key (UserID);
alter table [User]
add constraint fk_UserRole foreign key (RoleID) references Role(RoleID);

--********************************************************



/*
CREATE TABLE Province(
	ProvinceID int identity(1,1) NOT NULL,
	ProvinceName varchar(50) NULL,
	ProvinceActive tinyint default 1 NULL
);
alter table Province
add constraint pk_Province primary key (ProvinceID)
*/
select * from City
select * from province
/*
CREATE TABLE City(
	CityID int identity(1,1) NOT NULL,
	ProvinceID int NULL,
	CityName varchar(50) NULL,
	CityActive tinyint default 1 NULL
);
alter table City
add constraint pk_City primary key (CityID);
alter table City
add constraint fk_CityProvince foreign key (CityID) references City(CityID)
*/


create table Company(
CompanyID int identity(1,1) not null,
CompanyRuc varchar(13) not null,
ProvinceID int,
CityID int,
CompanyName varchar(200),
CompanyAddress varchar(200),
CompanyPhone varchar(100),
CompanyPhoto varchar(200),
CompanyUrlVerification varchar(200),
CompanyCodeQrVerification varchar(200),
CompanyActive tinyint default 1
);
alter table Company
add constraint pk_Company primary key (CompanyID);
alter table Company
add constraint fk_CompanyProvince foreign key (ProvinceID) references Province(ProvinceID);
alter table Company
add constraint fk_CompanyCity foreign key (CityID) references City(CityID);

alter table Company
add CompanyPhoto varchar(200),
CompanyUrlVerification varchar(200),
CompanyCodeQrVerification varchar(200)

/*
--SE ELIMINA
create table Department(
DepartmentID int identity(1,1) not null,
CompanyID varchar(13) not null,
DepartmentName varchar(200) not null,
DepartmentActive tinyint default 1
);
alter table Department
add constraint pk_Department primary key (DepartmentID);
alter table Department
add constraint fk_DepartmentCompany foreign key (CompanyID) references Company(CompanyID);
*/


select * from Person
/*
create table Person(
PersonID varchar(10),
PersonSignatureImage varchar(200),
PersonPhoto varchar(200),
ProvinceID int,
CityID int,
PersonName varchar(200),
PersonSurname varchar(200),
GenderID varchar(3),
MaritalStatusID varchar(3),
PersonDateOfBirth datetime,
PersonPhone varchar(15),
PersonEmail varchar(200),
PersonActive tinyint default 0
);
alter table Person
add constraint pk_Person primary key (PersonID);
alter table Person
add constraint fk_PersonProvince foreign key (ProvinceID) references Province(ProvinceID);
alter table Person
add constraint fk_PersonCity foreign key (CityID) references City(CityID);
*/

/*
//ELIMINAR ELIMINAR
create table CompanyPerson(
CompanyPersonID int identity(1,1) not null,
CompanyID varchar(13) not null,
PersonID varchar(10) not null,
PersonActive tinyint default 1
);
alter table CompanyPerson
add constraint pk_CompanyPerson primary key (CompanyPersonID);
alter table CompanyPerson
add constraint fk_CompanyPersonCompany foreign key (CompanyID) references Company(CompanyID);
alter table CompanyPerson
add constraint fk_CompanyPersonPerson foreign key (PersonID) references Person(PersonID);
*/

/*
create table Employee(
EmployeeID int identity(1,1) not null,
CompanyID int,
PersonID varchar(10),
EmployeeDateEntry datetime,
EmployeeDateExit datetime,
EmployeeReason varchar(500),
EmployeeActive tinyint default 1
);
alter table Employee
add constraint pk_Employee primary key (EmployeeID);
alter table Employee
add constraint fk_EmployeeCompanyPerson foreign key (EmployeeID) references CompanyPerson(CompanyPersonID);
*/

/*
create table DocumentGroup(
DocumentGroupID int identity(1,1) not null,
CompanyID int,
DocumentGroupType varchar(200),
DocumentGroupDate datetime,
DocumentGroupDescription varchar(500),
DocumentGroupActive tinyint default 1
);
alter table DocumentGroup
add constraint pk_DocumentGroupID primary key (DocumentGroupID);
alter table DocumentGroup
add constraint fk_DocumentGroupCompany foreign key (CompanyID) references Company(CompanyID);
*/

/*
create table Document(
DocumentID int identity(1,1) not null,
DocumentGroupID int,
DocumentType varchar(200),
PersonID varchar(10),
DocumentXmlID int,
DocumentCode varchar(200),
DocumentEmailSend varchar(200),
DocumentEmailSendState tinyint,
DocumentDateEmailSend datetime,
DocumentDateCreation datetime default getdate(),
DocumentActive tinyint default 1
);
alter table Document
add constraint pk_DocumentID primary key (DocumentID);
alter table Document
add constraint fk_DocumentDocumentGroup foreign key (DocumentGroupID) references DocumentGroup(DocumentGroupID);
alter table Document
add constraint fk_DocumentPerson foreign key (PersonID) references Person(PersonID);
*/
/*
create table Parameter(
ParameterID int identity(1,1),
CompanyID varchar(13),
ParameterName varchar(100),
ParameterValue varchar(500),
ParameterType varchar(100),
ParameterActive tinyint default 1
);
alter table Parameter
add constraint pk_Parameter primary key (ParameterID);
alter table Parameter
add constraint fk_ParameterCompany foreign key (CompanyID) references Company(CompanyID);
*/

--*******************************************************************************************
create table Gender(
GenderID varchar(200) not null,
GenderDescription varchar(200),
GenderActive tinyint default 1
);
alter table Gender
add constraint pk_GenderID primary key (GenderID);
--********************************************************************************************
create table MaritalStatus(
MaritalStatusID varchar(200) not null,
MaritalStatusDescription varchar(200),
MaritalStatusActive tinyint default 1
);
alter table MaritalStatus
add constraint pk_MaritalStatusID primary key (MaritalStatusID);
--********************************************************************************************
create table Occupation(
OccupationID varchar(200) not null,
OccupationDescription varchar(200),
OccupationActive tinyint default 1
);
alter table Occupation
add constraint pk_OccupationID primary key (OccupationID);
--********************************************************************************************
create table DocumentType(
DocumentTypeID varchar(200) not null,
DocumentTypeDescription varchar(200),
DocumentTypeActive tinyint default 1
);
alter table DocumentType
add constraint pk_DocumentTypeID primary key (DocumentTypeID);



/*
CREATE TABLE Usuario (
  UsuarioID int NOT NULL,
  UsuarioNombre varchar(100) NOT NULL,
  UsuarioClave varchar(500) NOT NULL,
  UsuarioAlias varchar(100) NOT NULL,
  UsuarioRol varchar(10) NOT NULL,
  UsuarioToken varchar(4000) NOT NULL,
  UsuarioHabilitar tinyint DEFAULT NULL
);
alter table Usuario
add constraint pk_UsuarioID primary key(UsuarioID);
*/




/*xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
create table List(
ListID int identity(1,1),
ListType varchar(100),
ListName varchar(200),
ListValue varchar(200),
ListActive tinyint default 1
);
alter table List
add constraint pk_List primary key (ListID);
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx*/
