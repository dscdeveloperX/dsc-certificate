import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { urlBase } from '../Constans/api-params';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { IDocumentType } from '../Models/Entity/IDocumentType';
import { IDocumentTypeRequest } from '../Models/Request/IDocumentTypeRequest';

@Injectable({
  providedIn: 'root'
})
export class DocumentTypeApiService {
  private http = inject(HttpClient);
  constructor() { }

  public DocumentTypeRead(documentTypeID:any, documentTypeActive:any, page:number, quantity:number ):Observable<IDataResponse<IDocumentType>>{
    return this.http.get<IDataResponse<IDocumentType>>(`${urlBase}/document-type/read?documentTypeID=${documentTypeID== null?'':documentTypeID}&documentTypeActive=${documentTypeActive}&page=${page}&quantity=${quantity}`);
  }

  public DocumentTypeCount():Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/document-type/count`);
  }


  public DocumentTypeCreate(data:IDocumentTypeRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/document-type/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentTypeUpdate(data:IDocumentTypeRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/document-type/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentTypeDelete(documentTypeID:string):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/document-type/delete/${documentTypeID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }



}
