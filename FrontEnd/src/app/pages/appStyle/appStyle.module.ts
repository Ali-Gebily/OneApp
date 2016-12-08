import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Routes, RouterModule } from '@angular/router';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import {ColorPickerModule} from 'angular2-color-picker';

import { AppStyleService } from './services/appStyle.service';
import { ListRulesComponent } from './components/listRules/listRules.component';
import { EditRuleStyleComponent } from './components/editRuleStyle/editRuleStyle.component'; 




const routes: Routes = [
  {
    path: '',
    redirectTo: "listRules"
  },
  {
    path: 'listRules',
    component: ListRulesComponent
  }
  ,
  {
    path: 'editRuleStyle/:id',
    component: EditRuleStyleComponent
  } 
];



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgaModule,
    RouterModule.forChild(routes),
     Ng2SmartTableModule,
     ColorPickerModule
  ],
  declarations: [
    ListRulesComponent,EditRuleStyleComponent
  ],
  providers: [
    AppStyleService
  ]
})
export default class appStylesModule { }
