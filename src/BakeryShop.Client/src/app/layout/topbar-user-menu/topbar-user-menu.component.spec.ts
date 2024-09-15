import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopbarUserMenuComponent } from './topbar-user-menu.component';

describe('TopbarUserMenuComponent', () => {
  let component: TopbarUserMenuComponent;
  let fixture: ComponentFixture<TopbarUserMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopbarUserMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopbarUserMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
