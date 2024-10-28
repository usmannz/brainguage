import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContextService } from 'src/app/services/context.service';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { Pages, Roles, SideMenu } from 'src/app/shared/enums';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '' },
    { path: '/questions', title: 'Questions',  icon:'ni-planet text-blue', class: '' },
    { path: '/questions-assignment', title: 'Questions Assignment',  icon:'ni-planet text-blue', class: '' },
    { path: '/quiz', title: 'Quiz',  icon:'ni-planet text-blue', class: '' },
    { path: '/categories', title: 'Categories',  icon:'ni-planet text-blue', class: '' },
    { path: '/mock-test', title: 'Mock Test',  icon:'ni-planet text-blue', class: '' },
    { path: '/demo-test', title: 'Demo Test',  icon:'ni-planet text-blue', class: '' },

    // { path: '/maps', title: 'Maps',  icon:'ni-pin-3 text-orange', class: '' },
    // { path: '/user-profile', title: 'User profile',  icon:'ni-single-02 text-yellow', class: '' },
    // { path: '/tables', title: 'Tables',  icon:'ni-bullet-list-67 text-red', class: '' },
    // { path: '/login', title: 'Login',  icon:'ni-key-25 text-info', class: '' },
    // { path: '/register', title: 'Register',  icon:'ni-circle-08 text-pink', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent extends BaseComponent implements OnInit {
  @Input() CurrentPage: Pages;
  public menuItems: any[];
  public isCollapsed = true;
  public get sideMenu(): typeof SideMenu {
    return SideMenu;
  }

  constructor(
    protected _contextService: ContextService,
    private _avRoute: ActivatedRoute,
    protected _router: Router

  ) {
    super(_contextService, _router);
    this.CurrentPage = Pages.None;

  }
  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }

  checkUserRole(sideMenu: SideMenu) {
    switch (sideMenu) {
      // case SideMenu.Dashboard:
      case SideMenu.Categories:
        case SideMenu.Questions:
        case SideMenu.QuestionsAssignment: 
        {
          var roleCheck = this._contextService._userRoles.find(item => item.roleId == Roles.Admin);
          if (roleCheck) {
            return true;
          }
          else {
            return false;
          }
        } 
        // case SideMenu.Questions:  
        // {
        //   var roleCheck = this._contextService._userRoles.find(item => item.roleId == Roles.Admin || item.roleId == Roles.User);
        //   if (roleCheck) {
        //     return true;
        //   }
        //   else {
        //     return false;
        //   }
        // } 
        case SideMenu.DemoTest:  
        case SideMenu.MockTest:  
        // case SideMenu.Quiz:  
        {
          var roleCheck = this._contextService._userRoles.find(item => item.roleId == Roles.User);
          if (roleCheck) {
            return true;
          }
          else {
            return false;
          }
        } 
      case SideMenu.User: {
        var roleCheck = this._contextService._userRoles.find(item => item.roleId == Roles.User);
        if (roleCheck) {
          return true;
        }
        else {
          return false;
        }
      }
      case SideMenu.Admin: {
        var roleCheck = this._contextService._userRoles.find(item => item.roleId == Roles.Admin);
        if (roleCheck) {
          return true;
        }
        else {
          return false;
        }
      }
      default: {
        break;
      }
    }
  }
  check(role: Roles) {

    var roleCheck = this._contextService._userRoles.find(item => item.roleId == role);
    if (roleCheck) {
      return true;
    }
    else {
      return false;
    }
  }

}
