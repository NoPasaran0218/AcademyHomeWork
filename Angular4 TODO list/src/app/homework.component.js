"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var WorkItem_1 = require("./WorkItem");
var work_service_1 = require("./work.service");
var HomeWorkComponent = (function () {
    function HomeWorkComponent(workService) {
        this.workService = workService;
    }
    HomeWorkComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.workService.getTasks().then(function (workItems) { return _this.workItems = workItems; }, function (temp) { return _this.temp = _this.workItems[_this.workItems.length - 1]; });
        //this.temp=this.workItems[this.workItems.length-1];
    };
    HomeWorkComponent.prototype.onSelected = function (task) {
        this.selectedTask = task;
        if (this.selectedTask === this.workItems[this.workItems.length - 1]) {
            this.Add();
        }
    };
    HomeWorkComponent.prototype.Add = function () {
        this.temp = new WorkItem_1.WorkItem();
        this.temp.title = "title";
        this.workItems.push(this.temp);
    };
    HomeWorkComponent.prototype.doComplite = function (task) {
        if (task != this.workItems[this.workItems.length - 1])
            this.workItems.find(function (o) { return o.id === task.id; }).isComplete = true;
    };
    HomeWorkComponent.prototype.removeTask = function (task) {
        if (task != this.workItems[this.workItems.length - 1])
            this.workItems.splice(this.workItems.findIndex(function (o) { return o.id === task.id; }), 1);
    };
    return HomeWorkComponent;
}());
HomeWorkComponent = __decorate([
    core_1.Component({
        selector: 'homework',
        templateUrl: './homework.component.html',
        styleUrls: ['./homework.component.css']
    }),
    __metadata("design:paramtypes", [work_service_1.WorkService])
], HomeWorkComponent);
exports.HomeWorkComponent = HomeWorkComponent;
//# sourceMappingURL=homework.component.js.map