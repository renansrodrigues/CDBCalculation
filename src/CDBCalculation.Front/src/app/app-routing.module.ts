import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CdbCalculationComponent } from './features/cdb-calculation/cdb-calculation.component';

export const routes: Routes = [
  { path: '', component: CdbCalculationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
