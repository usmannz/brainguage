import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContextService } from '../../services/context.service';
// import { StripeService } from '../../services/stripe.service';
import { BaseComponent } from '../../shared/base/base.component';
import { PageAccessType } from '../../shared/enums';
// import { TenantService } from '../../services/tenant.service';

@Component({
  selector: 'app-membership-failure',
  templateUrl: './membership-failure.component.html',
})
export class MembershipFailureComponent extends BaseComponent implements OnInit {
  sessionId:string= ""
  constructor(
    protected _contextService: ContextService,
    protected _router: Router,
    // private stripeService: StripeService,
    protected route: ActivatedRoute,
    // private _tenantService: TenantService,

  ) {
    super(_contextService,_router);
    this._pageAccessType = PageAccessType.PUBLIC;    
    if (this.route.snapshot.params['CHECKOUT_SESSION_ID']) {
      this.sessionId =this.route.snapshot.params['CHECKOUT_SESSION_ID'];
      // this.stripeService.failureInformation(this.sessionId).subscribe((data: any) => {
      //             });
      }
//       this.route.queryParams.subscribe(params => {
//         for (let key in this.route.snapshot.queryParams) {
//           if (key == 'session_id' )
//             this.sessionId = params['session_id'];
//            
//           }
//       });
   }

   updateTenantStatus()
   {
    // if(localStorage["tenantStatus"])
    //     {
          // this._tenantService.updateTenanStatus().subscribe((status: any) => {
          //   localStorage["tenantStatus"]=JSON.stringify( status); 
          // });
                    // }
   }

  ngOnInit() {
    // this.updateTenantStatus();

  }

}
