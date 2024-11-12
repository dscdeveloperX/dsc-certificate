import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { saveAs } from 'file-saver';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-xml-generate-panel',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './xml-generate-panel.component.html',
  styleUrls: ['./xml-generate-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class XmlGeneratePanelComponent implements OnInit{
  
  /********************************************************************
  propiedades de entrada*/
  /********************************************************************
   eventos de salida*/
   
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private documentApiService = inject(DocumentApiService);
  private documentTypeApiService = inject(DocumentTypeApiService);
  /**********************************************************************
  campos*/
  public formGroup!:FormGroup;
  //modal id
  private formData =  new FormData();
  //
  public DataDocumentType:WritableSignal<IDocumentType[]> = signal<IDocumentType[]>([]);
  public Message:WritableSignal<string> = signal<string>('');
  public Procesando:WritableSignal<boolean> = signal<boolean>(false);

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      DocumentFile:['',[Validators.compose([Validators.required])]],
      DocumentType:['',[Validators.compose([Validators.required])]]
    });
    this.OnDocumentTypeRead();
  }

  
  /***********************************************************************
  servicios
  ***********************************************************************/
  private OnDocumentTypeRead(){
    this.documentTypeApiService.DocumentTypeRead(null,null,1,1000).subscribe({
      next:(data:IDataResponse<IDocumentType>)=>{
        this.DataDocumentType.set(data.Data);
      }
    });
  }

  private OnDocumenGenerate():void{
    this.Message.set("");
    //procesando
    this.Procesando.set(true);
    //agrego parametro type
  this.formData.append("DocumentType",this.DocumentTypeControl.value);
  //
  this.documentApiService.DocumentXmlGenerate(this.formData).subscribe(
    {
      next:(data:any)=>{
            var file = new Blob([data], {type: 'text/xml; charset=utf-8'});
            //npm install --save file-saver
            //para abrir en otra pestania
            //var fileURL = URL.createObjectURL(file);
            //window.open(fileURL);
            //alert(this.DocumentFileControl.value);
            switch(this.DocumentTypeControl.value){
              case "CERT-619":
                saveAs(file, "RolDePago.xml");
                break;
              case "CERT-410":
                //documentName= "HorasExtras.xml";
                break;
            }
            this.OnFormReset();
            this.Procesando.set(false);
            this.Message.set("Archivo procesado con éxito");
      },
      complete:()=>{
        this.Procesando.set(false);
      },
      error:(e)=>{
        this.OnFormReset();
        this.Procesando.set(false);
        this.Message.set(e.Message);
      }
    }
  );
}



/****************************************************************************
metodos
****************************************************************************/

private OnFormReset():void{
  //this.formGroup.reset();
  this.DocumentFileControl.reset();
  //foto por default
  //limpiar canvas
  //this.ClearCanvas();
  this.ClearformData();
}

private ClearformData(){
  //vaciamos formdata
  this.formData.delete('DocumentFile');
  this.formData.delete('DocumentType');
}

public capturarFile(event:any){
  //archivo capturado
  const archivoCapturado = event.target.files[0];
  this.formData.append('DocumentFile',archivoCapturado);
}





public OnSubmit(){
  if(this.formGroup.valid){
      this.OnDocumenGenerate()
  }
}



/******************************************************************
propiedades
******************************************************************/

public get DocumentFileControl():FormControl{
  return this.formGroup.get('DocumentFile') as FormControl;
}
public get DocumentTypeControl():FormControl{
  return this.formGroup.get('DocumentType') as FormControl;
}

}


