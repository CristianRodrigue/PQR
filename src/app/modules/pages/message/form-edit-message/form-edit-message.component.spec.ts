import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormEditMessageComponent } from './form-edit-message.component';

describe('FormEditMessageComponent', () => {
  let component: FormEditMessageComponent;
  let fixture: ComponentFixture<FormEditMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormEditMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormEditMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
