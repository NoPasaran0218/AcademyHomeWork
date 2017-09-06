import {Injectable} from '@angular/core';

import {TASKS} from "./mock-tasks";
import {Task} from './Task';

@Injectable()
export class TaskService{
    tasks:Task[];
    /**
     *
     */
    constructor() {
        this.tasks=TASKS;        
    }

    /*getTasks():Promise<Task[]>{
        return new Promise(resolve=>{
            setTimeout(()=>{
                resolve(TASKS);
            }, 500);
        });
    };

    getTask(id:number): Promise<Task>{
        return new Promise(resolve=>{
            setTimeout(() => {
                const task = TASKS.find(f=>f.id===id);
                resolve(task);
            }, 500);
        })
    };*/

    getTasks():Task[]{
        return this.tasks;
    }

    addTask(title:string):void{
        var newTask:Task;
        newTask = new Task(title);
        newTask.id=this.tasks.length;
        this.tasks.push(newTask);
    }

}