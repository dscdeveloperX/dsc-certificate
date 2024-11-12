import { Component, ChangeDetectorRef, ChangeDetectionStrategy, Renderer2, OnInit, ViewChild, inject, WritableSignal, signal, ElementRef, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscCalendarComponent } from 'src/app/dsc/dsc-calendar/dsc-calendar.component';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { ProvinceApiService } from 'src/app/Core/Services/province-api.service';
import { GenderApiService } from 'src/app/Core/Services/gender-api.service';
import { MaritalStatusApiService } from 'src/app/Core/Services/marital-status-api.service';
import { ICity } from 'src/app/Core/Models/Entity/ICity';
import { IProvinceCity } from 'src/app/Core/Models/Entity/IProvinceCity';
import { IGender } from 'src/app/Core/Models/Entity/IGender';
import { IMaritalStatus } from 'src/app/Core/Models/Entity/IMaritalStatus';
import { DateToString } from 'src/app/Core/Constans/convert';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-person-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule,DscCalendarComponent],
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonCreateComponent  implements OnInit, AfterViewInit {

  
  /********************************************************************
  viewchild*/
  @ViewChild('PersonDateOfBirth') PersonDateOfBirth!:DscCalendarComponent;
  @ViewChild('previewPhoto') previewPhoto!:ElementRef;
  @ViewChild('canvasSignature', {static: false}) canvasSignature!: ElementRef;
  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  private formData =  new FormData();
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
  /********************************************************************
  inject*/
  private personApiService = inject(PersonApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  private provinceApiService = inject(ProvinceApiService);
  private maritalStatusApiService = inject(MaritalStatusApiService);
  private genderApiServicee = inject(GenderApiService);
  private renderer2 = inject(Renderer2);
  /**********************************************************************
  variables*/
  public DataCity:WritableSignal<ICity[]> = signal<ICity[]>([]);
  public DataProvince:WritableSignal<IProvinceCity[]> = signal<IProvinceCity[]>([]);
  public DataGender:WritableSignal<IGender[]> = signal<IGender[]>([]);
  public DataMaritalStatus:WritableSignal<IMaritalStatus[]> = signal<IMaritalStatus[]>([]);
  
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    PersonID:['',[Validators.compose([Validators.required])]],
    Photo:['',[Validators.compose([Validators.required])]],
    Signature:['',[Validators.compose([Validators.required])]],
    PersonEmail:['',[Validators.compose([Validators.required])]],
    CityID:['',[Validators.compose([Validators.required])]],
    ProvinceID:['',[Validators.compose([Validators.required])]],
    PersonName:['',[Validators.compose([Validators.required])]],
    PersonSurname:['',[Validators.compose([Validators.required])]],
    PersonDateOfBirth:['',[Validators.compose([Validators.required])]],
    MaritalStatusID:['',[Validators.compose([Validators.required])]],
    PersonPhone:['',Validators.compose([Validators.required])],
    GenderID:['',[Validators.compose([Validators.required])]],
    PersonActive:[true,[Validators.compose([Validators.required])]]
    });
    //establecemos fecha inicial
    this.PersonDateOfBirthControl.setValue(DateToString(new Date()));
    //cargamos lista de companias
    //this.OnCompanyRead();
    //
    this.ReciveData();
    this.OnChangeProvince();
    this.OnMaritalStatusRead();
    this.OnGenderRead();
    this.OnProvinceRead();
    this.OnChangeProvince();
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
  (x)=>{}
  );
  }

  public OnGenderRead():void{
    this.genderApiServicee.GenderRead(null, true,1,100).subscribe(
      {
        next:(data:IDataResponse<IGender>)=>{
          this.DataGender?.set(data.Data);
        }
      }
    );
  }

  public OnProvinceRead():void{
    this.provinceApiService.ProvinceRead(null, null,1,1000).subscribe(
      {
        next:(data:IDataResponse<IProvinceCity>)=>{
          this.DataProvince?.set(data.Data);
          console.log(this.DataCity());
        }
      }
    );
  }

  public OnCityRead(provinceID:number):void{
    
    this.provinceApiService.ProvinceCityRead(provinceID, true,1,1000).subscribe(
      {
        next:(data:IDataResponse<ICity>)=>{
          this.DataCity?.set(data.Data);
        }
      }
    );
  }

  public OnMaritalStatusRead():void{
    this.maritalStatusApiService.MaritalStatusRead(null, true,1,100).subscribe(
      {
        next:(data:IDataResponse<IMaritalStatus>)=>{
          this.DataMaritalStatus?.set(data.Data);
        }
      }
    );
  }


 

