import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { saveAs } from 'file-saver';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DocumentApiService } from 'src/app/Core/Services/document-api.service';
import { IDocumentType } from 'src/app/Core/Models/Entity/IDocumentType';
import { DocumentTypeApiService } from 'src/app/Core/Services/document-type-api.service';
import { IMonth } from 'src/app/Core/Models/Entity/IMonth';
import { MonthsOfYear, YearRange } from 'src/app/Core/Constans/utility';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-pdf-generate-panel',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './pdf-generate-panel.component.html',
  styleUrls: ['./pdf-generate-panel.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PdfGeneratePanelComponent implements OnInit{
  
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
  public MessageValidation:WritableSignal<string[]> = signal<string[]>([]);
  //
  public documentYearData:string[]=[];
  public documentMonthData:IMonth[]=[];
  public documentYear?:string;
  public documentMonth?:string;
  private companyID:number=1;

  constructor(){
    this.documentYear = new Date().getFullYear().toString();
    this.documentMonth = (new Date().getMonth() + 1).toString().padStart(2,'0');
  }
  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      DocumentFile:['',[Validators.compose([Validators.required])]],
      DocumentType:['',[Validators.compose([Validators.required])]],
      DocumentGroupDescription:['',[Validators.compose([Validators.required])]],
      DocumentGroupDate:['',[Validators.compose([Validators.required])]]
    });
    this.OnDocumentTypeRead();
    this.GetDocumentPeriod();
    this.documentYearData = YearRange(0,50);
    this.documentMonthData = MonthsOfYear();//asignamos meses
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
  this.formData.append("CompanyID",this.companyID.toString());
  this.formData.append("DocumentGroupDescription",this.DocumentGroupDescriptionControl.value);
  this.formData.append("DocumentGroupDate",this.DocumentGroupDateControl.value);
  

  //

  this.documentApiService.DocumentPdfGenerate(this.formData).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
              //error y mensaje
              this.Procesando.set(false);
              this.Message.set(data.ErrorMessage);
              this.MessageValidation.set(data.Data);//string[]
            this.OnFormReset();
      },
      complete:()=>{
        this.Procesando.set(false);
      },
      error:(e)=>{
        this.Procesando.set(false);
        this.Message.set(e.Message);
      }
    }
  );
}



/****************************************************************************
metodos
****************************************************************************/

public OnChangeYear(event:any):void{
  this.documentYear = event.target.value
  this.GetDocumentPeriod();
}
public OnChangeMonth(event:any):void{
  this.documentMonth = event.target.value
  this.GetDocumentPeriod();
}

private GetDocumentPeriod():void{
  this.DocumentGroupDateControl.setValue(`${this.documentYear}-${this.documentMonth}-01`);
}

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
  this.formData.delete('CompanyID');
  this.formData.delete('DocumentFile');
  this.formData.delete('DocumentType');
  this.formData.delete('DocumentGroupDescription');
  this.formData.delete('DocumentGroupDate');
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
public get DocumentGroupDescriptionControl():FormControl{
  return this.formGroup.get('DocumentGroupDescription') as FormControl;
}
public get DocumentGroupDateControl():FormControl{
  return this.formGroup.get('DocumentGroupDate') as FormControl;
}



}
