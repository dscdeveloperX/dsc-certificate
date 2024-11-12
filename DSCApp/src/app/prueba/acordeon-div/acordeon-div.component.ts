import { Component, EventEmitter, VERSION } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-acordeon-div',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './acordeon-div.component.html',
  styleUrls: ['./acordeon-div.component.css']
})
export class AcordeonDivComponent {
  name: string;
  selected: boolean = false;
  eventSelected:EventEmitter<AcordeonDivComponent> = 
  new EventEmitter<AcordeonDivComponent>();

  constructor() {
      this.name = `Angular! v${VERSION.full}`;
  }

  public select(element: any) {
    //siempre cambia de true a false el elemento actual
      this.selected = !this.selected;
      //pasamos el actual objeto acordion
      this.eventSelected.emit(this);
  }
}
