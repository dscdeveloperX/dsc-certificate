import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IDocumentRequest } from '../Models/Request/IDocumentRequest';
import { IDataResponse } from '../Models/Response/IDataResponse';
import { urlBase } from '../Constans/api-params';
import { IDocument } from '../Models/Entity/IDocument';
import { IDocumentAdmin } from '../Models/Entity/IDocumentAdmin';
import { IDocumentUser } from '../Models/Entity/IDocumentUser';


@Injectable({
  providedIn: 'root'
})
export class DocumentApiService {
  private http = inject(HttpClient);
  constructor() { }

  public DocumentImageViewer(name:string):Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/document/view-image/?name=${name}`);
  }
//CERT-619.24_440

  public DocumentRead(documentID:any, documentActive:any, page:number, quantity:number ):Observable<IDataResponse<IDocument>>{
    return this.http.get<IDataResponse<IDocument>>(`${urlBase}/document/read?documentID=${documentID}&documentActive=${documentActive}&page=${page}&quantity=${quantity}`);
  }

  public DocumentCount(documentGroupID:number):Observable<IDataResponse<any>>{
    return this.http.get<IDataResponse<any>>(`${urlBase}/document/count/${documentGroupID}`);
  }



  public DocumentAdminRead(documentGroupID:any, page:number, quantity:number ):Observable<IDataResponse<IDocumentAdmin>>{
    return this.http.get<IDataResponse<IDocumentAdmin>>(`${urlBase}/document/document-admin-read?documentGroupID=${documentGroupID}&page=${page}&quantity=${quantity}`);
  }

  public DocumentUserRead(companyID:any, documentType:string, documentGroupDateYear:number ):Observable<IDataResponse<IDocumentUser>>{
    return this.http.get<IDataResponse<IDocumentUser>>(`${urlBase}/document/document-user-read?companyID=${companyID}&documentType=${documentType}&documentGroupDateYear=${documentGroupDateYear}`);
  }

  public DocumentCreate(data:IDocumentRequest):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/document/create`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentXmlGenerate(data:any):Observable<any>{
    return this.http.post<any>(`${urlBase}/document/xml-generate`, data);
  }

  public DocumentPdfGenerate(data:any):Observable<any>{
    return this.http.post<any>(`${urlBase}/document/pdf-generate`, data);
  }

  public DocumentUpdate(data:IDocumentRequest):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/document/update`, JSON.stringify(data),{headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentDelete(documentID:number):Observable<IDataResponse<any>>{
    return this.http.delete<IDataResponse<any>>(`${urlBase}/document/delete/${documentID}`, {headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentDeleteXml(data:any, documentGroupID:string):Observable<IDataResponse<any>>{
    return this.http.post<IDataResponse<any>>(`${urlBase}/document/delete-xml/${documentGroupID}`, JSON.stringify(data), {headers:new HttpHeaders({'content-type':'application/json'})});
  }

  public DocumentUpdateXml(data:any):Observable<IDataResponse<any>>{
    return this.http.put<IDataResponse<any>>(`${urlBase}/document/update-xml`, JSON.stringify(data), {headers:new HttpHeaders({'content-type':'application/json'})});
  }

}
