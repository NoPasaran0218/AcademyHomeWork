import { Component, OnInit } from '@angular/core';

import {Task} from "./Task";

import {TaskService} from './task.service';

@Component({
  selector: 'tasks',
  templateUrl: `./task.component.html`,
  styleUrls:['./task.component.css']
})
export class TasksComponent implements OnInit  { 
  tasks: Task[];
  selectedTask: Task;

  constructor(private taskService:TaskService){ }

  ngOnInit(){
    this.taskService.getTasks().then(tasks=>this.tasks=tasks);
  }

    onSelectedTask(task: Task){
      this.selectedTask=task;
    }

    doComplite(task:Task):void{
      this.taskService.doComplite(task.id);
      this.ngOnInit();
    }

    remove(task:Task):void{
      this.taskService.deleteTask(task.id);
      this.ngOnInit();
    }

    addTask(title: string){
      this.taskService.addTask(title);
      this.ngOnInit();
    }
}


