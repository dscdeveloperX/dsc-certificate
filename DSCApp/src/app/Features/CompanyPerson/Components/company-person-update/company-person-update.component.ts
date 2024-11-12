import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPersonApiService } from 'src/app/Core/Services/company-person-api.service';
import { ICompanyPerson } from 'src/app/Core/Models/Entity/ICompanyPerson';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-company-person-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-person-update.component.html',
  styleUrls: ['./company-person-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyPersonUpdateComponent {
  private companyPersonApiService = inject(CompanyPersonApiService);

  public OnCampanyPersonUpdate():void{
  let data:ICompanyPerson = {
    CompanyPersonID:2,
    CompanyID:'1000000000001',
    PersonID:'0000000001',
    PersonActive:true
  };
  this.companyPersonApiService.CompanyPersonUpdate(data).subscribe(
    {
      next:(data:IDataResponse<ICompanyPerson>)=>{
        console.log(data);
      }
    }
  );
}
}