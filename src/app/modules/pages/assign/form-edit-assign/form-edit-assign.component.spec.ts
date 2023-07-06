import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormEditAssignComponent } from './form-edit-assign.component';

describe('FormEditAssignComponent', () => {
  let component: FormEditAssignComponent;
  let fixture: ComponentFixture<FormEditAssignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormEditAssignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormEditAssignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
