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
    this.tasks=this.taskService.getTasks();//.then(tasks=>this.tasks=tasks);
  }
  /*hero: Hero = {
    id:1,
    name:"Winstorm"
  };*/

    onSelectedTask(task: Task){
      this.selectedTask=task;
    }

    doComplite(task:Task):void{
      task.isComplete=true;
    }

    remove(task:Task):void{
      task.removed=true;
    }

    addTask(title: string){
      /*var newTask:Task;
      newTask = new Task(title);
      newTask.id=this.tasks.length;
      this.tasks.push(newTask);*/
      this.taskService.addTask(title);
    }
}


