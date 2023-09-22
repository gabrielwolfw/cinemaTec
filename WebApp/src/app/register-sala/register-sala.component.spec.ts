import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterSalaComponent } from './register-sala.component';

describe('RegisterSalaComponent', () => {
  let component: RegisterSalaComponent;
  let fixture: ComponentFixture<RegisterSalaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterSalaComponent]
    });
    fixture = TestBed.createComponent(RegisterSalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
