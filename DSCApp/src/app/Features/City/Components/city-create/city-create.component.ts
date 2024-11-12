import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityApiService } from 'src/app/Core/Services/city-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { ICityRequest } from 'src/app/Core/Models/Request/ICityRequest';

@Component({
  selector: 'app-city-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './city-create.component.html',
  styleUrls: ['./city-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CityCreateComponent {
  private cityApiService = inject(CityApiService);

    public OnCityCreate():void{

    let data:ICityRequest = {
      CityName:'Laura City',
      CityActive:false
    };
    this.cityApiService.CityCreate(data).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          console.log(data);
        }
      }
    );

  }
}
