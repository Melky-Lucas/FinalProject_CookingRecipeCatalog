export enum RecipeDifficulty {
  Easy = 0,
  Medium = 1,
  Hard = 2
}

export interface UserDto {
  id: number;
  username: string;
  email: string;
  roleName: string;
}

export interface MeasureUnitDto {
  id: number;
  name: string;
  abbreviation: string;
}

export interface IngredientDto {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  categoryName: string;
}

export interface RecipeIngredientDto {
  id: number;
  quantity: number;
  isOptional: boolean;
  ingredient: IngredientDto;
  unit: MeasureUnitDto;
}

export interface RecipeCookingStepDto {
  id: number;
  stepNumber: number;
  title: string;
  estimatedDuration: string;
  instruction: string;
}

export interface RecipeTipDto {
  id: number;
  content: string;
  userId: number;
}

export interface RecipeDto {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  preparationTime: string;
  cookingTime: string;
  servings: number;
  difficulty: RecipeDifficulty;
  calories: number;
  isPublic: boolean;
  recipe_Ingredients: RecipeIngredientDto[];
  categoryNames: string[];
  cookingSteps: RecipeCookingStepDto[];
  tips: RecipeTipDto[];
  user: UserDto;
}

export interface CreateRecipeIngredientDto {
  ingredientId: number;
  quantity: number;
  unitId: number;
  isOptional: boolean;
}

export interface CreateRecipeStepDto {
  stepNumber: number;
  title: string;
  estimatedDuration: string;
  instruction: string;
}

export interface CreateRecipeDto {
  title: string;
  description: string;
  imageUrl: string;
  preparationTime: string;
  cookingTime: string;
  servings: number;
  difficulty: RecipeDifficulty;
  calories: number;
  userId: number;
  isPublic: boolean;
  category_Ids: number[];
  recipe_Ingredients: CreateRecipeIngredientDto[];
  cookingSteps: CreateRecipeStepDto[];
}

export interface UpdateRecipeDto {
  id: number;
  title: string;
  description: string;
  imageUrl: string;
  preparationTime: string;
  cookingTime: string;
  servings: number;
  difficulty: RecipeDifficulty;
  calories: number;
  isPublic: boolean;
}

export interface UpdateRecipeStepDto {
  id?: number;
  stepNumber: number;
  title: string;
  estimatedDuration: string;
  instruction: string;
}

export interface UpdateRecipeIngredientDto {
  id: number;
  ingredientId: number;
  quantity: number;
  unitId: number;
  isOptional: boolean;
}

export interface RecipeSearchQuery {
  title?: string;
  userId?: number;
  categoryIds?: number[];
  requiredIngredientIds?: number[];
  optionalIngredientIds?: number[];
  excludedIngredientIds?: number[];
  isPublic?: boolean;
  pageSize: number;
  pageNumber: number;
}
