import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { IDepartment } from '../Models/Entity/IDepartment';
import { IDepartmentRequest } from '../Models/Request/IDepartmentRequest';
import { ICompany } from '../Models/Entity/ICompany';


@Injectable({
  providedIn: 'root'
})
export class DepartmentApiService {

  private http = inject(HttpClient);
  constructor() { }

  public DepartmentRead(departmentID:any, departmentActive:any, page:number, quantity:number ):Observable<IDataResponse<ICompany>>{
    return this.http.get<IDataResponse<ICompany>>(`${urlBase}/department/read?departmentID=${departmentID}&departmentActive=${departmentActive}&page=${page}&quantity=${quantity}`);
  }

  public DepartmentCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/department/count`);
  }

  public DepartmentCreate(data:IDepartmentRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/department/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DepartmentUpdate(data:IDepartmentRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/department/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DepartmentDelete(departmentID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/department/delete/${departmentID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
