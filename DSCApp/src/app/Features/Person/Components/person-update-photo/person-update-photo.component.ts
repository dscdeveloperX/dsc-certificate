import { Component, inject,effect, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit, Input, SimpleChanges, OnChanges, Output, EventEmitter, DoCheck, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { PersonApiService } from 'src/app/Core/Services/person-api.service';
import { IPerson } from 'src/app/Core/Models/Entity/IPerson';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { urlHostBase } from 'src/app/Core/Constans/api-params';

@Component({
  selector: 'app-person-update-photo',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './person-update-photo.component.html',
  styleUrls: ['./person-update-photo.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class PersonUpdatePhotoComponent implements OnInit{
  
  /********************************************************************
  propiedades de entrada*/
  /********************************************************************
   eventos de salida*/

   @ViewChild('previewPhoto') previewPhoto!:ElementRef;
  
  /********************************************************************
  inject*/
  private formBuilder = inject(FormBuilder);
  private personApiService = inject(PersonApiService);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  private renderer2 = inject(Renderer2);
  /**********************************************************************
  campos*/
  public Data:WritableSignal<IPerson|undefined> = signal<IPerson|undefined>(undefined);
  public formGroup!:FormGroup;
  //modal id
  private id:string="modal-update-photo";
  private formData =  new FormData();
  public PhotoUrl:WritableSignal<string> = signal<string>('./assets/image/shared/photo-empty.jpg');
  

  
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      Photo:['',[Validators.compose([Validators.required])]]
    });
    //*******************************
    this.ReciveData();
    
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
  this.dscModalService.close(this.id,{data:'action-update-photo'});
  }

  private OnPersonUpdatePhoto():void{
    //el archivo de imagen ya es agregado en el momento de buscar la imagen
    this.formData.append('PersonPhoto',this.Data()!.PersonPhoto!);
    //
  this.personApiService.PersonUpdatePhoto(this.formData).subscribe(
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
  this.PhotoControl.reset();
  //foto por default
  this.renderer2.setAttribute(this.previewPhoto.nativeElement, 'src', `./assets/image/shared/photo-empty.jpg`);
  //limpiar canvas
  //this.ClearCanvas();
  this.ClearformData();
}

private ClearformData(){
  //vaciamos formdata
  this.formData.delete('PersonPhoto');
  this.formData.delete('Photo');
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


public InitializateFormulary(data:IPerson){
  this.Data.set(data);
  this.PhotoUrl.set(urlHostBase + this.Data()!.PersonPhoto);
  /*this.formGroup.patchValue(
    this.Data()!
  );*/
  
}

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnPersonUpdatePhoto()
  }
}



/******************************************************************
propiedades
******************************************************************/

public get PhotoControl():FormControl{
  return this.formGroup.get('Photo') as FormControl;
}

}