import {Injectable} from '@angular/core';

import {HEROES} from './mock-heroes';
import {Hero} from './hero';

@Injectable()
export class HeroService{
    getHeroes():Promise<Hero[]>{
        return new Promise(resolve=>{
            setTimeout(()=>{
                resolve(HEROES);
            }, 500);

        });
    };

    getHero(id:number): Promise<Hero>{
        return new Promise(resolve=>{
            setTimeout(() => {
                const hero = HEROES.find(f=>f.id===id);
                resolve(hero);
            }, 500);
        })
    };

}