import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { CatalogService } from '../../services/catalog.service';
import { RecipeService } from '../../services/recipe.service';
import { RecipeCategoryDto } from '../../models/catalog.models';
import {
  CreateRecipeDto,
  IngredientDto,
  MeasureUnitDto,
  RecipeDifficulty,
  RecipeDto,
  UpdateRecipeDto
} from '../../models/recipe.models';
import { difficultyLabel, minutesToTimeSpan, timeSpanToMinutes } from '../../utils/recipe.utils';
import { ProblemDetails } from '../../models/problem-details.model';
import { KeyValuePipe } from '@angular/common';
import { errorMessages } from '../../utils/form.utils';

interface IngredientFormValue {
  id?: number;
  ingredientId: number;
  quantity: number;
  unitId: number;
  isOptional: boolean;
}

interface StepFormValue {
  id?: number;
  stepNumber: number;
  title: string;
  estimatedMinutes: number;
  instruction: string;
}

@Component({
  selector: 'app-recipe-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, KeyValuePipe],
  templateUrl: './recipe-form.component.html',
  styleUrl: './recipe-form.component.css'
})
export class RecipeFormComponent implements OnInit {
  categories: RecipeCategoryDto[] = [];
  ingredients: IngredientDto[] = [];
  measureUnits: MeasureUnitDto[] = [];

  loading = true;
  saving = false;
  error = '';
  ApiErrorMessages: Record<string, string[]> = {};
  isEditMode = false;
  recipeId: number | null = null;
  originalCategoryIds: number[] = [];
  originalIngredients: IngredientFormValue[] = [];

  readonly difficulties = [
    { value: RecipeDifficulty.Easy, label: difficultyLabel(RecipeDifficulty.Easy) },
    { value: RecipeDifficulty.Medium, label: difficultyLabel(RecipeDifficulty.Medium) },
    { value: RecipeDifficulty.Hard, label: difficultyLabel(RecipeDifficulty.Hard) }
  ];

