using Core.Models;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static void InitializeDB(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<RecipeCatalogDBContext>();

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return; // The db has data
            }

            try
            {
                context.Database.BeginTransaction();

                // User Roles
                var adminRole = new Role() { Name = "Admin" };
                var userRole = new Role() { Name = "User" };

                context.Roles.AddRange(adminRole, userRole);

                // Recipe Categories
                var breakfastCat = new RecipeCategory() { Name = "Desayunos", Description = "Platos y preparaciones ideales para comenzar el día con energía." };
                var lunchCat = new RecipeCategory() { Name = "Almuerzos", Description = "Comidas principales y contundentes para la mitad del día." };
                var dinnerCat = new RecipeCategory() { Name = "Cenas", Description = "Opciones más ligeras y reconfortantes para cerrar la jornada." };
                var appetizersCat = new RecipeCategory() { Name = "Entradas y Aperitivos", Description = "Bocados, tapas y platos pequeños para abrir el apetito antes de comer." };
                var saladsCat = new RecipeCategory() { Name = "Ensaladas", Description = "Preparaciones frescas a base de vegetales, frutas, proteínas y aderezos." };
                var soupsCat = new RecipeCategory() { Name = "Sopas y Cremas", Description = "Caldos, sopas y cremas ideales para entrar en calor o como primer plato." };
                var mainDishesCat = new RecipeCategory() { Name = "Platos Principales", Description = "Recetas estelares que sirven como eje central de un menú o comida." };
                var sidesCat = new RecipeCategory() { Name = "Guarniciones", Description = "Acompañamientos perfectos para complementar cualquier plato fuerte." };
                var dessertsCat = new RecipeCategory() { Name = "Postres", Description = "Platos dulces y repostería para el cierre ideal de una comida." };
                var drinksCat = new RecipeCategory() { Name = "Bebidas y Cocteles", Description = "Jugos, licuados, infusiones y bebidas preparadas con o sin alcohol." };
                var fastFoodCat = new RecipeCategory() { Name = "Comida Rápida", Description = "Alternativas prácticas y deliciosas como hamburguesas, pizzas y snacks." };
                var vegetarianCat = new RecipeCategory() { Name = "Vegetarianas", Description = "Recetas libres de carnes, enfocadas en vegetales, granos y lácteos." };
                var veganCat = new RecipeCategory() { Name = "Veganas", Description = "Preparaciones 100% de origen vegetal, sin ningún ingrediente animal." };
                var glutenFreeCat = new RecipeCategory() { Name = "Sin Gluten", Description = "Opciones adaptadas y seguras para personas con intolerancia al gluten." };
                var fitnessCat = new RecipeCategory() { Name = "Fitness y Saludables", Description = "Recetas equilibradas, bajas en calorías o altas en proteína orientadas al bienestar." };
                var typicalFoodCat = new RecipeCategory() { Name = "Comida Típica", Description = "Platos tradicionales y emblemáticos de la gastronomía criolla y local." };
                var bakeryCat = new RecipeCategory() { Name = "Repostería y Panadería", Description = "Masas, panes, pasteles y galletas elaborados en casa." };
                var snacksCat = new RecipeCategory() { Name = "Bocadillos y Snacks", Description = "Opciones prácticas para picar entre horas o llevar a cualquier lugar." };

                // Ingredient Categories
                var meatAndPoultryCat = new IngredientCategory() { Name = "Carnes y Aves", Description = "Cortes de res, cerdo, pollo y otras aves frescos o congelados." };
                var fishAndSeafoodCat = new IngredientCategory() { Name = "Pescados y Mariscos", Description = "Variedad de pescados frescos, camarones, mariscos y productos del mar." };
                var fruitsAndVegetablesCat = new IngredientCategory() { Name = "Frutas y Verduras", Description = "Productos agrícolas frescos, vegetales de hoja, tubérculos y frutas de temporada." };
                var dairyAndEggsCat = new IngredientCategory() { Name = "Lácteos y Huevos", Description = "Leche, quesos, mantequilla, yogures y huevos frescos." };
                var grainsCerealsAndPastaCat = new IngredientCategory() { Name = "Granos, Cereales y Pasta", Description = "Arroz, avena, pastas, harinas y otros cereales base para la cocina." };
                var legumesCat = new IngredientCategory() { Name = "Legumbres", Description = "Frijoles, lentejas, garbanzos y otras leguminosas secas o enlatadas." };
                var spicesHerbsAndCondimentsCat = new IngredientCategory() { Name = "Especias, Hierbas y Condimentos", Description = "Sazonadores, hierbas aromáticas, sales, pimientas y polvos para dar sabor." };
                var oilsVinegarsAndSaucesCat = new IngredientCategory() { Name = "Aceites, Vinagres y Salsas", Description = "Aceites de cocina, vinagres, aderezos y salsas preparadas." };
                var bakeryAndPastryCat = new IngredientCategory() { Name = "Panadería y Repostería", Description = "Panes, levaduras, azúcares, cacao y otros insumos para hornear." };
                var nutsAndSeedsCat = new IngredientCategory() { Name = "Frutos Secos y Semillas", Description = "Nueces, almendras, maní, chía, linaza y semillas complementarias." };

                // Add recipe and ingredient categories to the context so they get generated Ids
                context.RecipeCategories.AddRange(
                    breakfastCat, lunchCat, dinnerCat, appetizersCat, saladsCat, soupsCat, mainDishesCat, sidesCat,
                    dessertsCat, drinksCat, fastFoodCat, vegetarianCat, veganCat, glutenFreeCat, fitnessCat, typicalFoodCat,
                    bakeryCat, snacksCat
                );

                context.IngredientCategories.AddRange(
                    meatAndPoultryCat, fishAndSeafoodCat, fruitsAndVegetablesCat, dairyAndEggsCat, grainsCerealsAndPastaCat,
                    legumesCat, spicesHerbsAndCondimentsCat, oilsVinegarsAndSaucesCat, bakeryAndPastryCat, nutsAndSeedsCat
                );

                // Persist categories first to ensure their Ids are set for FK assignments
                context.SaveChanges();

                // Ingredients
                var ChickenBreast = new Ingredient()
                {
                    Name = "Pechuga de Pollo",
                    Description = "Corte magro de pollo, versátil para ensaladas, plancha o guisos.",
                    ImageUrl = "https://example.com/images/ingredients/pechuga-pollo.jpg",
                    IngredientCategoryId = meatAndPoultryCat.Id
                };

                var GroundBeef = new Ingredient()
                {
                    Name = "Carne Molida de Res",
                    Description = "Carne picada ideal para albóndigas, hamburguesas o salsas boloñesas.",
                    ImageUrl = "https://example.com/images/ingredients/carne-molida.jpg",
                    IngredientCategoryId = meatAndPoultryCat.Id
                };

                var SalmonFillet = new Ingredient()
                {
                    Name = "Filete de Salmón",
                    Description = "Pescado azul rico en omega-3, perfecto para hornear o cocinar a la parrilla.",
                    ImageUrl = "https://example.com/images/ingredients/salmon.jpg",
                    IngredientCategoryId = fishAndSeafoodCat.Id
                };

                var Shrimp = new Ingredient()
                {
                    Name = "Camarones",
                    Description = "Mariscos versátiles ideales para salteados, pastas o ceviches.",
                    ImageUrl = "https://example.com/images/ingredients/camarones.jpg",
                    IngredientCategoryId = fishAndSeafoodCat.Id
                };

                var Tomato = new Ingredient()
                {
                    Name = "Tomate",
                    Description = "Fruta jugosa indispensable para ensaladas, salsas base y sofritos.",
                    ImageUrl = "https://example.com/images/ingredients/tomate.jpg",
                    IngredientCategoryId = fruitsAndVegetablesCat.Id
                };

                var WhiteOnion = new Ingredient()
                {
                    Name = "Cebolla Blanca",
                    Description = "Bulbo aromático esencial para dar sabor a bases de sopas, guisos y carnes.",
                    ImageUrl = "https://example.com/images/ingredients/cebolla.jpg",
                    IngredientCategoryId = fruitsAndVegetablesCat.Id
                };

                var Garlic = new Ingredient()
                {
                    Name = "Ajo",
                    Description = "Dientes aromáticos clave para potenciar el sabor de prácticamente cualquier plato.",
                    ImageUrl = "https://example.com/images/ingredients/ajo.jpg",
                    IngredientCategoryId = fruitsAndVegetablesCat.Id
                };

                var RomaineLettuce = new Ingredient()
                {
                    Name = "Lechuga Romana",
                    Description = "Hojas crujientes ideales como base para ensaladas frescas.",
                    ImageUrl = "https://example.com/images/ingredients/lechuga.jpg",
                    IngredientCategoryId = fruitsAndVegetablesCat.Id
                };

                var Eggs = new Ingredient()
                {
                    Name = "Huevos",
                    Description = "Fuente excelente de proteína versátil para desayunos, repostería y platos principales.",
                    ImageUrl = "https://example.com/images/ingredients/huevos.jpg",
                    IngredientCategoryId = dairyAndEggsCat.Id
                };

                var MozzarellaCheese = new Ingredient()
                {
                    Name = "Queso Mozzarella",
                    Description = "Queso de pasta hilada con gran capacidad de fundido, ideal para pizzas y gratinados.",
                    ImageUrl = "https://example.com/images/ingredients/mozzarella.jpg",
                    IngredientCategoryId = dairyAndEggsCat.Id
                };

                var WholeMilk = new Ingredient()
                {
                    Name = "Leche Entera",
                    Description = "Lácteo fluido indispensable para salsas blancas, batidos y repostería.",
                    ImageUrl = "https://example.com/images/ingredients/leche.jpg",
                    IngredientCategoryId = dairyAndEggsCat.Id
                };

                var WhiteRice = new Ingredient()
                {
                    Name = "Arroz Blanco",
                    Description = "Grano de cereal base para acompañar carnes, guisos o preparar salteados.",
                    ImageUrl = "https://example.com/images/ingredients/arroz.jpg",
                    IngredientCategoryId = grainsCerealsAndPastaCat.Id
                };

                var Spaghetti = new Ingredient()
                {
                    Name = "Espaguetis",
                    Description = "Pasta larga de trigo perfecta para combinar con salsas de tomate o cremas.",
                    ImageUrl = "https://example.com/images/ingredients/espaguetis.jpg",
                    IngredientCategoryId = grainsCerealsAndPastaCat.Id
                };

                var RolledOats = new Ingredient()
                {
                    Name = "Avena en Hoja",
                    Description = "Cereal nutritivo ideal para desayunos, porridges, batidos o repostería saludable.",
                    ImageUrl = "https://example.com/images/ingredients/avena.jpg",
                    IngredientCategoryId = grainsCerealsAndPastaCat.Id
                };

                var BlackBeans = new Ingredient()
                {
                    Name = "Frijoles Negros",
                    Description = "Leguminosa rica en fibra y proteína, base para sopas y guarniciones.",
                    ImageUrl = "https://example.com/images/ingredients/frijoles-negros.jpg",
                    IngredientCategoryId = legumesCat.Id
                };

                var Lentils = new Ingredient()
                {
                    Name = "Lentejas",
                    Description = "Legumbres de cocción rápida ideales para guisos reconfortantes o hamburguesas vegetarianas.",
                    ImageUrl = "https://example.com/images/ingredients/lentejas.jpg",
                    IngredientCategoryId = legumesCat.Id
                };

                var GroundBlackPepper = new Ingredient()
                {
                    Name = "Pimienta Negra Molida",
                    Description = "Especias picante y aromática para realzar el sabor de carnes y sopas.",
                    ImageUrl = "https://example.com/images/ingredients/pimienta.jpg",
                    IngredientCategoryId = spicesHerbsAndCondimentsCat.Id
                };

                var DriedOregano = new Ingredient()
                {
                    Name = "Orégano Seco",
                    Description = "Hierba aromática indispensable en salsas de tomate, carnes y platos italianos.",
                    ImageUrl = "https://example.com/images/ingredients/oregano.jpg",
                    IngredientCategoryId = spicesHerbsAndCondimentsCat.Id
                };

                var ExtraVirginOliveOil = new Ingredient()
                {
                    Name = "Aceite de Oliva Extra Virgen",
                    Description = "Grasa saludable ideal para aderezar ensaladas o cocinar a temperaturas moderadas.",
                    ImageUrl = "https://example.com/images/ingredients/aceite-oliva.jpg",
                    IngredientCategoryId = oilsVinegarsAndSaucesCat.Id
                };

                var WheatFlour = new Ingredient()
                {
                    Name = "Harina de Trigo",
                    Description = "Insumo fundamental para la elaboración de panes, masas, rebozados y repostería.",
                    ImageUrl = "https://example.com/images/ingredients/harina-trigo.jpg",
                    IngredientCategoryId = bakeryAndPastryCat.Id
                };

                // Add all ingredients
                context.Ingredients.AddRange(
                    ChickenBreast, GroundBeef, SalmonFillet, Shrimp, Tomato, WhiteOnion, Garlic, RomaineLettuce,
                    Eggs, MozzarellaCheese, WholeMilk, WhiteRice, Spaghetti, RolledOats, BlackBeans, Lentils,
                    GroundBlackPepper, DriedOregano, ExtraVirginOliveOil, WheatFlour
                );

                // Persist ingredients and finish transaction below

                // Measure Units
                var Gram = new MeasureUnit() { Name = "Gramo", Abbreviation = "g" };
                var Kilogram = new MeasureUnit() { Name = "Kilogramo", Abbreviation = "kg" };
                var Milliliter = new MeasureUnit() { Name = "Mililitro", Abbreviation = "ml" };
                var Liter = new MeasureUnit() { Name = "Litro", Abbreviation = "l" };
                var Ounce = new MeasureUnit() { Name = "Onza", Abbreviation = "oz" };
                var Pound = new MeasureUnit() { Name = "Libra", Abbreviation = "lb" };
                var Tablespoon = new MeasureUnit() { Name = "Cucharada", Abbreviation = "cda" };
                var Teaspoon = new MeasureUnit() { Name = "Cucharadita", Abbreviation = "cdta" };
                var Cup = new MeasureUnit() { Name = "Taza", Abbreviation = "taza" };
                var Unit = new MeasureUnit() { Name = "Unidad", Abbreviation = "ud" };
                var Pinch = new MeasureUnit() { Name = "Pizca", Abbreviation = "pizca" };
                var Clove = new MeasureUnit() { Name = "Diente", Abbreviation = "diente" };

                // Add measure units
                context.MeasureUnits.AddRange(Gram, Kilogram, Milliliter, Liter, Ounce, Pound, Tablespoon, Teaspoon, Cup, Unit, Pinch, Clove);

                // Persist remaining entities and commit transaction
                context.SaveChanges();
                context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                context.Database.RollbackTransaction();
                Console.WriteLine("Error seeding data. Transaction rolled back.");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}

