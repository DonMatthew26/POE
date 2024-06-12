using RecipeAppWPF.Models;
using System.Windows;

namespace RecipeAppWPF
{
    public partial class AddIngredientWindow : Window
    {
        public Ingredient Ingredient { get; private set; }

        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string name = IngredientNameTextBox.Text;
            double quantity = double.Parse(QuantityTextBox.Text);
            string unit = UnitTextBox.Text;
            int calories = int.Parse(CaloriesTextBox.Text);
            string foodGroup = FoodGroupTextBox.Text;

            Ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
            DialogResult = true;
            Close();
        }
    }
}