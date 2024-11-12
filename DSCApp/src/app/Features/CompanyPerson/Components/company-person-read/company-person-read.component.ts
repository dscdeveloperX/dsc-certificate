import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPersonApiService } from 'src/app/Core/Services/company-person-api.service';
import { ICompanyPerson } from 'src/app/Core/Models/Entity/ICompanyPerson';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-company-person-read',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-person-read.component.html',
  styleUrls: ['./company-person-read.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyPersonReadComponent {
  private companyPersonApiService = inject(CompanyPersonApiService);

  public Data:WritableSignal<ICompanyPerson[]> = signal<ICompanyPerson[]>([]);
  

  public OnCompanyPersonRead():void{
    this.companyPersonApiService.CompanyPersonRead(null, null).subscribe(
      {
        next:(data:IDataResponse<ICompanyPerson>)=>{
          this.Data?.set(data.Data);
          console.log(this.Data());
        }
      }
    );
  }
  
  
}
