import { Component, OnInit, ElementRef } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent extends BaseComponent implements OnInit {
  public focus;
  public listTitles: any[];
  public location: Location;
  public username = "";
  constructor(protected _contextService: ContextService,
    protected _router: Router,
location: Location,  private element: ElementRef) {
  super(_contextService,_router)
    this.location = location;
  }

  ngOnInit() {
    this.listTitles = ROUTES.filter(listTitle => listTitle);
   this.username = this._contextService.CurrentUserName;
  }
  getTitle(){
    var titlee = this.location.prepareExternalUrl(this.location.path());
    titlee = titlee.split('/')[1];  // Extracts "prep-test"

    // if(titlee.charAt(0) === '#' || titlee.charAt(0) === '/'){
    //     titlee = titlee.slice( 1 );
    // }

    for(var item = 0; item < this.listTitles.length; item++){
        if(this.listTitles[item].path === titlee){
            return this.listTitles[item].title;
        }
    }
    return '';
  }

  btnLogout_Clicked()
  {
      this._contextService.logout();
  }
}
