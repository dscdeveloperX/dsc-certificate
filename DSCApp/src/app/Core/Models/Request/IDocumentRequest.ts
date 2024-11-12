export interface IDocumentRequest {
    DocumentID?:number;
    GroupDocumentID:number;
    DocumentType:string;
    DocumentCode?:string;
    PersonID:string;
    DocumentDateCreation?:Date;
    DocumentActive?:boolean;
}
