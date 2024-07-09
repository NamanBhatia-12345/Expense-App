import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditExpenseComponent } from './edit-expense.component';

describe('EditExpenseComponent', () => {
  let component: EditExpenseComponent;
  let fixture: ComponentFixture<EditExpenseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditExpenseComponent]
    });
    fixture = TestBed.createComponent(EditExpenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
