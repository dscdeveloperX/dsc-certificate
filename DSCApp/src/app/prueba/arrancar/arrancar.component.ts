import { AfterViewInit, Component, QueryList, VERSION, ViewChildren } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AcordeonDivComponent } from '../acordeon-div/acordeon-div.component';

@Component({
  selector: 'app-arrancar',
  standalone: true,
  imports: [CommonModule, AcordeonDivComponent],
  templateUrl: './arrancar.component.html',
  styleUrls: ['./arrancar.component.css']
})
export class ArrancarComponent implements AfterViewInit  {
  @ViewChildren(AcordeonDivComponent) childs!:QueryList<AcordeonDivComponent>;
  public numbers;
  public name;
  constructor() {
      this.numbers = [0,1,2,3,4]
      this.name = `Angular! v${VERSION.full}`;
  }
  
  ngAfterViewInit() {
    //recorremos cada componente acordeon de querylist
      this.childs.forEach((item: AcordeonDivComponent) => { 
        //ejecutamos evento select (deseleccionamos todo)
        item.eventSelected.subscribe(selected => this.unselect(selected)) 
      });
  }

  unselect(selected:any) {
  //deselecciona todo
      this.childs.forEach((item) => { if (item !== selected) item.selected = false; });
  }
}
