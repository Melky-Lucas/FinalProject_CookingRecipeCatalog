import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { SlicePipe, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subject, combineLatest } from 'rxjs';
import { debounceTime, takeUntil, distinctUntilChanged } from 'rxjs/operators';
import { RecipeService } from '../../services/recipe.service';
import { CatalogService } from '../../services/catalog.service';
import { RecipeDto, IngredientDto, RecipeDifficulty } from '../../models/recipe.models';
import { RecipeCategoryDto } from '../../models/catalog.models';
import { difficultyLabel, formatTimeSpan } from '../../utils/recipe.utils';
import { trigger, transition, style, animate } from '@angular/animations';


interface Filters {
  title: string;
  categoryIds: number[];
  requiredIngredientIds: number[];
  optionalIngredientIds: number[];
  excludedIngredientIds: number[];
}

interface IngredientAutocomplete {
  required: { search: string; open: boolean };
  optional: { search: string; open: boolean };
  excluded: { search: string; open: boolean };
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, SlicePipe, FormsModule, NgClass],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, OnDestroy {
  private readonly destroy$ = new Subject<void>();
  private readonly titleDebounce$ = new Subject<string>();

  recipes: RecipeDto[] = [];
  pageNumber = 1;
  readonly pageSize = 9;
  loading = false;
  error = '';
  hasNextPage = false;
  totalResults = 0;

  categories: RecipeCategoryDto[] = [];
  ingredients: IngredientDto[] = [];
  categoriesLoading = false;
  ingredientsLoading = false;

  filters: Filters = {
    title: '',
    categoryIds: [],
    requiredIngredientIds: [],
    optionalIngredientIds: [],
    excludedIngredientIds: []
  };

  filtersCollapsed = true;

  autocomplete: IngredientAutocomplete = {
    required: { search: '', open: false },
    optional: { search: '', open: false },
    excluded: { search: '', open: false }
  };

  @ViewChild('reqInput') reqInputRef!: ElementRef;
  @ViewChild('optInput') optInputRef!: ElementRef;
  @ViewChild('excInput') excInputRef!: ElementRef;
  @ViewChild('titleInput') titleInputRef!: ElementRef;

  readonly difficultyLabel = difficultyLabel;
  readonly formatTimeSpan = formatTimeSpan;
  readonly RecipeDifficulty = RecipeDifficulty;

  constructor(
    private readonly recipeService: RecipeService,
    private readonly catalogService: CatalogService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.setupTitleDebounce();
    this.loadCatalogData();
    this.readFiltersFromUrl();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private setupTitleDebounce(): void {
    this.titleDebounce$
      .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        takeUntil(this.destroy$)
      )
      .subscribe(value => {
        this.filters.title = value;
        this.pageNumber = 1;
        this.syncFiltersToUrl();
        this.loadRecipes();
      });
  }

  private loadCatalogData(): void {
    this.categoriesLoading = true;
    this.ingredientsLoading = true;

    combineLatest([
      this.catalogService.getCategories(),
      this.catalogService.getIngredients()
    ])
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: ([cats, ings]) => {
          this.categories = cats.sort((a, b) => a.name.localeCompare(b.name));
          this.ingredients = ings.sort((a, b) => a.name.localeCompare(b.name));
          this.categoriesLoading = false;
          this.ingredientsLoading = false;
        },
        error: () => {
          this.categoriesLoading = false;
          this.ingredientsLoading = false;
        }
      });
  }

  private readFiltersFromUrl(): void {
    const qp = this.route.snapshot.queryParams;

    if (qp['title']) this.filters.title = qp['title'];
    if (qp['page']) this.pageNumber = parseInt(qp['page'], 10) || 1;
    if (qp['categories']) {
      this.filters.categoryIds = (Array.isArray(qp['categories']) ? qp['categories'] : [qp['categories']])
        .map((v: string) => parseInt(v, 10)).filter((v: number) => !isNaN(v));
    }
    if (qp['required']) {
      this.filters.requiredIngredientIds = (Array.isArray(qp['required']) ? qp['required'] : [qp['required']])
        .map((v: string) => parseInt(v, 10)).filter((v: number) => !isNaN(v));
    }
    if (qp['optional']) {
      this.filters.optionalIngredientIds = (Array.isArray(qp['optional']) ? qp['optional'] : [qp['optional']])
        .map((v: string) => parseInt(v, 10)).filter((v: number) => !isNaN(v));
    }
    if (qp['excluded']) {
      this.filters.excludedIngredientIds = (Array.isArray(qp['excluded']) ? qp['excluded'] : [qp['excluded']])
        .map((v: string) => parseInt(v, 10)).filter((v: number) => !isNaN(v));
    }

    this.loadRecipes();
  }

  syncFiltersToUrl(): void {
    const qp: Record<string, string | number | string[] | number[]> = {};
    if (this.filters.title) qp['title'] = this.filters.title;
    if (this.pageNumber > 1) qp['page'] = this.pageNumber;
    if (this.filters.categoryIds.length) qp['categories'] = this.filters.categoryIds;
    if (this.filters.requiredIngredientIds.length) qp['required'] = this.filters.requiredIngredientIds;
    if (this.filters.optionalIngredientIds.length) qp['optional'] = this.filters.optionalIngredientIds;
    if (this.filters.excludedIngredientIds.length) qp['excluded'] = this.filters.excludedIngredientIds;

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: qp,
      replaceUrl: true,
      queryParamsHandling: ''
    });
  }

  loadRecipes(): void {
    this.loading = true;
    this.error = '';

    this.recipeService
      .search({
        isPublic: true,
        pageSize: this.pageSize,
        pageNumber: this.pageNumber,
        title: this.filters.title || undefined,
        categoryIds: this.filters.categoryIds.length ? this.filters.categoryIds : undefined,
        requiredIngredientIds: this.filters.requiredIngredientIds.length ? this.filters.requiredIngredientIds : undefined,
        optionalIngredientIds: this.filters.optionalIngredientIds.length ? this.filters.optionalIngredientIds : undefined,
        excludedIngredientIds: this.filters.excludedIngredientIds.length ? this.filters.excludedIngredientIds : undefined
      })
      .subscribe({
        next: recipes => {
          this.recipes = recipes;
          this.hasNextPage = recipes.length === this.pageSize;
          this.totalResults = recipes.length < this.pageSize
            ? (this.pageNumber - 1) * this.pageSize + recipes.length
            : this.pageNumber * this.pageSize + (recipes.length === this.pageSize ? 1 : 0);
          this.loading = false;
        },
        error: () => {
          this.error = 'No se pudieron cargar las recetas. Verifique su conexión a Internet.';
          this.loading = false;
        }
      });
  }

  onTitleInput(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.titleDebounce$.next(value);
  }

  toggleCategory(categoryId: number): void {
    const idx = this.filters.categoryIds.indexOf(categoryId);
    if (idx >= 0) {
      this.filters.categoryIds.splice(idx, 1);
    } else {
      this.filters.categoryIds.push(categoryId);
    }
    this.onFiltersChanged();
  }

  onFiltersChanged(): void {
    this.pageNumber = 1;
    this.syncFiltersToUrl();
    this.loadRecipes();
  }

  getSelectedIngredientIds(): Set<number> {
    return new Set([
      ...this.filters.requiredIngredientIds,
      ...this.filters.optionalIngredientIds,
      ...this.filters.excludedIngredientIds
    ]);
  }

  filterIngredients(search: string, excludeIds: Set<number>): IngredientDto[] {
    const term = search.toLowerCase().trim();
    return this.ingredients.filter(ing => {
      if (excludeIds.has(ing.id)) return false;
      if (!term) return true;
      return ing.name.toLowerCase().includes(term);
    }).slice(0, 10);
  }

  openAutocomplete(field: keyof IngredientAutocomplete): void {
    this.autocomplete[field].open = true;
  }

  closeAutocomplete(field: keyof IngredientAutocomplete): void {
    setTimeout(() => {
      this.autocomplete[field].open = false;
      this.autocomplete[field].search = '';
    }, 150);
  }

  addIngredient(field: 'required' | 'optional' | 'excluded', ingredient: IngredientDto): void {
    const key = `${field}IngredientIds` as const;
    if (!this.filters[key].includes(ingredient.id)) {
      this.filters[key].push(ingredient.id);
      this.onFiltersChanged();
    }
    this.autocomplete[field].search = '';
    this.autocomplete[field].open = false;
  }

  removeIngredient(field: 'required' | 'optional' | 'excluded', ingredientId: number): void {
    const key = `${field}IngredientIds` as const;
    const idx = this.filters[key].indexOf(ingredientId);
    if (idx >= 0) {
      this.filters[key].splice(idx, 1);
      this.onFiltersChanged();
    }
  }

  getIngredientById(id: number): IngredientDto | undefined {
    return this.ingredients.find(i => i.id === id);
  }

  getCategoryById(id: number): RecipeCategoryDto | undefined {
    return this.categories.find(c => c.id === id);
  }

  clearTitleFilter(): void {
    this.filters.title = '';
    if (this.titleInputRef) {
      this.titleInputRef.nativeElement.value = '';
    }
    this.onFiltersChanged();
  }

  clearAllFilters(): void {
    this.filters = {
      title: '',
      categoryIds: [],
      requiredIngredientIds: [],
      optionalIngredientIds: [],
      excludedIngredientIds: []
    };
    if (this.titleInputRef) this.titleInputRef.nativeElement.value = '';
    if (this.reqInputRef) this.reqInputRef.nativeElement.value = '';
    if (this.optInputRef) this.optInputRef.nativeElement.value = '';
    if (this.excInputRef) this.excInputRef.nativeElement.value = '';
    this.onFiltersChanged();
  }

  hasActiveFilters(): boolean {
    return (
      !!this.filters.title ||
      this.filters.categoryIds.length > 0 ||
      this.filters.requiredIngredientIds.length > 0 ||
      this.filters.optionalIngredientIds.length > 0 ||
      this.filters.excludedIngredientIds.length > 0
    );
  }

  getActiveFiltersCount(): number {
    return (
      (this.filters.title ? 1 : 0) +
      this.filters.categoryIds.length +
      this.filters.requiredIngredientIds.length +
      this.filters.optionalIngredientIds.length +
      this.filters.excludedIngredientIds.length
    );
  }

  previousPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.syncFiltersToUrl();
      this.loadRecipes();
      window.scrollTo({ top: 0, behavior: 'smooth' });
    }
  }

  nextPage(): void {
    if (this.hasNextPage) {
      this.pageNumber++;
      this.syncFiltersToUrl();
      this.loadRecipes();
      window.scrollTo({ top: 0, behavior: 'smooth' });
    }
  }

  toggleFilters(): void {
    this.filtersCollapsed = !this.filtersCollapsed;
  }

  difficultyColor(difficulty: RecipeDifficulty): string {
    switch (difficulty) {
      case RecipeDifficulty.Easy: return 'success';
      case RecipeDifficulty.Medium: return 'warning';
      case RecipeDifficulty.Hard: return 'danger';
      default: return 'secondary';
    }
  }
}
