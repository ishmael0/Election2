import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormGroup, Validators } from '@angular/forms';
import { VoteComponent } from './vote/vote.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { CommonModule } from '@angular/common';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzModalModule } from 'ng-zorro-antd/modal';

const routes: Routes = [
  { path: '', component: VoteComponent },
];

 
@NgModule({
  declarations: [
    VoteComponent
  ],
  imports: [
    CommonModule, NzAlertModule, NzGridModule, NzCheckboxModule, NzCardModule, NzModalModule,
    RouterModule.forChild(routes), FormsModule, ReactiveFormsModule, NzInputModule, NzButtonModule  
  ],
})
export class VoteModule {
}
