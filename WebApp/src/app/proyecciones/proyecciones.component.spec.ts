import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProyeccionesComponent } from './proyecciones.component';

describe('ProyeccionesComponent', () => {
  let component: ProyeccionesComponent;
  let fixture: ComponentFixture<ProyeccionesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProyeccionesComponent]
    });
    fixture = TestBed.createComponent(ProyeccionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
