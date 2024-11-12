import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityApiService } from 'src/app/Core/Services/city-api.service';
import { ICityRequest } from 'src/app/Core/Models/Request/ICityRequest';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-city-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './city-update.component.html',
  styleUrls: ['./city-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CityUpdateComponent {
  private cityApiService = inject(CityApiService);

    public OnCityUpdate():void{
    let data:ICityRequest = {
      CityID:27,
      CityName:'Laura City xxx',
      CityActive:false
    };
    this.cityApiService.CityUpdate(data).subscribe(
      {
        next:(data:IDataResponse<any>)=>{
          console.log(data);
        }
      }
    );
  }
}
