import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IEmployeeRequest } from '../Models/Request/IEmployeeRequest';
import { IEmployee } from '../Models/Entity/IEmployee';


@Injectable({
  providedIn: 'root'
})
export class EmployeeApiService {

  private http = inject(HttpClient);
  constructor() { }

  public EmployeeRead(employeeID:any, employeeActive:any, page:number, quantity:number ):Observable<IDataResponse<IEmployee>>{
    return this.http.get<IDataResponse<IEmployee>>(`${urlBase}/employee/read?employeeID=${employeeID}&employeeActive=${employeeActive}&page=${page}&quantity=${quantity}`);
  }

  public EmployeeReadSearch(personName:string, companyID:number, page:number, quantity:number ):Observable<IDataResponse<IEmployee>>{
    return this.http.get<IDataResponse<IEmployee>>(`${urlBase}/employee/read-search?personName=${personName}&companyID=${companyID}&page=${page}&quantity=${quantity}`);
  }

  public EmployeeCount(personName:string,companyID:number):Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/employee/count?personName=${personName}&companyID=${companyID}`);
  }

  public EmployeeCreate(data:IEmployeeRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/employee/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public EmployeeUpdate(data:IEmployeeRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/employee/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public EmployeeDelete(companyID:number, employeeID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/employee/delete/${companyID}/${employeeID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
