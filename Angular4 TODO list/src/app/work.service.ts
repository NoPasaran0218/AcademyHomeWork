import {Injectable} from '@angular/core';

import {TASKS} from './mock-tasks';
import {WorkItem} from './WorkItem';

@Injectable()
export class WorkService{
    getTasks():Promise<WorkItem[]>{
        return new Promise(resolve=>{
            setTimeout(()=>{
                resolve(TASKS);
            }, 500);

        });
    };

    getTask(id:number): Promise<WorkItem>{
        return new Promise(resolve=>{
            setTimeout(() => {
                const task = TASKS.find(f=>f.id===id);
                resolve(task);
            }, 500);
        })
    };

}