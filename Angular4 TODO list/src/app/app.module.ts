import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule} from "@angular/forms";
import { AppComponent }  from './app.component';
import {TasksComponent} from './task.component';
import {TaskService} from './task.service';
import {RouterModule, Routes } from '@angular/router';

const routers: Routes=[
  {
    path:'',
    component: AppComponent,
    pathMatch:'full'
  },
  {
  path:'tasks',
  component: TasksComponent
  }
];

@NgModule({
  imports:      [ 
    BrowserModule, 
    FormsModule,
  RouterModule.forRoot(routers) 
],
  declarations: [ 
    AppComponent,
    TasksComponent
  ],
  bootstrap:    [ AppComponent ],
  providers: [TaskService]
})
export class AppModule { }
