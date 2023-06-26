import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateForumPageComponent } from './create-forum-page.component';

describe('CreateForumPageComponent', () => {
  let component: CreateForumPageComponent;
  let fixture: ComponentFixture<CreateForumPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateForumPageComponent]
    });
    fixture = TestBed.createComponent(CreateForumPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
