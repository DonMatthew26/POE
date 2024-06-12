using RecipeAppWPF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeAppWPF
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;
        private Recipe selectedRecipe;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            UpdateRecipeList();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow();
            if (addRecipeWindow.ShowDialog() == true)
            {
                recipes.Add(addRecipeWindow.Recipe);
                UpdateRecipeList();
            }
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selected)
            {
                selectedRecipe = selected;
                DisplayRecipeDetails(selected);
            }
        }

        private void UpdateRecipeList(IEnumerable<Recipe> filteredRecipes = null)
        {
            RecipeListBox.ItemsSource = (filteredRecipes ?? recipes).OrderBy(r => r.Name).ToList();
            RecipeListBox.Items.Refresh(); // Ensures the listbox updates correctly
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            RecipeNameTextBox.Text = recipe.Name;
            IngredientsListBox.ItemsSource = recipe.Ingredients.Select(i => $"{i.Quantity} {i.Unit} of {i.Name} - {i.Calories} calories ({i.FoodGroup})").ToList();
            StepsListBox.ItemsSource = recipe.Steps;
            double totalCalories = recipe.CalculateTotalCalories();
            TotalCaloriesTextBlock.Text = $"Total Calories: {totalCalories}";
            if (totalCalories > 300)
            {
                MessageBox.Show("Warning: Total calories exceed 300!", "Calorie Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecipe == null) return;

            double factor = double.Parse(((Button)sender).Tag.ToString());
            selectedRecipe.ScaleIngredients(factor);
            DisplayRecipeDetails(selectedRecipe);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRecipe == null) return;

            selectedRecipe.ResetIngredients();
            DisplayRecipeDetails(selectedRecipe);
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            UpdateRecipeList();
            RecipeNameTextBox.Clear();
            IngredientsListBox.ItemsSource = null;
            StepsListBox.ItemsSource = null;
            TotalCaloriesTextBlock.Text = string.Empty;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = IngredientNameFilterTextBox.Text.ToLower();
            string foodGroup = FoodGroupFilterTextBox.Text.ToLower();
            bool maxCaloriesParsed = double.TryParse(MaxCaloriesFilterTextBox.Text, out double maxCalories);

            var filteredRecipes = recipes.Where(r =>
            {
                bool ingredientMatch = string.IsNullOrEmpty(ingredientName) || r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientName));
                bool foodGroupMatch = string.IsNullOrEmpty(foodGroup) || r.Ingredients.Any(i => i.FoodGroup.ToLower().Contains(foodGroup));
                bool caloriesMatch = !maxCaloriesParsed || r.CalculateTotalCalories() <= maxCalories;

                return ingredientMatch && foodGroupMatch && caloriesMatch;
            }).ToList();

            UpdateRecipeList(filteredRecipes);
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            IngredientNameFilterTextBox.Clear();
            FoodGroupFilterTextBox.Clear();
            MaxCaloriesFilterTextBox.Clear();
            UpdateRecipeList();
        }
    }
}