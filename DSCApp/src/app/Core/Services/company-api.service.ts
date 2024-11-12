import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ICompany } from '../Models/Entity/ICompany';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { ICompanyRequest } from '../Models/Request/ICompanyRequest';

@Injectable({
  providedIn: 'root'
})
export class CompanyApiService {

  private http = inject(HttpClient);
  constructor() { }

  public CompanyRead(companyID:any, companyActive:any, page:number, quantity:number ):Observable<IDataResponse<ICompany>>{
    return this.http.get<IDataResponse<ICompany>>(`${urlBase}/company/read?companyID=${companyID}&companyActive=${companyActive}&page=${page}&quantity=${quantity}`);
  }
  public CompanyReadFull(companyID:any, companyActive:any, page:number, quantity:number ):Observable<IDataResponse<ICompany>>{
    return this.http.get<IDataResponse<ICompany>>(`${urlBase}/company/read-full?companyID=${companyID}&companyActive=${companyActive}&page=${page}&quantity=${quantity}`);
  }
/*
  public CompanyDepartmentRead(companyID:any, companyActive:any, page:number, quantity:number ):Observable<IData>{
    return this.http.get<IData>(`${urlBase}/company/company-department-read?companyID=${companyID == null?'':companyID}&companyActive=${companyActive}&page=${page}&quantity=${quantity}`);
  }
*/
  public CompanyCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/company/count`);
  }



  public CompanyCreate(data:ICompanyRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/company/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CompanyUpdate(data:ICompanyRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/company/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CompanyDelete(companyID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/company/delete/${companyID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
