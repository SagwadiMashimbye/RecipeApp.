using RecipeAppPoe.Models;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppPoe
{
    public partial class MainWindow : Window
    {
        public delegate void CalorieNotificationHandler(string message);
        private List<Recipe> recipes = new List<Recipe>();
        public event CalorieNotificationHandler CalorieNotification;
        private Dictionary<string, List<Ingredient>> originalIngredientQuantities = new Dictionary<string, List<Ingredient>>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeEventHandlers();
            CalorieNotification += HandleCalorieNotification;
        }
        //
        private void HandleCalorieNotification(string message)
        {
            NotificationTextBlock.Text = message;
            NotificationTextBlock.Visibility = Visibility.Visible;
        }

        private void InitializeEventHandlers()
        {
            // Button Click Event Handlers
            AddRecipeButton.Click += AddRecipeButton_Click;
            DisplayRecipeButton.Click += DisplayRecipeButton_Click;
            ScaleRecipeButton.Click += ScaleRecipeButton_Click;
            ResetQuantityButton.Click += ResetQuantityButton_Click;
            ClearAllDataButton.Click += ClearAllDataButton_Click;
            CloseAppButton.Click += CloseAppButton_Click;
            FilterButton.Click += FilterButton_Click;

            // ComboBox SelectionChanged Event Handlers
            ScaleRecipeComboBox.SelectionChanged += ScaleRecipeComboBox_SelectionChanged;
           

            // Initial visibility setup
            HideAllPanels();

            // Clear Menu Selection ComboBoxes
            ScaleRecipeComboBox.SelectionChanged += ClearMenuComboBox_SelectionChanged;
            ResetRecipeComboBox.SelectionChanged += ClearMenuComboBox_SelectionChanged;
        }


        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPanel(AddRecipePanel);
            UpdateRecipeList(ApplicationHelper.GetAllRecipes());
        }

        private void DisplayRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPanel(DisplayRecipesPanel);
            // Clear previous details
            DisplayRecipeName.Text = "";
            DisplayIngredientsPanel.Children.Clear();
            DisplayStepsPanel.Children.Clear();

            var selectedRecipe = RecipeListView.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                DisplayRecipeDetails(selectedRecipe);

                // Check for high calorie ingredients and show notification if needed
                if (selectedRecipe.Ingredients.Any(i => i.Calories > 300))
                {
                    NotificationTextBlock.Text = "This recipe contains ingredients with high calorie content (>300 cal).";
                    NotificationTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    NotificationTextBlock.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UpdateRecipeList(List<Recipe> recipes)
        {
            // Replace RecipeComboBox with RecipeListView
            RecipeListView.ItemsSource = recipes;
            RecipeListView.DisplayMemberPath = "Name";
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = IngredientFilterTextBox.Text.Trim();
            string foodGroupFilter = FoodGroupFilterComboBox.SelectedItem as string;
            int.TryParse(MaxCaloriesFilterTextBox.Text.Trim(), out int maxCalories);

            List<Recipe> filteredRecipes = ApplicationHelper.GetAllRecipes();

            if (!string.IsNullOrWhiteSpace(ingredientFilter))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter.ToLower()))).ToList();
            }

            if (!string.IsNullOrWhiteSpace(foodGroupFilter) && foodGroupFilter != "All")
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup == foodGroupFilter)).ToList();
            }

            if (maxCalories > 0)
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Sum(i => i.Calories) <= maxCalories).ToList();
            }

            UpdateRecipeList(filteredRecipes);

            // Check for recipes with high calorie ingredients
            foreach (var recipe in filteredRecipes)
            {
                if (recipe.Ingredients.Any(i => i.Calories > 300))
                {
                    NotificationTextBlock.Text = "This recipe contains ingredients with high calorie content (>300 cal).";
                    NotificationTextBlock.Visibility = Visibility.Visible;
                    break; // Display notification for the first recipe found
                }
                else
                {
                    NotificationTextBlock.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void TogglePanelVisibility(Panel panel)
        {
            panel.Visibility = panel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void RecipeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListView.SelectedItem != null)
            {
                string selectedRecipeName = RecipeListView.SelectedItem.ToString();
                var selectedRecipe = ApplicationHelper.GetAllRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

                if (selectedRecipe != null)
                {
                    DisplayRecipeDetails(selectedRecipe);
                }
            }
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPanel(ScaleRecipeMenu);
            if (ScaleRecipeMenu.Visibility == Visibility.Visible)
            {
                ScaleRecipeComboBox.ItemsSource = ApplicationHelper.GetAllRecipes().Select(r => r.Name).ToList();
            }
            else
            {
                // Replace RecipeComboBox with RecipeListView
                RecipeListView.SelectedIndex = -1;
            }
        }
        private void HideAllPanels()
        {
            AddRecipePanel.Visibility = Visibility.Collapsed;
            DisplayRecipesPanel.Visibility = Visibility.Collapsed;
            FilterPanel.Visibility = Visibility.Visible;
            ScaleRecipeMenu.Visibility = Visibility.Collapsed;
            ResetQuantityMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowPanel(UIElement panel)
        {
            HideAllPanels();
            panel.Visibility = Visibility.Visible;
        }

        private void HidePanel(Panel panel)
        {
            panel.Visibility = Visibility.Collapsed;
        }

        private void HideOtherPanels(UIElement visiblePanel)
        {
            foreach (var child in MainStackPanel.Children)
            {
                if (child != visiblePanel)
                {
                    (child as UIElement).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ScaleRecipeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScaleRecipeComboBox.SelectedItem != null)
            {
                string selectedRecipeName = ScaleRecipeComboBox.SelectedItem.ToString();
                var selectedRecipe = ApplicationHelper.GetAllRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

                if (selectedRecipe != null)
                {
                    List<string> scaleFactors = new List<string> { "2x", "3x", "0.5x" };
                    ScaleFactorComboBox.ItemsSource = scaleFactors;
                }
            }
        }

        private void ApplyScale_Click(object sender, RoutedEventArgs e)
        {
            if (ScaleRecipeComboBox.SelectedItem != null && ScaleFactorComboBox.SelectedItem != null)
            {
                string selectedRecipeName = ScaleRecipeComboBox.SelectedItem.ToString();
                var selectedRecipe = ApplicationHelper.GetAllRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

                if (selectedRecipe != null)
                {
                    string scaleFactor = ScaleFactorComboBox.SelectedItem.ToString();
                    double factor = scaleFactor switch
                    {
                        "2x" => 2.0,
                        "3x" => 3.0,
                        "0.5x" => 0.5,
                        _ => 1.0
                    };

                    selectedRecipe.ScaleRecipe(factor);
                    DisplayRecipe(selectedRecipe);
                    MessageBox.Show("The recipe has been scaled successfully.");
                }
            }
        }

        private void ClearRecipeForm()
        {
            RecipeNameTextBox.Clear();
            IngredientsStackPanel.Children.Clear();
            RecipeStepsStackPanel.Children.Clear();
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            DisplayRecipeName.Text = recipe.Name;
            DisplayIngredientsPanel.Children.Clear();
            DisplayStepsPanel.Children.Clear();

            foreach (var ingredient in recipe.Ingredients)
            {
                DisplayIngredientsPanel.Children.Add(new TextBlock
                {
                    Text = $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} cal)"
                });
            }

            foreach (var step in recipe.Steps)
            {
                StackPanel stepPanel = new StackPanel { Orientation = Orientation.Horizontal };
                CheckBox stepCheckBox = new CheckBox { Content = step.Description, Margin = new Thickness(0, 0, 10, 0) };
                stepPanel.Children.Add(stepCheckBox);
                DisplayStepsPanel.Children.Add(stepPanel);
            }
        }


        private void DisplayRecipe(Recipe recipe)
        {
            DisplayRecipeDetails(recipe);
        }

        private void ResetQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPanel(ResetQuantityMenu);
            ResetRecipeComboBox.ItemsSource = ApplicationHelper.GetAllRecipes().Select(r => r.Name).ToList();
        }

        private void ApplyReset_Click(object sender, RoutedEventArgs e)
        {
            if (ResetRecipeComboBox.SelectedItem is string recipeName)
            {
                ApplicationHelper.ResetQuantity(recipeName);
                MessageBox.Show($"{recipeName} quantities have been reset.");
            }
        }

        private void ClearAllDataButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all recipes?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ApplicationHelper.ClearAllData();
                MessageBox.Show("All recipes have been cleared.");
                UpdateRecipeList(ApplicationHelper.GetAllRecipes());
            }
        }

        private void ClearMenuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear other ComboBox selections if needed to ensure only one menu is active at a time
            if (sender != ScaleRecipeComboBox) ScaleRecipeComboBox.SelectedIndex = -1;
            if (sender != ResetRecipeComboBox) ResetRecipeComboBox.SelectedIndex = -1;
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel ingredientPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };

            TextBox nameTextBox = new TextBox { Margin = new Thickness(0, 0, 10, 0), Width = 150 };
            TextBox quantityTextBox = new TextBox { Margin = new Thickness(0, 0, 10, 0), Width = 50 };
            ComboBox unitComboBox = new ComboBox { Margin = new Thickness(0, 0, 10, 0), Width = 100, ItemsSource = new[] { "Tablespoon", "Teaspoon", "Cups" } };
            TextBox caloriesTextBox = new TextBox { Margin = new Thickness(0, 0, 10, 0), Width = 80 };
            ComboBox foodGroupComboBox = new ComboBox
            {
                Margin = new Thickness(0, 0, 10, 0),
                Width = 150,
                ItemsSource = new[]
                {
                    "Starchy food", "Vegetables and Fruits", "Dry beans, Peas, Lentils and Soy",
                    "Chicken, Fish, Meat and Eggs", "Milk and Dairy products", "Fats and Oils", "Water"
                }
            };

            ingredientPanel.Children.Add(new TextBlock { Text = "Name:", Margin = new Thickness(0, 0, 10, 0) });
            ingredientPanel.Children.Add(nameTextBox);
            ingredientPanel.Children.Add(new TextBlock { Text = "Quantity:", Margin = new Thickness(0, 0, 10, 0) });
            ingredientPanel.Children.Add(quantityTextBox);
            ingredientPanel.Children.Add(new TextBlock { Text = "Unit:", Margin = new Thickness(0, 0, 10, 0) });
            ingredientPanel.Children.Add(unitComboBox);
            ingredientPanel.Children.Add(new TextBlock { Text = "Calories:", Margin = new Thickness(0, 0, 10, 0) });
            ingredientPanel.Children.Add(caloriesTextBox);
            ingredientPanel.Children.Add(new TextBlock { Text = "Food Group:", Margin = new Thickness(0, 0, 10, 0) });
            ingredientPanel.Children.Add(foodGroupComboBox);

            IngredientsStackPanel.Children.Add(ingredientPanel);
        }

        private void RemoveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsStackPanel.Children.Count > 0)
            {
                IngredientsStackPanel.Children.RemoveAt(IngredientsStackPanel.Children.Count - 1);
            }
        }


        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stepPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
            TextBox stepDescriptionTextBox = new TextBox { Width = 300, Margin = new Thickness(0, 0, 10, 0) };
            stepPanel.Children.Add(stepDescriptionTextBox);
            RecipeStepsStackPanel.Children.Add(stepPanel);
        }

        private void RecipeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = RecipeListView.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Recipe name cannot be empty.");
                return;
            }

            List<Ingredient> ingredients = new List<Ingredient>();
            bool hasHighCalorieIngredient = false;

            foreach (StackPanel ingredientPanel in IngredientsStackPanel.Children)
            {
                var nameTextBox = ingredientPanel.Children.OfType<TextBox>().ElementAtOrDefault(0);
                var quantityTextBox = ingredientPanel.Children.OfType<TextBox>().ElementAtOrDefault(1);
                var unitComboBox = ingredientPanel.Children.OfType<ComboBox>().ElementAtOrDefault(0);
                var caloriesTextBox = ingredientPanel.Children.OfType<TextBox>().ElementAtOrDefault(2);
                var foodGroupComboBox = ingredientPanel.Children.OfType<ComboBox>().ElementAtOrDefault(1);

                if (nameTextBox != null && quantityTextBox != null && unitComboBox != null && caloriesTextBox != null && foodGroupComboBox != null)
                {
                    string ingredientName = nameTextBox.Text.Trim();
                    if (string.IsNullOrWhiteSpace(ingredientName) ||
                        !double.TryParse(quantityTextBox.Text.Trim(), out double quantity) ||
                        unitComboBox.SelectedItem == null ||
                        !int.TryParse(caloriesTextBox.Text.Trim(), out int calories) ||
                        foodGroupComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please ensure all ingredient fields are correctly filled.");
                        return;
                    }

                    string unit = unitComboBox.SelectedItem.ToString();
                    string foodGroup = foodGroupComboBox.SelectedItem.ToString();

                    var ingredient = new Ingredient
                    {
                        Name = ingredientName,
                        Quantity = quantity,
                        Unit = unit,
                        Calories = calories,
                        FoodGroup = foodGroup
                    };
                    ingredient.SetOriginalQuantity();
                    ingredients.Add(ingredient);

                    if (calories > 100)
                    {
                        hasHighCalorieIngredient = true;
                    }
                }
            }

            List<RecipeStep> steps = new List<RecipeStep>();
            foreach (StackPanel stepPanel in RecipeStepsStackPanel.Children)
            {
                var stepTextBox = stepPanel.Children.OfType<TextBox>().FirstOrDefault();
                if (stepTextBox != null)
                {
                    string stepDescription = stepTextBox.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(stepDescription))
                    {
                        steps.Add(new RecipeStep { Description = stepDescription });
                    }
                }
            }

            if (ingredients.Count == 0 || steps.Count == 0)
            {
                MessageBox.Show("A recipe must have at least one ingredient and one step.");
                return;
            }

            var newRecipe = new Recipe
            {
                Name = recipeName,
                Ingredients = ingredients,
                Steps = steps
            };

            ApplicationHelper.AddRecipe(newRecipe);
            MessageBox.Show("Recipe saved successfully.");

            if (hasHighCalorieIngredient)
            {
                CalorieNotification?.Invoke("This recipe contains ingredients with high calorie content.");
            }

            ClearRecipeForm();
        }

    }
}
