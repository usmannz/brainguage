import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminQuestionsListingComponent } from './admin-question-listing.component';


describe('AdminQuestionsListingComponent', () => {
  let component: AdminQuestionsListingComponent;
  let fixture: ComponentFixture<AdminQuestionsListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminQuestionsListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminQuestionsListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
