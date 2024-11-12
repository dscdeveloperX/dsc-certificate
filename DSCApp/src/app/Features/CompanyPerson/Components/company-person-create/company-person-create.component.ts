import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyPersonApiService } from 'src/app/Core/Services/company-person-api.service';
import { ICompanyPerson } from 'src/app/Core/Models/Entity/ICompanyPerson';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-company-person-create',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-person-create.component.html',
  styleUrls: ['./company-person-create.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class CompanyPersonCreateComponent {
  private companyPersonApiService = inject(CompanyPersonApiService);

  public OnCampanyPersonCreate():void{
  let data:ICompanyPerson = {
    CompanyID:'1000000000001',
    PersonID:'0000000001',
    PersonActive:true
  };
  this.companyPersonApiService.CompanyPersonCreate(data).subscribe(
    {
      next:(data:IDataResponse<ICompanyPerson>)=>{
        console.log(data);
      }
    }
  );
}
}