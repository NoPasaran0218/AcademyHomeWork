"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var hero_detail_component_1 = require("./hero-detail.component");
var heroes_component_1 = require("./heroes.component");
var hero_service_1 = require("./hero.service");
var dashboard_component_1 = require("./dashboard.component");
var router_1 = require("@angular/router");
var homework_component_1 = require("./homework.component");
var work_service_1 = require("./work.service");
var work_details_component_1 = require("./work-details.component");
var routers = [
    {
        path: '',
        component: app_component_1.AppComponent,
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        component: dashboard_component_1.DashboardComponent
    },
    {
        path: 'heroes',
        component: heroes_component_1.HeroesComponent
    },
    {
        path: 'details/:id',
        component: hero_detail_component_1.HeroDetailsComponent
    },
    {
        path: 'homework',
        component: homework_component_1.HomeWorkComponent
    },
    {
        path: 'workdetails/:id',
        component: work_details_component_1.WorkDetailsComponent
    }
];
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            router_1.RouterModule.forRoot(routers)
        ],
        declarations: [
            app_component_1.AppComponent,
            hero_detail_component_1.HeroDetailsComponent,
            heroes_component_1.HeroesComponent,
            dashboard_component_1.DashboardComponent,
            homework_component_1.HomeWorkComponent,
            work_details_component_1.WorkDetailsComponent
        ],
        bootstrap: [app_component_1.AppComponent],
        providers: [hero_service_1.HeroService, work_service_1.WorkService]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map