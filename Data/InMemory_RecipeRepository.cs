using Core.Model;
using Core.Interfaces;

namespace Data
{
    public class InMemory_RecipeRepository : IRecipeRepository
    {
        private List<Recipe> Recipes { get; set; } = [];

        public InMemory_RecipeRepository()
        {
            Recipes.Add(new Recipe
            {
                Id = 1,
                Title = "Spaghetti Carbonara",
                Description = "A classic Italian pasta dish made with eggs, cheese, pancetta, and pepper.",
                Ingredients = "200g spaghetti, 100g pancetta, 2 large eggs, 50g grated Parmesan cheese, salt, black pepper",
                Instructions = "1. Cook the spaghetti in salted boiling water until al dente. 2. In a pan, cook the pancetta until crispy. 3. In a bowl, whisk together the eggs and Parmesan cheese. 4. Drain the spaghetti and return it to the pot. 5. Add the pancetta and egg mixture to the spaghetti and toss quickly to combine. 6. Season with salt and black pepper to taste.",
                Tips = "Serve immediately for the best flavor."
            });
            Recipes.Add(new Recipe
            {
                Id = 2,
                Title = "Chicken Curry",
                Description = "A flavorful and spicy chicken curry made with a blend of spices and coconut milk.",
                Ingredients = "500g chicken breast, 1 onion, 2 cloves garlic, 1 tbsp curry powder, 400ml coconut milk, salt, pepper",
                Instructions = "1. Sauté the chopped onion and garlic in a pan until translucent. 2. Add the chicken pieces and cook until browned. 3. Stir in the curry powder and cook for another minute. 4. Pour in the coconut milk and bring to a simmer. 5. Cook until the chicken is cooked through and the sauce has thickened. 6. Season with salt and pepper to taste.",
                Tips = "Serve with steamed rice or naan bread."
            });
        }

        public List<Recipe> GetAll()
        {
            return Recipes;
        }

        public Recipe GetById(int id)
        {
            return Recipes.FirstOrDefault(r => r.Id == id) ?? throw new InvalidOperationException($"The recipe with this ID({id}) doesn't exist");
        }

        public void Add(Recipe recipe)
        {
            recipe.Id = Recipes.Count > 0 ? Recipes.Max(r => r.Id) + 1 : 1;
            Recipes.Add(recipe);
        }

        public void Update(Recipe updatedRecipe)
        {
            var existingRecipe = GetById(updatedRecipe.Id);
            existingRecipe.Title = updatedRecipe.Title;
            existingRecipe.Description = updatedRecipe.Description;
            existingRecipe.Ingredients = updatedRecipe.Ingredients;
            existingRecipe.Instructions = updatedRecipe.Instructions;
            existingRecipe.Tips = updatedRecipe.Tips;
        }

        public void Delete(int id)
        {
            var recipeToDelete = GetById(id);
            Recipes.Remove(recipeToDelete);
        }
    }
}
