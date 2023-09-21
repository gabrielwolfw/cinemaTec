import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterMovieComponent } from './register-movie.component';

describe('RegisterMovieComponent', () => {
  let component: RegisterMovieComponent;
  let fixture: ComponentFixture<RegisterMovieComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterMovieComponent]
    });
    fixture = TestBed.createComponent(RegisterMovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
