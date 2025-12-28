import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CdbCalculationService } from '../../core/services/cdb-calculation.service';
import { CdbCalculationResponse } from '../../models/cdb-calculation-response.model';

@Component({
  selector: 'app-cdb-calculation',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './cdb-calculation.component.html',
  styleUrls: ['./cdb-calculation.component.css']
})
export class CdbCalculationComponent implements OnInit {

  form!: FormGroup;
  result?: CdbCalculationResponse;
  error?: string;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private service: CdbCalculationService
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      redemptionValue: [
        null,
        [Validators.required, Validators.min(0.01)]
      ],
      termMonths: [
        null,
        [Validators.required, Validators.min(2)]
      ]
    });
  }

  formatCurrency(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value;
    
    
    const numbersOnly = value.replace(/\D/g, '');
    
    if (numbersOnly === '') {
      this.form.patchValue({ redemptionValue: null }, { emitEvent: false });
      input.value = '';
      return;
    }

    
    const numericValue = parseFloat(numbersOnly) / 100;
    
    
    this.form.patchValue({ redemptionValue: numericValue }, { emitEvent: false });
    
    
    const formatted = new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }).format(numericValue);
    
    input.value = formatted;
  }

  onCurrencyBlur(event: Event): void {
    const input = event.target as HTMLInputElement;
    const value = this.form.get('redemptionValue')?.value;
    
    if (value === null || value === undefined || isNaN(value)) {
      input.value = '';
      return;
    }

    
    const formatted = new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }).format(value);
    
    input.value = formatted;
  }

  onCurrencyFocus(event: Event): void {
    const input = event.target as HTMLInputElement;
    const value = this.form.get('redemptionValue')?.value;
    
    if (value !== null && value !== undefined && !isNaN(value)) {
      
      
      const numericString = value.toFixed(2).replace('.', ',');
      input.value = numericString;
    } else {
      input.value = '';
    }
  }

  submit(): void {
    this.error = undefined;
    this.result = undefined;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    

    this.service.calculate(this.form.value)
      .subscribe({
        next: res => {
          this.result = res;
          this.loading = false;          
        },
        error: err => {
          this.error = err.error?.error ?? 'Unexpected error';
          this.loading = false;
        }
      });
  }

  get f() {
    return this.form?.controls || {};
  }
}
