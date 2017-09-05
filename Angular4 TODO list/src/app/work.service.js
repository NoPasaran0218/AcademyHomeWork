"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var mock_tasks_1 = require("./mock-tasks");
var WorkService = (function () {
    function WorkService() {
    }
    WorkService.prototype.getTasks = function () {
        return new Promise(function (resolve) {
            setTimeout(function () {
                resolve(mock_tasks_1.TASKS);
            }, 500);
        });
    };
    ;
    WorkService.prototype.getTask = function (id) {
        return new Promise(function (resolve) {
            setTimeout(function () {
                var task = mock_tasks_1.TASKS.find(function (f) { return f.id === id; });
                resolve(task);
            }, 500);
        });
    };
    ;
    return WorkService;
}());
WorkService = __decorate([
    core_1.Injectable()
], WorkService);
exports.WorkService = WorkService;
//# sourceMappingURL=work.service.js.map