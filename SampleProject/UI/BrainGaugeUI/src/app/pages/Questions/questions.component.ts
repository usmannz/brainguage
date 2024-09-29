import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { ContextService } from 'src/app/services/context.service';
import { Router } from '@angular/router';
import { PageAccessType, Roles } from 'src/app/shared/enums';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.scss']
})
export class Questionsomponent extends BaseComponent  implements OnInit {
  constructor(
    protected _ctxService: ContextService,
    protected _router: Router

  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PUBLIC;
    this._pageAccessLevel.push(
      Roles.Admin,
    )
  }
  ngOnInit() {
    super.ngOnInit();
  }
}
