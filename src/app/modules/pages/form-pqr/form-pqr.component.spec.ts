import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormPqrComponent } from './form-pqr.component';

describe('FormPqrComponent', () => {
  let component: FormPqrComponent;
  let fixture: ComponentFixture<FormPqrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormPqrComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormPqrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
