export interface IDocumentGroupRequest {
    DocumentGroupID?:number;
    CompanyID:string;
    CompanyName:string;
    DocumentGroupType:string;
    DocumentGroupDate?:Date;
    DocumentGroupDescription:string;
    DocumentGroupActive?:boolean;
}