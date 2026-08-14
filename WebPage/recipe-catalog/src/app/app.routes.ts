import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'recetas/:id',
    loadComponent: () => import('./pages/recipe-detail/recipe-detail.component').then(m => m.RecipeDetailComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'registro',
    loadComponent: () => import('./pages/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'mis-recetas',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/my-recipes/my-recipes.component').then(m => m.MyRecipesComponent)
  },
  {
    path: 'mis-recetas/nueva',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/recipe-form/recipe-form.component').then(m => m.RecipeFormComponent)
  },
  {
    path: 'mis-recetas/:id/editar',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/recipe-form/recipe-form.component').then(m => m.RecipeFormComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
