import { Component, OnInit } from '@angular/core';

import {Hero} from "./hero";

import {HeroService} from './hero.service';

@Component({
  selector: 'heroes',
  templateUrl: `./heroes.component.html`,
  styleUrls:['./heroes.component.css']
})
export class HeroesComponent implements OnInit  { 
  heroes: Hero[];
  selectedHero: Hero;

  constructor(private heroService:HeroService){ }

  ngOnInit(){
    this.heroService.getHeroes().then(heroes=>this.heroes=heroes);
  }
  /*hero: Hero = {
    id:1,
    name:"Winstorm"
  };*/

    onSelected(hero: Hero){
    this.selectedHero=hero;
  }
}


