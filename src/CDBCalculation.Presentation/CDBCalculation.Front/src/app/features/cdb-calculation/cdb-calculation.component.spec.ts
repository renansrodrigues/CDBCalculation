import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { CdbCalculationComponent } from './cdb-calculation.component';
import { CdbCalculationService } from '../../core/services/cdb-calculation.service';

describe('CdbCalculationComponent (Reactive Forms)', () => {
  let component: CdbCalculationComponent;
  let fixture: ComponentFixture<CdbCalculationComponent>;
  let serviceSpy: jasmine.SpyObj<CdbCalculationService>;

  beforeEach(() => {
    serviceSpy = jasmine.createSpyObj(
      'CdbCalculationService',
      ['calculate']
    );

    TestBed.configureTestingModule({
      imports: [CdbCalculationComponent],
      providers: [
        { provide: CdbCalculationService, useValue: serviceSpy }
      ]
    });

    fixture = TestBed.createComponent(CdbCalculationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should mark form invalid when empty', () => {
    expect(component.form.invalid).toBeTrue();
  });

  it('should not call service when form is invalid', () => {
    component.submit();
    expect(serviceSpy.calculate).not.toHaveBeenCalled();
  });

  it('should call service when form is valid', () => {
    serviceSpy.calculate.and.returnValue(
      of({ grossValue: 1123.08, netWorth: 898.47 })
    );

    component.form.setValue({
      initialValue: 1000,
      termMonths: 12
    });

    component.submit();

    expect(serviceSpy.calculate).toHaveBeenCalled();
    expect(component.result?.grossValue).toBe(1123.08);
    expect(component.result?.netWorth).toBe(898.47);
  });

  it('should handle api error', fakeAsync(() => {
    serviceSpy.calculate.and.returnValue(
      throwError(() => ({
        error: { error: 'An error occurred' }
      }))
    );

    component.form.setValue({
      initialValue: 1000,
      termMonths: 10
    });

    component.submit();
    tick();

    expect(serviceSpy.calculate).toHaveBeenCalled();
    expect(component.error).toBe('An error occurred');
    expect(component.loading).toBeFalse();
  }));
});