/*
  public OnPersonCreate():void{
    let data:IPerson= {
      PersonID:this.PersonIDControl.value,
      Photo:this.PhotoControl.value,
      Signature:this.SignatureControl.value,
      CityID:this.CityIDControl.value,
      ProvinceID:this.ProvinceIDControl.value,
      PersonName:this.PersonNameControl.value,
      PersonSurname:this.PersonSurnameControl.value,
      PersonDateOfBirth:this.PersonDateOfBirthControl.value,
      MaritalStatusID:this.MaritalStatusIDControl.value,
      GenderID:this.GenderIDControl.value,
      PersonActive:this.PersonActiveControl.value
    };
    this.personApiService.PersonCreate(data).subscribe(
      {
        next:(data:IData)=>{
          if(data.State){
            this.OnFormReset();
            this.closeModal();
            
          }
      }
    }
    );
  }
*/
public async OnPersonCreate():Promise<void>{
  await this.FormDataAppendCanvas();
   this.formData.append('PersonID',this.PersonIDControl.value);
   this.formData.append('PersonEmail',this.PersonEmailControl.value);
   this.formData.append('CityID',this.CityIDControl.value);
   this.formData.append('ProvinceID',this.ProvinceIDControl.value);
   this.formData.append('PersonName',this.PersonNameControl.value);
   this.formData.append('PersonSurname',this.PersonSurnameControl.value);
   this.formData.append('PersonDateOfBirth',this.PersonDateOfBirthControl.value);
   this.formData.append('MaritalStatusID',this.MaritalStatusIDControl.value);
   this.formData.append('GenderID',this.GenderIDControl.value);
   this.formData.append('PersonPhone',this.PersonPhoneControl.value);
   this.formData.append('PersonActive',this.PersonActiveControl.value);
  
  this.personApiService.PersonCreate(this.formData).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
        if(data.ErrorCodigo == 0){//State
          
          this.OnFormReset();
          this.closeModal();
          
        }
    }
  }
  );
}

  //******************************************** */


  

//modal (cierra y envia data a origen)
public closeModal() {
  this.dscModalService.close(this.id,{data:'action-create'});
}



/****************************************************************************
metodos
****************************************************************************/

//obtenemos el objeto de formulario en cada cambio
private OnChangeProvince():void{
  this.ProvinceIDControl.valueChanges.subscribe((value)=>{
    /*console.log('valueChanges');
    console.log(value );*/
    this.OnCityRead(value);
  });
  }

private ClearformData(){
  //vaciamos formdata
  this.formData.delete('PersonID');
  this.formData.delete('PersonEmail');
  this.formData.delete('CityID');
  this.formData.delete('ProvinceID');
  this.formData.delete('PersonName');
  this.formData.delete('PersonSurname');
  this.formData.delete('PersonSurname');
  this.formData.delete('PersonDateOfBirth');
  this.formData.delete('MaritalStatusID');
  this.formData.delete('GenderID');
  this.formData.delete('PersonActive');
  this.formData.delete('Photo');
  this.formData.delete('Signature');
}

