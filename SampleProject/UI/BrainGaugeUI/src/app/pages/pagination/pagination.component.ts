import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { ContextService } from '../../services/context.service';
import { BaseComponent } from '../../shared/base/base.component';
import { PageAccessType } from '../../shared/enums';
import { Pager } from 'src/app/entities/pager';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
})
export class PaginationComponent extends BaseComponent implements OnInit {
  private _pagination: Pager = new Pager();
  @Output() pager = new EventEmitter();
  @Input() _count;
  public firstPageVisibility: boolean = false;
  public lastPageVisibility: boolean = true;
  public totalPages: number = 0;
  public currentPage: number = 1;
  public showPagination: boolean = false;
  constructor(
    protected _ctxService: ContextService,
    protected _router: Router

  ) {
    super(_ctxService, _router);
    this._pageAccessType = PageAccessType.PRIVATE;
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    for (let property in changes) {
      if (property === '_count') {
        if (this._count > 0) {
          this.showPagination = true;
        }
        else {
          this.showPagination = false;
        }

        this.totalPages = Math.ceil(this._count / this._pagination.pageSize);
        this.validatePaginationControls();


      }
    }
  }

  validatePaginationControls() {
    if (this.currentPage == 1) {
      this.firstPageVisibility = false;
    }
    else {
      this.firstPageVisibility = true;

    }

    if (this.currentPage == this.totalPages) {
      this.lastPageVisibility = false;
    }
    else {
      this.lastPageVisibility = true;
    }

    if (this.totalPages >= 1 && this.totalPages <= this.currentPage) {
      this.lastPageVisibility = false;
    }
    else {
      this.lastPageVisibility = true;
    }

  }

  counter(i: number) {
    return new Array(i);
  }

  onPageChange(currentPage) {
    this._pagination.pageIndex = Number(currentPage);
    this.pager.emit({ pageIndex: this._pagination.pageIndex, pageSize: this._pagination.pageSize })
  }

  onchangePageIndex(currentPage:number) {
    this.currentPage = Number(currentPage);
    this.validatePaginationControls();
    this._pagination.pageIndex = currentPage;
    this.pager.emit({ pageIndex: this._pagination.pageIndex, pageSize: this._pagination.pageSize })

  }

  onchangePageSize(pageSize:number) {
    this._pagination.pageSize = Number(pageSize);
    this.totalPages = Math.ceil(this._count / this._pagination.pageSize);
    this._pagination.pageIndex = 1;
    this.currentPage = 1;
    this.validatePaginationControls();
    this.pager.emit({ pageIndex: this._pagination.pageIndex, pageSize: this._pagination.pageSize })
  }

  onchangePageForward() {
    this.firstPageVisibility = true
    this.currentPage = this.currentPage + 1;
    this.validatePaginationControls();
    this._pagination.pageIndex = this.currentPage;
    this.pager.emit({ pageIndex: this._pagination.pageIndex, pageSize: this._pagination.pageSize })
  }

  onchangePageBackword() {
    this.firstPageVisibility = true
    this.currentPage = this.currentPage - 1;
    this.validatePaginationControls();
    this._pagination.pageIndex = this.currentPage;
    this.pager.emit({ pageIndex: this._pagination.pageIndex, pageSize: this._pagination.pageSize })
  }

}
