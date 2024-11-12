import { AfterContentInit, AfterViewInit, Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalComponent } from 'src/app/dsc/dsc-modal/dsc-modal.component';
import { EditComponent } from '../edit/edit.component';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';

@Component({
  selector: 'app-lista',
  standalone: true,
  imports: [CommonModule, EditComponent, DscModalComponent],
  templateUrl: './lista.component.html',
  styleUrls: ['./lista.component.css']
})
export class ListaComponent implements AfterViewInit {

  //modal id
  public id:string="modal-1";
  //modal injectar servicio
  private dscModalService = inject(DscModalService);


  //modal (recibe data de destino)
  ngAfterViewInit(): void {
    this.dscModalService.dataOut(this.id)?.subscribe(
      (x)=>{console.log(x);});
  }

//modal (abrir y enviar data a destino)
public openModal() {
    this.dscModalService.open(this.id,{nombre:'Darwin'});
}


}
