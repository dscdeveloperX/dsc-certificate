import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IGroupDocument } from '../Models/Entity/IGroupDocument';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IGroupDocumentRequest } from '../Models/Request/IGroupDocumentRequest';


@Injectable({
  providedIn: 'root'
})
export class GroupDocumentApiService {

  private http = inject(HttpClient);
  constructor() { }

  public GroupDocumentRead(groupDocumentID:any, groupDocumentActive:any, page:number, quantity:number ):Observable<IDataResponse<IGroupDocument>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/group-document/read?groupDocumentID=${groupDocumentID}&groupDocumentActive=${groupDocumentActive}&page=${page}&quantity=${quantity}`);
  }

  public GroupDocumentCompanyRead(companyID:any, groupDocumentActive:any, page:number, quantity:number ):Observable<IDataResponse<IGroupDocument>>{
    return this.http.get<IDataResponse<IGroupDocument>>(`${urlBase}/group-document/group-document-company-read?companyID=${companyID == null?'':companyID}&groupDocumentActive=${groupDocumentActive}&page=${page}&quantity=${quantity}`);
  }

  public GroupDocumentCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/group-document/count`);
  }


  public GroupDocumentCreate(data:IGroupDocumentRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/group-document/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public GroupDocumentUpdate(data:IGroupDocumentRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/group-document/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public GroupDocumentDelete(groupDocumentID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/group-document/delete/${groupDocumentID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
