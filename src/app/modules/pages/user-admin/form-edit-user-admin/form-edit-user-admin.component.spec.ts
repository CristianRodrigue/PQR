import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormEditUserAdminComponent } from './form-edit-user-admin.component';

describe('FormEditUserAdminComponent', () => {
  let component: FormEditUserAdminComponent;
  let fixture: ComponentFixture<FormEditUserAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormEditUserAdminComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormEditUserAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
