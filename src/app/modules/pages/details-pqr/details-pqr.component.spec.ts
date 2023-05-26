import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsPqrComponent } from './details-pqr.component';

describe('DetailsPqrComponent', () => {
  let component: DetailsPqrComponent;
  let fixture: ComponentFixture<DetailsPqrComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsPqrComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsPqrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
