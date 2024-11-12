import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IPerson } from '../Models/Entity/IPerson';
import { IPersonEmployee } from '../Models/Entity/IPersonEmployee';
import { IPersonRequest } from '../Models/Request/IPersonRequest';


@Injectable({
  providedIn: 'root'
})
export class PersonApiService {

  private http = inject(HttpClient);
  constructor() { }

  public PersonRead(personID:any, personActive:any, page:number, quantity:number ):Observable<IDataResponse<IPerson>>{
    return this.http.get<IDataResponse<IPerson>>(`${urlBase}/person/read?personID=${personID == null?'':personID}&personActive=${personActive}&page=${page}&quantity=${quantity}`);
  }
  public PersonReadSearch(personName:any, page:number, quantity:number ):Observable<IDataResponse<IPerson>>{
    return this.http.get<IDataResponse<IPerson>>(`${urlBase}/person/read-search?personName=${personName == null?'':personName}&page=${page}&quantity=${quantity}`);
  }

  public PersonEmployeeRead(companyID:number):Observable<IDataResponse<IPersonEmployee>>{
    return this.http.get<IDataResponse<IPersonEmployee>>(`${urlBase}/person/person-employee-read?companyID=${companyID}`);
  }


  public PersonCount(personName:any):Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/person/count?personName=${personName == null?'':personName}`);
  }

  /*public PersonCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/person/count`);
  }*/

  /*
  public PersonCreate(data:IPerson):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/person/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }
  */
  public PersonCreate(data:any):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/person/create`, data);
    //,{headers:new HttpHeaders({'content-type':'multipart/form-data'}
  }

  public PersonUpdatePhoto(data:any):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/person/update-photo`, data);
  }

  public PersonUpdateSignature(data:any):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/person/update-signature`, data);
  }

  public PersonUpdate(data:IPersonRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/person/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public PersonDelete(personID:string):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/person/delete/${personID==null?'':personID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }


}
