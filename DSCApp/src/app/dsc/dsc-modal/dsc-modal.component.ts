import { Component, ChangeDetectionStrategy, EventEmitter, OnDestroy, Input, WritableSignal, signal, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscModalService } from './dsc-modal.service';

@Component({
  selector: 'dsc-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dsc-modal.component.html',
  styleUrls: ['./dsc-modal.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DscModalComponent implements OnInit, OnDestroy {

  //propiedades de entrada y salida
  @Input({required:true})
  public Id:string='';
  @Input({required:true})
  public Title:string='';
  //
  public OnOff:WritableSignal<boolean>= signal<boolean>(false);
  //eventos de salida (sin @Output no forma parte del html)
  public DataIn:EventEmitter<any>=new EventEmitter<any>();
  public DataOut:EventEmitter<any>=new EventEmitter<any>();
  //injeccion de servicios
  private dscModalService = inject(DscModalService);


  ngOnInit(): void {
    this.dscModalService.add(this);
  }

  
  public open(data:any):void{
    this.OnOff.set(true);
    this.DataIn.emit(data);
  }
  
  public close(data:any):void{
    this.OnOff.set(false);
    this.DataOut.emit(data);
  }


  ngOnDestroy(): void {
    this.dscModalService.remove(this.Id);
  }



}
