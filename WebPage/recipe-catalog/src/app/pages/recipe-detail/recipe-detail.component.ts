import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RecipeService } from '../../services/recipe.service';
import { RecipeDto } from '../../models/recipe.models';
import { difficultyLabel, formatTimeSpan } from '../../utils/recipe.utils';

@Component({
  selector: 'app-recipe-detail',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.css'
})
export class RecipeDetailComponent implements OnInit {
  recipe: RecipeDto | null = null;
  loading = false;
  error = '';

  readonly difficultyLabel = difficultyLabel;
  readonly formatTimeSpan = formatTimeSpan;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly recipeService: RecipeService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loading = true;

    this.recipeService.getById(id).subscribe({
      next: recipe => {
        this.recipe = recipe;
        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo cargar la receta.';
        this.loading = false;
      }
    });
  }

  changeServings(increment: number): void {
    if (!this.recipe) {
      return;
    }

    const newServings = this.recipe.servings + increment;
    
    if (newServings <= 0 || newServings > 15) {
      return;
    }

    for (const ingredient of this.recipe.recipe_Ingredients) {
      ingredient.quantity = (ingredient.quantity / this.recipe.servings) * newServings;
    }

    this.recipe.servings = newServings;
  }
}
