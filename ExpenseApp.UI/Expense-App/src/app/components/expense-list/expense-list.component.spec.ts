import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseListComponent } from './expense-list.component';

describe('ExpenseListComponent', () => {
  let component: ExpenseListComponent;
  let fixture: ComponentFixture<ExpenseListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExpenseListComponent]
    });
    fixture = TestBed.createComponent(ExpenseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
