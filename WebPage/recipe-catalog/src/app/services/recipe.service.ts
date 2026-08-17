import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {
  CreateRecipeDto,
  CreateRecipeIngredientDto,
  RecipeDto,
  RecipeSearchQuery,
  UpdateRecipeDto,
  UpdateRecipeIngredientDto,
  UpdateRecipeStepDto
} from '../models/recipe.models';

@Injectable({ providedIn: 'root' })
export class RecipeService {
  private readonly baseUrl = `${environment.apiUrl}/Recipe`;

  constructor(private readonly http: HttpClient) {}

  search(query: RecipeSearchQuery) {
    let params = new HttpParams()
      .set('PageSize', query.pageSize)
      .set('PageNumber', query.pageNumber);

    if (query.title) {
      params = params.set('Title', query.title);
    }
    if (query.userId != null) {
      params = params.set('UserId', query.userId);
    }
    if (query.isPublic != null) {
      params = params.set('IsPublic', query.isPublic);
    }
    if (query.categoryIds?.length) {
      query.categoryIds.forEach(id => {
        params = params.append('CategoryIds', id);
      });
    }
    if (query.requiredIngredientIds?.length) {
      query.requiredIngredientIds.forEach(id => {
        params = params.append('RequiredIngredientIds', id);
      });
    }
    if (query.optionalIngredientIds?.length) {
      query.optionalIngredientIds.forEach(id => {
        params = params.append('OptionalIngredientIds', id);
      });
    }
    if (query.excludedIngredientIds?.length) {
      query.excludedIngredientIds.forEach(id => {
        params = params.append('ExcludedIngredientIds', id);
      });
    }

    return this.http.get<RecipeDto[]>(this.baseUrl, { params });
  }

  getById(id: number) {
    return this.http.get<RecipeDto>(`${this.baseUrl}/${id}`);
  }

  create(recipe: CreateRecipeDto) {
    return this.http.post<RecipeDto>(this.baseUrl, recipe);
  }

  update(id: number, recipe: UpdateRecipeDto) {
    return this.http.put<RecipeDto>(`${this.baseUrl}/${id}`, recipe);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  updateSteps(recipeId: number, steps: UpdateRecipeStepDto[]) {
    return this.http.patch(`${this.baseUrl}/${recipeId}/Steps`, steps);
  }

  addCategory(recipeId: number, categoryId: number) {
    return this.http.post(`${this.baseUrl}/${recipeId}/Category/${categoryId}`, null);
  }

  removeCategory(recipeId: number, categoryId: number) {
    return this.http.delete(`${this.baseUrl}/${recipeId}/Category/${categoryId}`);
  }

  addIngredient(recipeId: number, ingredient: CreateRecipeIngredientDto) {
    return this.http.post(`${this.baseUrl}/${recipeId}/Ingredient`, ingredient);
  }

  updateIngredient(recipeId: number, ingredient: UpdateRecipeIngredientDto) {
    return this.http.put(`${this.baseUrl}/${recipeId}/Ingredient`, ingredient);
  }

  removeIngredient(recipeId: number, recipeIngredientId: number) {
    return this.http.delete(`${this.baseUrl}/${recipeId}/Ingredient/${recipeIngredientId}`);
  }
}
