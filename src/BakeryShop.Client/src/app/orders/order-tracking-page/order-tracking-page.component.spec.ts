import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderTrackingPageComponent } from './order-tracking-page.component';

describe('OrderTrackingPageComponent', () => {
  let component: OrderTrackingPageComponent;
  let fixture: ComponentFixture<OrderTrackingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderTrackingPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderTrackingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
