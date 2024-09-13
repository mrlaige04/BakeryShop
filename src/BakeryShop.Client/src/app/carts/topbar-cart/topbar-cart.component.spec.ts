import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopbarCartComponent } from './topbar-cart.component';

describe('TopbarCartComponent', () => {
  let component: TopbarCartComponent;
  let fixture: ComponentFixture<TopbarCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopbarCartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopbarCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
