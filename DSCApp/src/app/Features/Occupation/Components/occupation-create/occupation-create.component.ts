import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';
import { OccupationApiService } from 'src/app/Core/Services/occupation-api.service';
import { IOccupation } from 'src/app/Core/Models/Entity/IOccupation';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-occupation-create',
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './occupation-create.component.html',
  styleUrls: ['./occupation-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class OccupationCreateComponent implements OnInit{


  /********************************************************************
  variables*/
  //modal id
  private id:string="modal-create";
  /********************************************************************
  inject*/
  private occupationApiService = inject(OccupationApiService);
  private formBuilder = inject(FormBuilder);
  //modal injectar servicios
  private dscModalService = inject(DscModalService);
  /**********************************************************************
  variables*/
  public formGroup!:FormGroup;
  

  ngOnInit(): void {
    //inicializamos formulario
    this.formGroup = this.formBuilder.group({
    //OccupationID:[''],
    OccupationDescription:['',[Validators.compose([Validators.required])]],
    OccupationActive:[true,[Validators.compose([Validators.required])]]
    });
    //
    this.ReciveData();
    //this.OnChangeOccupationDescription();
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


  private OnOccupationCreate():void{
  let data:IOccupation = {
    OccupationID:this.GenerarID(this.OccupationDescriptionControl.value),
    OccupationDescription:this.OccupationDescriptionControl.value,
    OccupationActive:this.OccupationActiveControl.value
  };
  console.log(data);
  this.occupationApiService.OccupationCreate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        if(data.ErrorCodigo == 0){//State
          this.OnFormReset();
          this.closeModal();
          
        }
      }
    }
  );
}





//modal (cierra y envia data a origen)
public closeModal() {
  this.dscModalService.close(this.id,{data:'action-create'});
}

/****************************************************************************
metodos
****************************************************************************/

public OnSubmit(){
  if(this.formGroup.valid){
      this.OnOccupationCreate()
  }
}

//obtenemos el objeto de formulario en cada cambio
/*private OnChangeOccupationDescription(){
  this.OccupationDescriptionControl.valueChanges.subscribe((value)=>{
    let generarID = this.GenerarID(value);
    this.OccupationIDControl.setValue(generarID!=''?generarID:this.OccupationIDControl.value);
  });
  }*/

private OnFormReset():void{
  this.formGroup.reset();
  this.OccupationActiveControl.setValue(true);
}


private GenerarID2(msg:string):string{
  if(msg!=null){
  // Se puede crear un arreglo a partir de la cadena
let search = 'áéíóúñ :,;_=+*-/@$#!()&<>."'.split('');
// Solo tomé algunos caracteres, completa el arreglo
let replace = ['a','e','i','o','u','ni','-','','','','-','','','','-','','','','','','','','','','','',''];
  // Eliminar tildes
  //msg = msg.normalize('NFKD');
  // Convertir en minúsculas
  msg = msg.toLowerCase();
  // Recorrer todos los caracteres
  search.forEach((char, index) => {
    // Remplazar cada caracter en la cadena
    msg = msg.replaceAll(char, replace[index]);
});}else{
  msg = '';
}
return msg;
}

private GenerarID(msg:string):string{
  //if(msg!=null && msg.length <=5){
  // Se puede crear un arreglo a partir de la cadena
let search = 'áéíóúñ :,;_=+*-/@$#!()&<>."'.split('');
// Solo tomé algunos caracteres, completa el arreglo
let replace = ['a','e','i','o','u','ni','-','','','','-','','','','-','','','','','','','','','','','',''];
  // Eliminar tildes
  //msg = msg.normalize('NFKD');
  // Convertir en minúsculas
  msg = msg.toLowerCase();
  // Recorrer todos los caracteres
  search.forEach((char, index) => {
    // Remplazar cada caracter en la cadena
  msg = msg.replaceAll(char, replace[index]);
});
msg = msg.substring(0,5) + '-' + Math.floor(Math.random() * 1000);
//}else{
//  msg = '';
//}
return msg;
}

/******************************************************************
propiedades
******************************************************************/

/*public get OccupationIDControl():FormControl{
  return this.formGroup.get('OccupationID') as FormControl;
}*/
public get OccupationDescriptionControl():FormControl{
  return this.formGroup.get('OccupationDescription') as FormControl;
}
public get OccupationActiveControl():FormControl{
  return this.formGroup.get('OccupationActive') as FormControl;
}




}
