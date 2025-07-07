import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPresentComponent } from './show-present.component';

describe('ShowPresentComponent', () => {
  let component: ShowPresentComponent;
  let fixture: ComponentFixture<ShowPresentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowPresentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowPresentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
