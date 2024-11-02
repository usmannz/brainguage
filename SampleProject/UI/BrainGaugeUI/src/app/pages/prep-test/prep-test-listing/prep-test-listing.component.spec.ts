import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PrepTestListingComponent } from './prep-test-listing.component';


describe('PrepTestListingComponent', () => {
  let component: PrepTestListingComponent;
  let fixture: ComponentFixture<PrepTestListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepTestListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepTestListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
