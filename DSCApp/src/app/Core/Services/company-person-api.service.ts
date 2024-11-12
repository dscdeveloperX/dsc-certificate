import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { ICompanyPersonRequest } from '../Models/Request/ICompanyPersonRequest';
import { ICompanyPerson } from '../Models/Entity/ICompanyPerson';


@Injectable({
  providedIn: 'root'
})
export class CompanyPersonApiService {
  private http = inject(HttpClient);
  constructor() { }

  public CompanyPersonRead(companyPersonID:any, companyPersonActive:any):Observable<IDataResponse<ICompanyPerson>>{
    return this.http.get<IDataResponse<ICompanyPerson>>(`${urlBase}/company-person/read?companyPersonID=${companyPersonID}&companyPersonActive=${companyPersonActive}`);
  }

  
  public CompanyPersonCreate(data:ICompanyPersonRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/company-person/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CompanyPersonUpdate(data:ICompanyPersonRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/company-person/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public CompanyPersonDelete(companyPersonID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/company-person/delete/${companyPersonID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
