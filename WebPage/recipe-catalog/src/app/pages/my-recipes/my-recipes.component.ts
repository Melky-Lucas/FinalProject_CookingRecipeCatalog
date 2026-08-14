import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RecipeService } from '../../services/recipe.service';
import { RecipeDto } from '../../models/recipe.models';
import { difficultyLabel } from '../../utils/recipe.utils';

@Component({
  selector: 'app-my-recipes',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './my-recipes.component.html',
  styleUrl: './my-recipes.component.css'
})
export class MyRecipesComponent implements OnInit {
  recipes: RecipeDto[] = [];
  pageNumber = 1;
  readonly pageSize = 10;
  loading = false;
  error = '';
  hasNextPage = false;
  recipeToDeleteId: number | null = null;

  readonly difficultyLabel = difficultyLabel;

  constructor(
    private readonly auth: AuthService,
    private readonly recipeService: RecipeService
  ) {}

  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes(): void {
    const userId = this.auth.getUserId();
    if (!userId) {
      return;
    }

    this.loading = true;
    this.error = '';

    this.recipeService
      .search({
        userId,
        pageSize: this.pageSize,
        pageNumber: this.pageNumber
      })
      .subscribe({
        next: recipes => {
          this.recipes = recipes;
          this.hasNextPage = recipes.length === this.pageSize;
          this.loading = false;
        },
        error: () => {
          this.error = 'No se pudieron cargar tus recetas.';
          this.loading = false;
        }
      });
  }

  setRecipeToDelete(id: number): void {
    this.recipeToDeleteId = id;
  }

  confirmDelete(): void {
    if (this.recipeToDeleteId !== null) {
      this.deleteRecipe(this.recipeToDeleteId);
      this.recipeToDeleteId = null;
    }
  }
  
  deleteRecipe(id: number): void {
    this.recipeService.delete(id).subscribe({
      next: () => this.loadRecipes(),
      error: () => {
        this.error = 'No se pudo eliminar la receta.';
      }
    });
  }

  previousPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadRecipes();
    }
  }

  nextPage(): void {
    if (this.hasNextPage) {
      this.pageNumber++;
      this.loadRecipes();
    }
  }
}
