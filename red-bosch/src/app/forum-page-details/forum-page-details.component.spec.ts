import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumPageDetailsComponent } from './forum-page-details.component';

describe('ForumPageDetailsComponent', () => {
  let component: ForumPageDetailsComponent;
  let fixture: ComponentFixture<ForumPageDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ForumPageDetailsComponent]
    });
    fixture = TestBed.createComponent(ForumPageDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
