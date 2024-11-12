import { Component, ComponentRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DscCalendarComponent } from 'src/app/dsc/dsc-calendar/dsc-calendar.component';
import {DateToString} from '../../../app/global/convert'

@Component({
  selector: 'app-calendario',
  standalone: true,
  imports: [CommonModule, DscCalendarComponent],
  templateUrl: './calendario.component.html',
  styleUrls: ['./calendario.component.css']
})
export class CalendarioComponent {

  @ViewChild('calendar1') calendar1!:DscCalendarComponent;
  
  public fecha1:string= DateToString(new Date(2000,0,10));
  public fecha2:string= DateToString(new Date());
  //public modal!:number;

  
  public OnModalOpen(){
    this.calendar1.Open();
  }

  public OnDateFecha1(data:string){
    this.fecha1 =data;
  }
  public OnDateFecha2(data:string){
    this.fecha2 =data;
  }

}
