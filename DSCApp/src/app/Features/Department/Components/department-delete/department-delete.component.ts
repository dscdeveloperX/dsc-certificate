import { Component, inject, ChangeDetectionStrategy, ChangeDetectorRef, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentApiService } from 'src/app/Core/Services/department-api.service';
import { IDataResponse } from 'src/app/Core/Models/Response/IDataResponse';

@Component({
  selector: 'app-department-delete',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './department-delete.component.html',
  styleUrls: ['./department-delete.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class DepartmentDeleteComponent {
  private departmentApiService = inject(DepartmentApiService);

  public OnDepartmentDelete():void{
  this.departmentApiService.DepartmentDelete(4).subscribe(
    {
      next:(data:IDataResponse<any>)=>{
        console.log(data);
      }
    }
  );
}
}
