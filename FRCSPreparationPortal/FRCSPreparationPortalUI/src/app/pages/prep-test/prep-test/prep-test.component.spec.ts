import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PrepTestComponent } from './prep-test.component';


describe('PrepTestComponent', () => {
  let component: PrepTestComponent;
  let fixture: ComponentFixture<PrepTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
