using RecipeAppWPF.Models;
using System.Collections.Generic;
using System.Windows;

namespace RecipeAppWPF
{
    public partial class AddRecipeWindow : Window
    {
        public Recipe Recipe { get; private set; }

        public AddRecipeWindow()
        {
            InitializeComponent();
            Recipe = new Recipe();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var addIngredientWindow = new AddIngredientWindow();
            if (addIngredientWindow.ShowDialog() == true)
            {
                Recipe.Ingredients.Add(addIngredientWindow.Ingredient);
                IngredientsListBox.Items.Add($"{addIngredientWindow.Ingredient.Quantity} {addIngredientWindow.Ingredient.Unit} of {addIngredientWindow.Ingredient.Name} - {addIngredientWindow.Ingredient.Calories} calories ({addIngredientWindow.Ingredient.FoodGroup})");
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            var addStepWindow = new AddStepWindow();
            if (addStepWindow.ShowDialog() == true)
            {
                Recipe.Steps.Add(addStepWindow.StepDescription);
                StepsListBox.Items.Add(addStepWindow.StepDescription);
            }
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe.Name = RecipeNameTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}