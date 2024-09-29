import { Component, OnInit } from '@angular/core';
import { ContextService } from './services/context.service';
import { HomeService } from './services/home.service';
import { Router } from '@angular/router';
import { isDevMode } from '@angular/core';
import { BaseComponent } from './shared/base/base.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent extends BaseComponent implements OnInit {
  title = 'argon-dashboard-angular';
  public showLeftMenue: Boolean = true;
  public isDevMode: boolean = true;

  constructor(
      protected _contextService: ContextService,
      private _homeService: HomeService,
      protected _router: Router,
      // private _themeService: ThemeService,

      ) {
      super(_contextService,_router);

      this.isDevMode = isDevMode();
  }

  ngOnInit() {
      this._contextService.service_OnInit();
  }

  ngAfterViewInit() {
      setTimeout(() => {
          this.loadApplicationCache();
          // this.loadApplicationTheme();
      }, 0);
  }

  //// loadApplicationCache /////////////////////////////////////////////////////////////////////////////////////////////////////////////
  async loadApplicationCache() {
      console.log('app cache...');

  }

  // async loadApplicationTheme()
  // {
  //     if(localStorage["theme"] == 'lightTheme')
  //     {
  //         this._themeService.toggleLight();
  //     }
  //     else
  //     {
  //         this._themeService.toggleDark();
  //     }
  // }
}


