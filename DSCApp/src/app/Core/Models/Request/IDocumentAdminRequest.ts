export interface IDocumentAdminRequest {
            DocumentID:number;
            DocumentGroupID: number;
            DocumentType:string;
            PersonID:string;
            PersonName:string;
            PersonSurname:string;
            DocumentCode:string;
            DocumentEmailSend:string;
            DocumentEmailSendState:boolean;
            DocumentDateEmailSend?:Date;
            DocumentDateCreation:Date;            
}
