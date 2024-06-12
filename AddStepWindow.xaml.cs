using System.Windows;

namespace RecipeAppWPF
{
    public partial class AddStepWindow : Window
    {
        public string StepDescription { get; private set; }

        public AddStepWindow()
        {
            InitializeComponent();
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            StepDescription = StepDescriptionTextBox.Text;
            DialogResult = true;
            Close();
        }
    }
}