public ClearCanvas():void {
  this.CanvasContext.clearRect(0, 0, this.canvasSignature.nativeElement.width, this.canvasSignature.nativeElement.height);
  this.SignatureControl.setValue('');
}

public async capturarFilePhoto(event:any):Promise<any>{
  //archivo capturado
  const archivoCapturado = event.target.files[0];
  this.formData.append('Photo',archivoCapturado);
  //guardamos imagen en localstorage
  //this.cargarImage(archivoCapturado);
  await this.convertPhotoToBase64(archivoCapturado);
}

private async cargarImage(image:File){
  //this.renderer2.setAttribute(this.previsualizacionImagen.nativeElement, 'src',await this.convertToBase64(image));
}

private convertPhotoToBase64(file:File):Promise<string>{
    return new Promise<string> ((resolve,reject)=> {
         const reader = new FileReader();
         reader.readAsDataURL(file);
         reader.onload = () => {
          //localStorage.setItem('logo',reader.result?.toString() || '');
          //this.OnGetProductLogoCustom(categoryID, productID);
          //return '';//resolve(localStorage.getItem('logo')!.toString());
          //console.log(reader.result?.toString());
          this.renderer2.setAttribute(this.previewPhoto.nativeElement, 'src', reader.result?.toString() || '');
          resolve('');
          return '';
        };
         reader.onerror = error => reject(error);
     });
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

public OnModalPersonDateOfBirthOpen(){
  this.PersonDateOfBirth.Open();
}

public OnPersonDateOfBirth(data:string){
  this.PersonDateOfBirthControl.setValue(data);
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnPersonCreate()
  }
}

private OnFormReset():void{
  //this.formGroup.reset();
  this.PersonIDControl.reset();
  this.PersonEmailControl.reset();
  this.CityIDControl.reset();
  this.ProvinceIDControl.reset();
  this.PersonNameControl.reset();
  this.PersonSurnameControl.reset();
  this.GenderIDControl.reset();
  this.MaritalStatusIDControl.reset();
  this.PersonPhoneControl.reset();
  //establecemos fecha inicial
  this.PersonDateOfBirthControl.setValue(DateToString(new Date()));
  //foto por default
  this.renderer2.setAttribute(this.previewPhoto.nativeElement, 'src', `./assets/image/shared/photo-empty.j
  pg`);
  //limpiar canvas
  this.ClearCanvas();
  this.ClearformData();
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

/*public get CompanyIDControl():FormControl{
  return this.formGroup.get('CompanyID') as FormControl;
}*/
public get PersonIDControl():FormControl{
  return this.formGroup.get('PersonID') as FormControl;
}
public get PhotoControl():FormControl{
  return this.formGroup.get('Photo') as FormControl;
}
public get SignatureControl():FormControl{
  return this.formGroup.get('Signature') as FormControl;
}
public get PersonEmailControl():FormControl{
  return this.formGroup.get('PersonEmail') as FormControl;
}
public get CityIDControl():FormControl{
  return this.formGroup.get('CityID') as FormControl;
}

public get ProvinceIDControl():FormControl{
  return this.formGroup.get('ProvinceID') as FormControl;
}
public get PersonNameControl():FormControl{
  return this.formGroup.get('PersonName') as FormControl;
}
public get PersonSurnameControl():FormControl{
  return this.formGroup.get('PersonSurname') as FormControl;
}
public get PersonDateOfBirthControl():FormControl{
  return this.formGroup.get('PersonDateOfBirth') as FormControl;
}
public get MaritalStatusIDControl():FormControl{
  return this.formGroup.get('MaritalStatusID') as FormControl;
}
public get PersonPhoneControl():FormControl{
  return this.formGroup.get('PersonPhone') as FormControl;
}
public get GenderIDControl():FormControl{
  return this.formGroup.get('GenderID') as FormControl;
}
public get PersonActiveControl():FormControl{
  return this.formGroup.get('PersonActive') as FormControl;
}
  
  
}

