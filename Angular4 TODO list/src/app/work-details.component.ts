import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { WorkItem } from './WorkItem';
import { WorkService } from './work.service';
import 'rxjs/add/operator/switchMap';
@Component({
    selector: 'work-details',
    templateUrl: './work-details.component.html'
})
export class WorkDetailsComponent implements OnInit {
    @Input() task: WorkItem;
    constructor(
        private workService: WorkService,
        private route: ActivatedRoute,
        private location: Location
    ) { }
    ngOnInit() {
        this.route.paramMap
            .switchMap((params: ParamMap) => this.workService.getTask(+params.get('id')))
            .subscribe((task: WorkItem ) => {
                this.task = task;
            });
    }
}