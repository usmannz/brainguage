
import { Component } from '@angular/core';
import { PageAccessType, Roles, SideMenu, } from '../enums';
import { ContextService } from '../../services/context.service';
import { Router } from '@angular/router';
declare var window: any;
declare var jquery: any;
declare var $: any;

@Component({
    selector: 'app-base',
    templateUrl: './base.component.html',
    styleUrls: ['./base.component.css']
})
export class BaseComponent {
    protected _pageAccessType: PageAccessType = PageAccessType.PUBLIC;
    protected _pageAccessLevel :Roles[]=[];
    public isEmail: boolean;
    public tenantStatus:boolean = false;
    public emailAddress:string;
    protected _contextService: ContextService;
    protected _router: Router;
    constructor(protected contextService: ContextService,protected router: Router ) {
        this._contextService = contextService;
        this._router = router;
        if(localStorage["email"])
        {
            this.emailAddress = JSON.parse(localStorage["email"]);
        }
        
        if(localStorage["isEmail"])
        {
            this.isEmail = JSON.parse(localStorage["isEmail"]);
        } 

        // if(localStorage["tenantStatus"])
        // {
        //   var status = JSON.parse(localStorage["tenantStatus"]);
        //   if(status == TenantStatus.Active)
        //   {
        //     this.tenantStatus = true;
        //   }
        // } 
    }

    ngOnInit() {
        this.validateUserSession();
        this.main_documentReady();
    }

    main_documentReady() {
    }

    setPageTitle(title: string) {
        window.document.title = title;
    }

    validateUserSession() {
        if (this._pageAccessType == PageAccessType.PUBLIC)
            return;


        if (!this._contextService._session || !localStorage["app"]) {
            this._contextService.logout();
            return;
        }
        // let roleCheck = this.contextService._userRoles.filter(item => this._pageAccessLevel.some(o2 => item.roleId === o2));
        let roleCheck = [];

        if(!this.tenantStatus)
        {
         this.tenantInActiveAccess();
        }

        if(roleCheck.length ==0)
        {
        this._router.navigate(['/projects'])
        }
        //// check token expiry
        var currentTime = new Date().getTime() / 1000;
        if (parseInt(this._contextService._session.exp) < currentTime) {
            this._contextService.logout();
        }
    }

    showMessage(message: string, type: string) {
        window.showMessage(message, type);
    }
   

    openPopup(url: string) {
        window.openPopup(url);
    }

    getElementById(id: string) {
        return $(window.document.getElementById(id));
    }

    removeDom(obj) {
        return window.removeDom(obj);
    }

    trackByFunction(index, item) {
        return item.id;
     }

     redirectTo(uri: string) {
        this._router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
          this._router.navigate([uri]));
      }
    
      superAdminRedirectTo(uri: string) {
        uri = `sa/${uri}`
        this._router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
          this._router.navigate([uri]));
      }

     

      checkUserRole(sideMenu: SideMenu) {
        switch (sideMenu) {
          case SideMenu.Admin: {
            var roleCheck = this.contextService._userRoles.find(item => item.roleId == Roles.Admin);
            if (roleCheck) {
              return true;
            }
            else {
              return false;
            }
          }
          case SideMenu.User: {
            var roleCheck = this.contextService._userRoles.find(item => item.roleId == Roles.User);
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

      tenantInActiveAccess()
      {
        var routerlink =this._router.url.replace(/\//g, "");
        switch (routerlink) {
          case 'profile': {
            this._router.navigate(['/profile'])    
            }
        break;
        default: {
          this._router.navigate(['/settings'])    
          break;
        }
      }      }
      
      hideEmailBanner(event:any)
      {
        this.isEmail = true;
      }

      // viewOnlyProject(statusId:number)
      // {
      //   if(statusId == ProjectStatus["In Progress"] || statusId == ProjectStatus['Not Started'])
      //   {
      //     return false;
      //   }
      //   else
      //   {
      //     return true;
      //   }

      // }

}
