export interface ICompanyDepartmentRequest {
    CompanyID:string;
    CompanyName:string;
    CompanyActive?:boolean;
    DepartmentID?:number;
    DepartmentName:string;
    DepartmentActive?:boolean;
}
