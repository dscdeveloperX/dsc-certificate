import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityApiService } from 'src/app/Core/Services/city-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-city-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './city-delete.component.html',
  styleUrls: ['./city-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CityDeleteComponent {
  private cityApiService = inject(CityApiService);

    public OnCityDelete():void{
    this.cityApiService.CityDelete(26).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          console.log(data);
        }
      }
    );
  }
}
