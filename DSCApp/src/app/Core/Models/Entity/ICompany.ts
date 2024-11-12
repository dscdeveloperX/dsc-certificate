export interface ICompany {
    CompanyID?:number;
    CompanyRuc:string;
    ProvinceID:number;
    ProvinceName?:string;
    CityID:number;
    CityName?:string;
    CompanyName:string;
    CompanyAddress:string;
    CompanyPhone:string;
    CompanyActive?:boolean;
}
