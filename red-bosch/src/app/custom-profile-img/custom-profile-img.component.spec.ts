import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomProfileImgComponent } from './custom-profile-img.component';

describe('CustomProfileImgComponent', () => {
  let component: CustomProfileImgComponent;
  let fixture: ComponentFixture<CustomProfileImgComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomProfileImgComponent]
    });
    fixture = TestBed.createComponent(CustomProfileImgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
