import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPresentComponent } from './edit-present.component';

describe('EditPresentComponent', () => {
  let component: EditPresentComponent;
  let fixture: ComponentFixture<EditPresentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditPresentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditPresentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
