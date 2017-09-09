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
        this.tasks = [
            {id:1, title: "task1", isComplete:false, removed:false},
            {id:2, title: "task2", isComplete:false, removed:false},
            {id:3, title: "task3", isComplete:false, removed:false},
            {id:4, title: "task4", isComplete:false, removed:false},
            {id:5, title: "task5", isComplete:false, removed:false},
            {id:6, title: "task6", isComplete:false, removed:false},
        ];
        this.writeListInLocalStorage(this.tasks);     
    }

    writeListInLocalStorage(list:Task[]):void{
        localStorage.setItem("Tasks", JSON.stringify(list));
    }

    getListFromLocalStorage():Task[]{
        return JSON.parse(localStorage.getItem("Tasks"));
    }

    getTasks():Promise<Task[]>{
        return new Promise(resolve=>{
            setTimeout(() => {
                resolve(this.getListFromLocalStorage().filter(i => !i.removed));
            }, 50);
        });
    };

    addTask(title: string): Promise<Task> {
        return new Promise(resolve => {
            setTimeout(() => {
                let item = new Task(title);
                item.id = this.tasks.length;
                this.tasks.push(item);
                this.writeListInLocalStorage(this.tasks);
                resolve(item);
            }, 50);
        });
    }

    deleteTask(id: number): Promise<boolean> {
        return new Promise<boolean>(resolve => {
            setTimeout(() => {
                const index = this.getListFromLocalStorage().findIndex(i => i.id === id);
                if (index != -1) {
                    this.tasks[index].removed = true;
                    this.writeListInLocalStorage(this.tasks);
                    resolve(true);
                } else {
                    resolve(false);
                }                
            }, 50);
        });
    }

    doComplite(id: number): Promise<boolean> {
        return new Promise<boolean>(resolve => {
            setTimeout(() => {
                const index = this.getListFromLocalStorage().findIndex(i => i.id === id);
                if (index != -1) {
                    this.tasks[index].isComplete = true;
                    this.writeListInLocalStorage(this.tasks);
                    resolve(true);
                } else {
                    resolve(false);
                }                
            }, 50);
        });
    }

}