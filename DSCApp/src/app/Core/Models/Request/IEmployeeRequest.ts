export interface IEmployeeRequest {
    PersonPhoto?:string;
    EmployeeID?:number;
    CompanyID:number;
    CompanyName?:string;
    PersonID:string;
    PersonName?:string;
    PersonSurname?:string;
    EmployeeDateEntry:Date;
    EmployeeDateExit?:Date;
    EmployeeReason?:string;
    EmployeeActive?:boolean;
}
