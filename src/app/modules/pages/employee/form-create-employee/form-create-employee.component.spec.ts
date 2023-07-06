import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormCreateEmployeeComponent } from './form-create-employee.component';

describe('FormCreateEmployeeComponent', () => {
  let component: FormCreateEmployeeComponent;
  let fixture: ComponentFixture<FormCreateEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormCreateEmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormCreateEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
