import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityApiService } from 'src/app/Core/Services/city-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';
import { ICity } from 'src/app/Core/Models/Entity/ICity';

@Component({
  selector: 'app-city-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './city-read.component.html',
  styleUrls: ['./city-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CityReadComponent {

  private cityApiService = inject(CityApiService);

  public Data:WritableSignal<ICity[]> = signal<ICity[]>([]);

  public OnCityRead():void{
    this.cityApiService.CityRead(null, null,1,1).subscribe(
      {
        next:(data:IDataResponse<ICity>)=>{
          this.Data.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }
  

}
