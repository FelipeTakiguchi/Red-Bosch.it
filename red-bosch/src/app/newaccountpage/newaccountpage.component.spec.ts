import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewaccountpageComponent } from './newaccountpage.component';

describe('NewaccountpageComponent', () => {
  let component: NewaccountpageComponent;
  let fixture: ComponentFixture<NewaccountpageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewaccountpageComponent]
    });
    fixture = TestBed.createComponent(NewaccountpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
