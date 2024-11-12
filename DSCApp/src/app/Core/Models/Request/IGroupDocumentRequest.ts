export interface IGroupDocumentRequest {
    GroupDocumentID?:number;
    CompanyID:string;
    GroupDocumentType:string;
    GroupDocumentDate?:Date;
    GroupDocumentDescription:string;
    GroupDocumentActive?:boolean;
}