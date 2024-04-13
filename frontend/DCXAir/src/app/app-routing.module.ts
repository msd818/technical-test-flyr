import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './view/components/home/home.component';
import { SearchComponent } from './view/components/search/search.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    children: [
      { path: '', component: HomeComponent, title: 'Inicio' },
    ]
  },
  {
    path: 'search',
    children: [
      { path: '', component: SearchComponent, title: 'Busqueda' },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
