import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrencyManagementPageComponent } from './currency-management-page.component';

describe('CurrencyManagementPageComponent', () => {
  let component: CurrencyManagementPageComponent;
  let fixture: ComponentFixture<CurrencyManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrencyManagementPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrencyManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
