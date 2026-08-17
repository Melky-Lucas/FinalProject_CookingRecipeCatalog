import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { RecipeCategoryDto } from '../models/catalog.models';
import { IngredientDto, MeasureUnitDto } from '../models/recipe.models';

@Injectable({ providedIn: 'root' })
export class CatalogService {
  constructor(private readonly http: HttpClient) {}

  getCategories() {
    return this.http.get<RecipeCategoryDto[]>(`${environment.apiUrl}/RecipeCategory`);
  }

  getIngredients() {
    return this.http.get<IngredientDto[]>(`${environment.apiUrl}/Ingredient`);
  }

  getMeasureUnits() {
    return this.http.get<MeasureUnitDto[]>(`${environment.apiUrl}/MeasureUnit`);
  }
}
