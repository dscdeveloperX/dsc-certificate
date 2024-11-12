import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { IDocument } from 'src/app/Core/Models/Entity/IDocument';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-document-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './document-create.component.html',
  styleUrls: ['./document-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DocumentCreateComponent {
  private documentApiService = inject(DocumentApiService);

  public OnDocumentCreate():void{
  let data:IDocument = {
    GroupDocumentID:1,
    DocumentType:'cert-777',
    PersonID:'0000000001',
    DocumentDateCreation:new Date(1977,6,21),
    DocumentActive:true
  };
  this.documentApiService.DocumentCreate(data).subscribe(
    {
      next:(data:IDataResponse<IDocument>)=>{
        console.log(data);
      }
    }
  );
}
}