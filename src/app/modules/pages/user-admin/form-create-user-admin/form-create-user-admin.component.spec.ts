import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormCreateUserAdminComponent } from './form-create-user-admin.component';

describe('FormCreateUserAdminComponent', () => {
  let component: FormCreateUserAdminComponent;
  let fixture: ComponentFixture<FormCreateUserAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormCreateUserAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormCreateUserAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