  readonly form = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(250)]],
    imageUrl: ['', [Validators.required, Validators.maxLength(250)]],
    preparationMinutes: [30, [Validators.required, Validators.min(1)]],
    cookingMinutes: [20, [Validators.required, Validators.min(0)]],
    servings: [4, [Validators.required, Validators.min(1)]],
    difficulty: [RecipeDifficulty.Easy, Validators.required],
    calories: [0, [Validators.required, Validators.min(0)]],
    isPublic: [true],
    categoryIds: [[] as number[]],
    ingredients: this.fb.array([] as FormGroup[]),
    steps: this.fb.array([] as FormGroup[])
  });

  errorMessages = errorMessages;

  constructor(
    private readonly fb: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly auth: AuthService,
    private readonly catalogService: CatalogService,
    private readonly recipeService: RecipeService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!idParam;
    this.recipeId = idParam ? Number(idParam) : null;

    forkJoin({
      categories: this.catalogService.getCategories(),
      ingredients: this.catalogService.getIngredients(),
      units: this.catalogService.getMeasureUnits()
    }).subscribe({
      next: ({ categories, ingredients, units }) => {
        this.categories = categories.sort((a, b) => a.name[0].charCodeAt(0) -  b.name[0].charCodeAt(0));
        this.ingredients = ingredients.sort((a, b) => a.name[0].charCodeAt(0) -  b.name[0].charCodeAt(0));
        this.measureUnits = units.sort((a, b) => a.name[0].charCodeAt(0) -  b.name[0].charCodeAt(0));

        if (this.isEditMode && this.recipeId) {
          this.loadRecipe(this.recipeId);
        } else {
          this.addIngredientRow();
          this.addStepRow();
          this.loading = false;
        }
      },
      error: () => {
        this.error = 'No se pudieron cargar los catálogos.';
        this.loading = false;
      }
    });
  }

  get ingredientsArray(): FormArray {
    return this.form.get('ingredients') as FormArray;
  }

  get stepsArray(): FormArray {
    return this.form.get('steps') as FormArray;
  }

  addIngredientRow(data?: IngredientFormValue): void {
    this.ingredientsArray.push(
      this.fb.group({
        id: [data?.id ?? null],
        ingredientId: [data?.ingredientId ?? (this.ingredients[0]?.id ?? 0), Validators.required],
        quantity: [data?.quantity ?? 1, [Validators.required, Validators.min(1)]],
        unitId: [data?.unitId ?? (this.measureUnits[0]?.id ?? 0), Validators.required],
        isOptional: [data?.isOptional ?? false]
      })
    );
  }

  removeIngredientRow(index: number): void {
    this.ingredientsArray.removeAt(index);
  }

  addStepRow(data?: StepFormValue): void {
    this.stepsArray.push(
      this.fb.group({
        id: [data?.id ?? null],
        stepNumber: [data?.stepNumber ?? this.stepsArray.length + 1, Validators.required],
        title: [data?.title ?? '', Validators.required],
        estimatedMinutes: [data?.estimatedMinutes ?? 5, [Validators.required, Validators.min(1)]],
        instruction: [data?.instruction ?? '', Validators.required]
      })
    );
  }

  removeStepRow(index: number): void {
    this.stepsArray.removeAt(index);
    this.reindexSteps();
  }

  isFieldInvalid(fieldName: string): boolean {
    const control = this.form.get(fieldName);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  isIngredientFieldInvalid(index: number, fieldName: string): boolean {
    const control = this.form.get(['ingredients', index])?.get(fieldName);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  isStepsFieldInvalid(index: number, fieldName: string): boolean {
    const control = this.form.get(['steps', index])?.get(fieldName);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  getErrors(fieldName: string) {
    const control = this.form.get(fieldName);
    return control?.errors ? Object.keys(control.errors) : [];
  }

  getIngredientErrors(index: number, fieldName: string) {
    const control = this.form.get(['ingredients', index])?.get(fieldName);
    return control?.errors ? Object.keys(control.errors) : [];
  }

  getStepsErrors(index: number, fieldName: string) {
    const control = this.form.get(['steps', index])?.get(fieldName);
    return control?.errors ? Object.keys(control.errors) : [];
  }

  submit(): void {
    if (this.form.invalid || this.ingredientsArray.length === 0 || this.stepsArray.length === 0) {
      this.form.markAllAsTouched();
      this.error = 'Complete correctamente los campos obligatorios.';

      return;
    }

    this.saving = true;
    this.error = '';

    if (this.isEditMode && this.recipeId) {
      this.updateRecipe(this.recipeId);
    } else {
      this.createRecipe();
    }
  }

  private loadRecipe(id: number): void {
    this.recipeService.getById(id).subscribe({
      next: recipe => {
        this.patchForm(recipe);
        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo cargar la receta.';
        this.loading = false;
      }
    });
  }

  private patchForm(recipe: RecipeDto): void {
    const categoryIds = this.categories
      .filter(category => recipe.categoryNames.includes(category.name))
      .map(category => category.id);

    this.originalCategoryIds = [...categoryIds];
    this.originalIngredients = recipe.recipe_Ingredients.map(item => ({
      id: item.id,
      ingredientId: item.ingredient.id,
      quantity: item.quantity,
      unitId: item.unit.id,
      isOptional: item.isOptional
    }));

    this.form.patchValue({
      title: recipe.title,
      description: recipe.description,
      imageUrl: recipe.imageUrl,
      preparationMinutes: timeSpanToMinutes(recipe.preparationTime),
      cookingMinutes: timeSpanToMinutes(recipe.cookingTime),
      servings: recipe.servings,
      difficulty: recipe.difficulty,
      calories: recipe.calories,
      isPublic: recipe.isPublic,
      categoryIds
    });

    this.ingredientsArray.clear();
    this.originalIngredients.forEach(item => this.addIngredientRow(item));

    this.stepsArray.clear();
    recipe.cookingSteps
      .sort((a, b) => a.stepNumber - b.stepNumber)
      .forEach(step =>
        this.addStepRow({
          id: step.id,
          stepNumber: step.stepNumber,
          title: step.title,
          estimatedMinutes: timeSpanToMinutes(step.estimatedDuration),
          instruction: step.instruction
        })
      );
  }

  get hasApiErrors(): boolean {
    return this.ApiErrorMessages && Object.keys(this.ApiErrorMessages).length > 0;
  }


  private createRecipe(): void {
    const userId = this.auth.getUserId();
    if (!userId) {
      this.error = 'Sesión no válida.';
      this.saving = false;
      return;
    }

    const payload = this.buildCreatePayload(userId);
    this.recipeService.create(payload).subscribe({
      next: () => this.router.navigate(['/mis-recetas']),
      error: (problem: ProblemDetails) => {
        if (problem.errors === null) {
          this.error = problem.detail || 'No se pudo crear la receta.';
        }
        else {
          this.ApiErrorMessages = problem.errors || { };

          if (Object.keys(this.ApiErrorMessages).length === 0) {
            this.ApiErrorMessages = { ...this.ApiErrorMessages, [problem.title || 'Ocurrió un error inesperado.']: [] };
          }
        }

        this.saving = false;
      },
      complete: () => {
        this.saving = false;
      }
    });
  }

  private updateRecipe(id: number): void {
    const payload: UpdateRecipeDto = {
      id,
      title: this.form.value.title!,
      description: this.form.value.description!,
      imageUrl: this.form.value.imageUrl!,
      preparationTime: minutesToTimeSpan(Number(this.form.value.preparationMinutes)),
      cookingTime: minutesToTimeSpan(Number(this.form.value.cookingMinutes)),
      servings: Number(this.form.value.servings),
      difficulty: Number(this.form.value.difficulty),
      calories: Number(this.form.value.calories),
      isPublic: !!this.form.value.isPublic
    };

    this.recipeService.update(id, payload).subscribe({
      next: () => this.syncRelations(id),
      error: (problem: ProblemDetails) => {
        if (problem.errors === null) {
          this.error = problem.detail || 'No se pudo actualizar la receta.';
        }
        else {
          this.ApiErrorMessages = problem.errors || { };

          if (Object.keys(this.ApiErrorMessages).length === 0) {
            this.ApiErrorMessages = { ...this.ApiErrorMessages, [problem.title || 'Ocurrió un error inesperado.']: [] };
          }
        }

        this.saving = false;
      },
      complete: () => {
        this.saving = false;
      }
    });
  }

  private syncRelations(recipeId: number): void {
    const selectedCategoryIds = (this.form.value.categoryIds ?? []) as number[];
    const currentIngredients = this.ingredientsArray.getRawValue() as IngredientFormValue[];
    const currentSteps = this.stepsArray.getRawValue() as StepFormValue[];

    const categoriesToAdd = selectedCategoryIds.filter(id => !this.originalCategoryIds.includes(id));
    const categoriesToRemove = this.originalCategoryIds.filter(id => !selectedCategoryIds.includes(id));

    const ingredientOps = this.buildIngredientOperations(recipeId, currentIngredients);
    const categoryOps = [
      ...categoriesToAdd.map(id => this.recipeService.addCategory(recipeId, id)),
      ...categoriesToRemove.map(id => this.recipeService.removeCategory(recipeId, id))
    ];

    forkJoin([
      ...categoryOps,
      ...ingredientOps,
      this.recipeService.updateSteps(
        recipeId,
        currentSteps.map(step => ({
          id: step.id,
          stepNumber: step.stepNumber,
          title: step.title,
          estimatedDuration: minutesToTimeSpan(step.estimatedMinutes),
          instruction: step.instruction
        }))
      )
    ]).subscribe({
      next: () => this.router.navigate(['/mis-recetas']),
      error: (problem: ProblemDetails) => {
        console.error('Error al sincronizar relaciones:', problem);
        this.error = problem.detail || 'No se pudo actualizar la receta.';
        this.ApiErrorMessages = problem.errors || {};

        if (Object.keys(this.ApiErrorMessages).length === 0) {
          this.ApiErrorMessages = { ...this.ApiErrorMessages, [problem.title || 'Ocurrió un error inesperado.']: [] };
        }
        
        this.saving = false;
      },
      complete: () => {
        this.saving = false;
      }
    });
  }

  private buildIngredientOperations(recipeId: number, current: IngredientFormValue[]) {
    const ops = [];
    const currentIds = new Set(current.filter(item => item.id).map(item => item.id!));

    for (const original of this.originalIngredients) {
      if (original.id && !currentIds.has(original.id)) {
        ops.push(this.recipeService.removeIngredient(recipeId, original.id));
      }
    }

    for (const item of current) {
      if (item.id) {
        ops.push(
          this.recipeService.updateIngredient(recipeId, {
            id: item.id,
            ingredientId: Number(item.ingredientId),
            quantity: Number(item.quantity),
            unitId: Number(item.unitId),
            isOptional: !!item.isOptional
          })
        );
      } else {
        ops.push(
          this.recipeService.addIngredient(recipeId, {
            ingredientId: Number(item.ingredientId),
            quantity: Number(item.quantity),
            unitId: Number(item.unitId),
            isOptional: !!item.isOptional
          })
        );
      }
    }

    return ops;
  }

  private buildCreatePayload(userId: number): CreateRecipeDto {
    const ingredients = this.ingredientsArray.getRawValue() as IngredientFormValue[];
    const steps = this.stepsArray.getRawValue() as StepFormValue[];

    return {
      title: this.form.value.title!,
      description: this.form.value.description!,
      imageUrl: this.form.value.imageUrl!,
      preparationTime: minutesToTimeSpan(Number(this.form.value.preparationMinutes)),
      cookingTime: minutesToTimeSpan(Number(this.form.value.cookingMinutes)),
      servings: Number(this.form.value.servings),
      difficulty: Number(this.form.value.difficulty),
      calories: Number(this.form.value.calories),
      userId,
      isPublic: !!this.form.value.isPublic,
      category_Ids: (this.form.value.categoryIds ?? []) as number[],
      recipe_Ingredients: ingredients.map(item => ({
        ingredientId: Number(item.ingredientId),
        quantity: Number(item.quantity),
        unitId: Number(item.unitId),
        isOptional: !!item.isOptional
      })),
      cookingSteps: steps.map(step => ({
        stepNumber: Number(step.stepNumber),
        title: step.title,
        estimatedDuration: minutesToTimeSpan(step.estimatedMinutes),
        instruction: step.instruction
      }))
    };
  }

  private reindexSteps(): void {
    this.stepsArray.controls.forEach((control, index) => {
      control.get('stepNumber')?.setValue(index + 1);
    });
  }

  toggleCategory(categoryId: number, event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;
    const current = [...((this.form.value.categoryIds ?? []) as number[])];

    if (checked && !current.includes(categoryId)) {
      current.push(categoryId);
    } else if (!checked) {
      const index = current.indexOf(categoryId);
      if (index >= 0) {
        current.splice(index, 1);
      }
    }

    this.form.patchValue({ categoryIds: current });
  }

  isCategorySelected(categoryId: number): boolean {
    return ((this.form.value.categoryIds ?? []) as number[]).includes(categoryId);
  }
}
