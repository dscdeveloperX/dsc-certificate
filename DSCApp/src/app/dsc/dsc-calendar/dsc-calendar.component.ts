import { Component, ChangeDetectionStrategy, WritableSignal,signal, Output, EventEmitter, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IDscCalendar } from './idsc-calendar';

@Component({
  selector: 'dsc-calendar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dsc-calendar.component.html',
  styleUrls: ['./dsc-calendar.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DscCalendarComponent  implements OnInit, OnChanges {

  /********************************************************************
  propiedades de entrada*/
  @Input({required:true})
  public Date!:string;
  @Input({required:true})
  public IsModal!:boolean;
  /********************************************************************
  eventos de salida*/
  @Output() public Change:EventEmitter<string> = new EventEmitter<string>();
  /********************************************************************
  inject*/

  /**********************************************************************
  campos*/
  public dateMonthCalendar:WritableSignal<IDscCalendar[]> = signal<IDscCalendar[]>([]);
  public months:string[] = ["Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"];
  public calendarCurrentDate:WritableSignal<string> = signal<string>('');
  public Years:WritableSignal<number[]> = signal<number[]>([]);//almacena anios generados
  public modal:WritableSignal<boolean> = signal<boolean>(false);//abre o cierra popup
  public optionManipulate:string='day';//day|year|month (habilita la navegacion por anio, mes)
  public year!:number;//anio fecha global
  private yearSelect!:number;//anio momentaneo
  private monthSelect!:number;//mes momentaneo
  private dateInput!:Date;//fecha ingresada por el usuario en tipo date (antes de convertirla en string)
  private date:Date = new Date();//fecha global
  private month!:number;//mes fecha global
  private listYearIndex:number=0;
  

ngOnChanges(changes: SimpleChanges): void {
  if(changes['Date']){
  if(changes['Date'].currentValue != changes['Date'].previousValue){
      this.dateInput = this.StringToDate(this.Date);
      this.date = this.dateInput;
      this.year = this.date.getFullYear();
      this.month = this.date.getMonth();
      this.manipulate();
  }
}
if(changes['IsModal']){
if(changes['IsModal'].currentValue != changes['IsModal'].previousValue){
  //si esta propiedad es pasada
  if(changes['IsModal'].firstChange && changes['IsModal'].currentValue == false){
    this.modal.set(true);
  }
}
}


    
}

ngOnInit(): void {
  this.manipulate();
}

constructor(){
  this.year = this.date.getFullYear();
  this.month = this.date.getMonth();
}
  
/***********************************************************************
servicios
***********************************************************************/

/****************************************************************************
metodos
****************************************************************************/

//emitimos la fecha seleccionada 
public OnChangeDate(date:string, estado:string){
  if(estado!=='inactive'){
    if(this.IsModal){
      this.Close();
    }
    
    this.Change.emit(date);
  }
}

public Open(){
  this.modal.set(true);
}

public Close(){
  this.modal.set(false);
}

//permite movernos por los meses del calendario
public OnChangePrevNext(name:string){
  if(this.optionManipulate == 'day'){
          // Check if the icon is "calendar-prev"
        // or "calendar-next"
        this.month = name === "calendar-prev" ? this.month - 1 : this.month + 1;
 
        // Check if the month is out of range
        if (this.month < 0 || this.month > 11) {
 
            // Set the date to the first day of the
            // month with the new year
            this.date = new Date(this.year, this.month, new Date().getDate());
            
            // Set the year to the new year
            this.year = this.date.getFullYear();
 
            // Set the month to the new month
            this.month = this.date.getMonth();
        }
 
        else {
 
            // Set the date to the current date
            this.date = new Date();
        }
 
        // Call the manipulate function to
        // update the calendar display
        this.manipulate();

  }else if(this.optionManipulate == 'year'){
     // Check if the icon is "calendar-prev"
        // or "calendar-next"
        let yearMove = name === "calendar-prev" ?  - 1 : + 1;
        this.ListYear(this.year, yearMove);

  }
  
  
  
}

//permite indexar lista de dias calendario para evitar cambios de todos los dias al hacer select
public calendarID(index:number, item:any):number{
  return index;
}

  //genera la lista de 12 anios segun fecha actual
  private ListYear(yearCurrent:number, yearMove:number){
    //si es igual a 0 establece anio actual
    //si valor + 1 o - 1, subir o bajar index
    if(yearMove==0){
      this.listYearIndex = Math.ceil((yearCurrent-109)/12);
    }else{
      this.listYearIndex += yearMove;
    }
    
    let listYearInit:number = (109+(12*((this.listYearIndex)-1))+1);
    let listYearFinal:number = listYearInit + 11;
    let yearArray:number[]=[];
    for(let i=listYearInit;i<=listYearFinal;i++){
      yearArray.push(i);
    }
    this.Years.set(yearArray);
    this.calendarCurrentDate.set(`${listYearInit} - ${listYearFinal}`);
    
  }

  //convierte fecha de tipo string a date
  private StringToDate(date:string):Date{
    let dArray = date.split('-');
    return new Date(parseInt(dArray[0]), parseInt(dArray[1])-1,parseInt(dArray[2]));
  }

  //selecciona un mes
  public OnMonthSelect(month:number){
    this.monthSelect =  month;
    this.optionManipulate = 'day';
    this.date.setFullYear(this.yearSelect);
    this.date.setMonth(this.monthSelect);
    this.date.setDate(1);
    this.year = this.date.getFullYear();
    this.month = this.date.getMonth();
    this.manipulate();
  }

  //selecciona un anio
  public OnYearsSelect(year:number){
    this.yearSelect =  year;
    this.optionManipulate = 'month';
  }

  // genera el calendario
  public manipulate():void{
      if(this.optionManipulate=='day'){
        this.dateMonthCalendar.set([]);
      // Obtén el primer día del mes actual(y a la ves obtenemos el dia de la semana)
      //1 martes dia de la semana 2
      let dayone = new Date(this.year, this.month, 1).getDay();
      // Obtener la última fecha del mes actual
      // ultimo dia de agosto es 31
      let lastdate = new Date(this.year, this.month + 1, 0).getDate();
      let dayend_nextTime = (new Date(this.year, this.month + 1, 0).getTime()) + (24*60*60*1000);
      let dayend_newTime = new Date(dayend_nextTime);
      //obtenemos anio y mes siguiente
      let lastdate_year = dayend_newTime.getFullYear();
      let lastdate_month = dayend_newTime.getMonth();
      // Obtener el día de la última fecha del mes
      //31 jueves dia de la semana 4
      let dayend = new Date(this.year, this.month, lastdate).getDay();
      //Obtener la última fecha del mes anterior
      // ultimo dia de julio es 31
      let monthlastdate = new Date(this.year, this.month, 0).getDate();
      let monthlastdate_year = new Date(this.year, this.month, 0).getFullYear();
      let monthlastdate_month = new Date(this.year, this.month, 0).getMonth();
      
      // Variable para almacenar el HTML del calendario generado
      //let lit = "";
   
      // Bucle para agregar las últimas fechas del mes anterior
      for (let i = dayone; i > 0; i--) {
          this.dateMonthCalendar.mutate((x:IDscCalendar[])=>x.push({
            Class:'inactive',
            Date:this.DateToString(new Date(monthlastdate_year,monthlastdate_month,(monthlastdate - i + 1))),
            Day:(monthlastdate - i + 1).toString()
            
          }));
          //console.log((monthlastdate - i + 1));
          //lit +=`<li class="inactive">${monthlastdate - i + 1}</li>`;
      }
   
      // Bucle para agregar las fechas del mes actual

      for (let i = 1; i <= lastdate; i++) {
   
          // Compruebe si la fecha actual es hoy
          let isToday = i === this.dateInput.getDate()
              && this.month === this.dateInput.getMonth()
              && this.year === this.dateInput.getFullYear()
              ? "active"
              : "";
              this.dateMonthCalendar.mutate((x:IDscCalendar[])=>x.push({
                Class:isToday,
                Date:this.DateToString(new Date(this.year,this.month,i)),
                Day: i.toString()
              }));
              //lit += `<li class="${isToday}">${i}</li>`;
      }
   
      // Bucle para agregar las primeras fechas del mes siguiente
      for (let i = dayend; i < 6; i++) {
        this.dateMonthCalendar.mutate((x:IDscCalendar[])=>x.push(
          {
            Class:'inactive',
            Date:this.DateToString(new Date(lastdate_year,lastdate_month,(i - dayend + 1))),
            Day:(i - dayend + 1).toString()
          }));
          //lit += `<li class="inactive">${i - dayend + 1}</li>`
      }
   
      // Actualizar el texto del elemento de fecha actual
      // con el formato actual del mes y el año
      this.calendarCurrentDate.set(`${this.months[this.month]} - ${this.year}`);
      //this.renderer2.setProperty(this.currdate.nativeElement,'innerText',`${this.months[this.month]} ${this.year}`);
      // actualizar el código HTML del elemento dates
      // con el calendario generado
      //this.renderer2.setProperty(this.day.nativeElement,'innerHTML',lit);
    }
  }

  //convierte fecha de un tipo date a string
  private DateToString(date:Date):string{
    let y =  date.getFullYear().toString();
    let m =  (date.getMonth()+1).toString().padStart(2,'0');
    let d =  date.getDate().toString().padStart(2,'0');
    return `${y}-${m}-${d}`;
  }

  //muestra los anios al hacer click en anio-mes
  public OnMostrarAnio(){
    if(this.optionManipulate == 'day'){
      this.optionManipulate = 'year';
      this.ListYear(this.year,0);
    }
    
  }

/******************************************************************
propiedades
******************************************************************/


}
