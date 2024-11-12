import { Component, OnInit, inject,signal,effect, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from 'src/app/dsc/dsc-modal/dsc-modal.service';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
//modal id
private id:string="modal-1";
//modal injectar servicios
private dscModalService = inject(DscModalService);
  
//modal (recibe data de origen)
 ngOnInit(): void {
  this.dscModalService.dataIn(this.id)?.subscribe(
    (x)=>{console.log(x)}
 )
}
 
//modal (cierra y envia data a origen)
 public closeModal() {
     this.dscModalService.close(this.id,{codigo:'0918723453'});
 }

}
