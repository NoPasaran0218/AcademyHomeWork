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
var Task_1 = require("./Task");
var TaskService = (function () {
    /**
     *
     */
    function TaskService() {
        this.tasks = [
            { id: 1, title: "task1", isComplete: false, removed: false },
            { id: 2, title: "task2", isComplete: false, removed: false },
            { id: 3, title: "task3", isComplete: false, removed: false },
            { id: 4, title: "task4", isComplete: false, removed: false },
            { id: 5, title: "task5", isComplete: false, removed: false },
            { id: 6, title: "task6", isComplete: false, removed: false },
        ];
        this.writeListInLocalStorage(this.tasks);
    }
    TaskService.prototype.writeListInLocalStorage = function (list) {
        localStorage.setItem("Tasks", JSON.stringify(list));
    };
    TaskService.prototype.getListFromLocalStorage = function () {
        return JSON.parse(localStorage.getItem("Tasks"));
    };
    TaskService.prototype.getTasks = function () {
        var _this = this;
        return new Promise(function (resolve) {
            setTimeout(function () {
                resolve(_this.getListFromLocalStorage().filter(function (i) { return !i.removed; }));
            }, 50);
        });
    };
    ;
    TaskService.prototype.addTask = function (title) {
        var _this = this;
        return new Promise(function (resolve) {
            setTimeout(function () {
                var item = new Task_1.Task(title);
                item.id = _this.tasks.length;
                _this.tasks.push(item);
                _this.writeListInLocalStorage(_this.tasks);
                resolve(item);
            }, 50);
        });
    };
    TaskService.prototype.deleteTask = function (id) {
        var _this = this;
        return new Promise(function (resolve) {
            setTimeout(function () {
                var index = _this.getListFromLocalStorage().findIndex(function (i) { return i.id === id; });
                if (index != -1) {
                    _this.tasks[index].removed = true;
                    _this.writeListInLocalStorage(_this.tasks);
                    resolve(true);
                }
                else {
                    resolve(false);
                }
            }, 50);
        });
    };
    TaskService.prototype.doComplite = function (id) {
        var _this = this;
        return new Promise(function (resolve) {
            setTimeout(function () {
                var index = _this.getListFromLocalStorage().findIndex(function (i) { return i.id === id; });
                if (index != -1) {
                    _this.tasks[index].isComplete = true;
                    _this.writeListInLocalStorage(_this.tasks);
                    resolve(true);
                }
                else {
                    resolve(false);
                }
            }, 50);
        });
    };
    return TaskService;
}());
TaskService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], TaskService);
exports.TaskService = TaskService;
//# sourceMappingURL=task.service.js.map