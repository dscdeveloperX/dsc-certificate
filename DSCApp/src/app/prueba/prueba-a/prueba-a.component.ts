import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-prueba-a',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './prueba-a.component.html',
  styleUrls: ['./prueba-a.component.css']
})
export class PruebaAComponent {
public contexto1:any = {
  cedula:'0918723453',
  nombre:'Darwin Sanchez',
  edad:30
};
}
