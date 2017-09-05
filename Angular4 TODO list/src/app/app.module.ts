import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule} from "@angular/forms";
import { AppComponent }  from './app.component';
import {HeroDetailsComponent} from './hero-detail.component';
import {HeroesComponent} from './heroes.component';
import {HeroService} from './hero.service';
import { DashboardComponent } from "./dashboard.component";
import {RouterModule, Routes } from '@angular/router';
import {HomeWorkComponent} from './homework.component';
import {WorkService} from './work.service';
import {WorkDetailsComponent} from './work-details.component';

const routers: Routes=[
  {
    path:'',
    component: AppComponent,
    pathMatch:'full'
  },
  {
  path: 'dashboard',
  component: DashboardComponent
  },
  {
  path:'heroes',
  component: HeroesComponent
  },
{
  path: 'details/:id',
  component: HeroDetailsComponent
},
{
  path: 'homework',
  component: HomeWorkComponent
},
{
  path:'workdetails/:id',
  component: WorkDetailsComponent
}];

@NgModule({
  imports:      [ 
    BrowserModule, 
    FormsModule,
  RouterModule.forRoot(routers) 
],
  declarations: [ 
    AppComponent, 
    HeroDetailsComponent,
    HeroesComponent,
    DashboardComponent,
    HomeWorkComponent,
    WorkDetailsComponent],
  bootstrap:    [ AppComponent ],
  providers: [HeroService, WorkService]
})
export class AppModule { }
