import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentApiService } from 'src/app/Core/Services/department-api.service';
import { IDepartment } from 'src/app/Core/Models/Entity/IDepartment';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';


@Component({
  selector: 'app-department-update',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './department-update.component.html',
  styleUrls: ['./department-update.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DepartmentUpdateComponent {
  private departmentApiService = inject(DepartmentApiService);

  public OnDepartmentUpdate():void{
  let data:IDepartment = {
    DepartmentID:4,
    CompanyID:'2000000000001',
    DepartmentName:'Deparment B dos',
    DepartmentActive:true
  };
  this.departmentApiService.DepartmentUpdate(data).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}