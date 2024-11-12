import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck, Renderer2, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { IPerson } from 'src/app/Core/Models/Entity/IPerson';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-person-update-signature',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './person-update-signature.component.html',
  styleUrls: ['./person-update-signature.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonUpdateSignatureComponent implements OnInit, AfterViewInit{
  
  /********************************************************************
  propiedades de entrada*/
  /********************************************************************
   eventos de salida*/
   @ViewChild('canvasSignature', {static: false}) canvasSignature!: ElementRef;
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private personApiService = inject(PersonApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  private renderer2 = inject(Renderer2);
  //canvas
  private CanvasContext!:any;
  private MouseDownCanvasFunc!: Function;
  private MouseUpCanvasFunc!: Function;
  private MouseMoveCanvasFunc!: Function;
  //
  private TouchStartCanvasFunc!: Function;
  private TouchEndCanvasFunc!: Function;
  private TouchMoveCanvasFunc!: Function;
  //
  private MouseLeaveCanvasFunc!: Function;
  //
  private initialX:number=0;
  private initialY:number=0;
  /**********************************************************************
  campos*/
  public Data:WritableSignal<IPerson|undefined> = signal<IPerson|undefined>(undefined);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update-signature";
  private formData =  new FormData();
  
  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      Signature:['',[Validators.compose([Validators.required])]]
    });
    //*******************************
    this.ReciveData();
  }


  //inicializa canvas
  ngAfterViewInit(): void {
    this.CanvasContext = this.canvasSignature.nativeElement.getContext('2d');
    //mousedown
    this.MouseDownCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'mousedown', (event) => {
      this.OnMouseDownCanvas(event);
    });
    //mouseup
    this.MouseUpCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'mouseup', (event) => {
      this.OnMouseUpCanvas(event);
    });
    //mouseleave
    this.MouseLeaveCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'mouseleave', (event) => {
      this.OnMouseLeaveCanvas(event);
    });


    //touchstart
    this.TouchStartCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'touchstart', (event) => {
      this.OnTouchStartCanvas(event);
    });
    //touchend
    this.TouchEndCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'touchend', (event) => {
      this.OnTouchEndCanvas(event);
    });
    

  }

  
  /***********************************************************************
  servicios
  ***********************************************************************/
  //modal (recibe data de origen)
  private ReciveData(){
    this.dscModalService.dataIn(this.id)?.subscribe(
    (x)=>{
      this.Data.set(x.data);
      this.InitializateFormulary(this.Data()!);
    }
    );
    }
  
//modal (cierra y envia data a origen)
  public closeModal() {
  this.dscModalService.close(this.id,{data:'action-update-signature'});
  }

  private async OnPersonUpdateSignature():Promise<void>{
    await this.FormDataAppendCanvas();
    //el archivo de imagen ya es agregado en el momento de buscar la imagen
    this.formData.append('PersonSignature',this.Data()!.PersonSignatureImage!);
    //
  this.personApiService.PersonUpdateSignature(this.formData).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo==0){//State
          this.OnFormReset();
          this.closeModal();
        }
      }
    }
  );
}



/****************************************************************************
metodos
****************************************************************************/

private OnFormReset():void{
  //this.formGroup.reset();
  this.SignatureControl.reset();
  this.ClearformData();
}


public async FormDataAppendCanvas():Promise<string>{
  return new Promise<string> ((resolve,reject)=> {
  
    let canvas = this.canvasSignature.nativeElement;
  canvas.toBlob((x:Blob)=>{
    //const b = new Blob([x],{type: 'image/png'});
    const file = new File([x], `Signature${Math.floor(Math.random() * 1000)}.png`,{type: 'image/png'});
    //const url = URL.createObjectURL(x);
    //this.renderer2.setAttribute(this.previewPhoto.nativeElement, 'src', url);
    this.formData.append('Signature', file);
    console.log(this.formData.get('Signature'));
    resolve('');
  });

    return '';});
  
}

private ClearformData(){
  //vaciamos formdata
  this.formData.delete('Signature');
  this.formData.delete('PersonSignature');
  this.ClearCanvas();
}

public ClearCanvas():void {
  this.CanvasContext.clearRect(0, 0, this.canvasSignature.nativeElement.width, this.canvasSignature.nativeElement.height);
  this.SignatureControl.setValue('');
}

public InitializateFormulary(data:IPerson){
  this.Data.set(data);
  /*this.formGroup.patchValue(
    this.Data()!
  );*/
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnPersonUpdateSignature()
  }
}


/****************************************************************************
CANVAS 
*****************************************************************************/
private ChangeDetectorRef = inject(ChangeDetectorRef);

public OnMouseDownCanvas(event:any){
  this.initialX = event.offsetX;
  this.initialY = event.offsetY;
  //cada vez que escribe validamos el formulario
  this.SignatureControl.setValue('-');
  this.ChangeDetectorRef.detectChanges();
  //dibujo
  this.Draw(this.initialX,this.initialY);
  //agrego evento mousemove
  this.MouseMoveCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'mousemove', (event) => {
    this.OnMouseMoveCanvas(event);
  });
}
public OnMouseUpCanvas(event:any){
  this.MouseMoveCanvasFunc();
}
public OnMouseLeaveCanvas(event:any){
  if(this.MouseMoveCanvasFunc){
  this.MouseMoveCanvasFunc();
  }

}
public OnMouseMoveCanvas(event:any){
  this.Draw(event.offsetX,event.offsetY);
}

//dibujar canvas
private Draw(x:number, y:number):void{
  this.CanvasContext.beginPath();
  this.CanvasContext.moveTo(this.initialX,this.initialY);
  this.CanvasContext.lineWidth = 5;
  this.CanvasContext.strokeStyle = "#ff0040";
  this.CanvasContext.lineCap = "round";
  this.CanvasContext.lineJoin = "round";
  this.CanvasContext.lineTo(x,y);
  this.CanvasContext.stroke();
  this.initialX = x;
  this.initialY = y;

}



public OnTouchStartCanvas(event:any){
  let rect = this.canvasSignature.nativeElement.getBoundingClientRect();
  this.initialX = (event.touches[0].clientX - rect.left);
  this.initialY = (event.touches[0].clientY - rect.top);
  //cada vez que escribe validamos el formulario
  this.SignatureControl.setValue('-');
  this.ChangeDetectorRef.detectChanges();
  //dibujo
  this.Draw(this.initialX,this.initialY);
  //agrego evento mousemove
  this.TouchMoveCanvasFunc = this.renderer2.listen(this.canvasSignature.nativeElement, 'touchmove', (event) => {
    this.OnTouchMoveCanvas(event);
  });
}
public OnTouchEndCanvas(event:any){
  this.TouchMoveCanvasFunc();

}
/*public OnMouseLeaveCanvas(event:any){
  if(this.MouseMoveCanvasFunc){
  this.MouseMoveCanvasFunc();
  }
}
*/
public OnTouchMoveCanvas(event:any){
  let rect = this.canvasSignature.nativeElement.getBoundingClientRect();
  this.Draw((event.touches[0].clientX - rect.left) ,(event.touches[0].clientY - rect.top));
}


/******************************************************************
propiedades
******************************************************************/

public get SignatureControl():FormControl{
  return this.formGroup.get('Signature') as FormControl;
}

}