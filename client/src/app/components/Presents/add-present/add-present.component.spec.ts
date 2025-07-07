import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPresentComponent } from './add-present.component';

describe('AddPresentComponent', () => {
  let component: AddPresentComponent;
  let fixture: ComponentFixture<AddPresentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPresentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPresentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
