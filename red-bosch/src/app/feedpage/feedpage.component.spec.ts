import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedPageComponent } from './feedpage.component';

describe('FeedpageComponent', () => {
  let component: FeedPageComponent;
  let fixture: ComponentFixture<FeedPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FeedPageComponent]
    });
    fixture = TestBed.createComponent(FeedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
