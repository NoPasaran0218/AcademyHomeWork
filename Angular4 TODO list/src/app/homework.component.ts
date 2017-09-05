import {Component, OnInit} from '@angular/core';
import {WorkItem} from './WorkItem';
import {WorkService} from './work.service';

@Component({
    selector: 'homework',
    templateUrl: './homework.component.html',
    styleUrls: ['./homework.component.css']
})

export class HomeWorkComponent implements OnInit{
    workItems: WorkItem[];
    selectedTask: WorkItem;
    temp: WorkItem;
    constructor(private workService:WorkService){}
    
      ngOnInit(){
        this.workService.getTasks().then(workItems=>this.workItems=workItems, temp=>this.temp=this.workItems[this.workItems.length-1]);
        //this.temp=this.workItems[this.workItems.length-1];
      }
    
        onSelected(task: WorkItem){
            this.selectedTask=task;
            if (this.selectedTask===this.workItems[this.workItems.length-1]){
                this.Add();
            }
        }

        Add(){
            this.temp=new WorkItem();
            this.temp.title="title";
            this.workItems.push(this.temp);
        }

        doComplite(task: WorkItem){
            if (task!=this.workItems[this.workItems.length-1])
            this.workItems.find(o=>o.id===task.id).isComplete=true;
        }

        removeTask(task: WorkItem){
            if (task!=this.workItems[this.workItems.length-1])
            this.workItems.splice(this.workItems.findIndex(o=>o.id===task.id),1);
        }
}