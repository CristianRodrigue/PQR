import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormCreateAssignComponent } from './form-create-assign.component';

describe('FormCreateAssignComponent', () => {
  let component: FormCreateAssignComponent;
  let fixture: ComponentFixture<FormCreateAssignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormCreateAssignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormCreateAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
