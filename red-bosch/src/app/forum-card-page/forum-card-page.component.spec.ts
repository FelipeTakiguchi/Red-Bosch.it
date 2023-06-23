import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumCardPageComponent } from './forum-card-page.component';

describe('ForumCardPageComponent', () => {
  let component: ForumCardPageComponent;
  let fixture: ComponentFixture<ForumCardPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ForumCardPageComponent]
    });
    fixture = TestBed.createComponent(ForumCardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
