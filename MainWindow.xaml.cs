using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tax_Calulator_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            WINDOW.ResizeMode = ResizeMode.NoResize;
        }

        
        private void SetIncomeText(string NewText)
        {
            string InputText;
            if (NewText == null)
            {
               InputText = IncomeInput.Text.Substring(0, IncomeInput.Text.Length-1);
            }
            else
            {
                InputText = IncomeInput.Text + NewText;
            }
            // Add $ to start of text
            
            InputText = InputText.Replace("$", "");
            double.TryParse(InputText, out double Income);
            IncomeInput.Text = $"${Income}";
            IncomeInput.CaretIndex = IncomeInput.GetLineLength(0);
            // Calulations
            NetIncomeLab.Content = $"Net Income: ${TaxReturn.NetIncome(Income)}";
            TaxRateLab.Content = $"Tax Rate: {TaxReturn.GetRate(Income)}%";
            LostMoneyLab.Content = $"Money Lost: ${TaxReturn.TaxedAmount(Income)}";
        }
        private void IncomeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse($"{IncomeInput.Text.Replace("$", "")}{e.Text}", out _);
            if (!e.Handled)
            {
                e.Handled = true;
                SetIncomeText(e.Text);
            }
        }

        private void WageBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string NewText = WageBox.Text + e.Text;
            e.Handled = !double.TryParse(NewText.Replace("$", ""), out _);
            if (!e.Handled)
            {
                e.Handled = true;
                SetWageInput(NewText);
            }
        }
        private void WageBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                string NewText = WageBox.Text.Substring(0, WageBox.Text.Length - 1);
                if (NewText == "$")
                {
                    NewText = "0";
                }
                SetWageInput(NewText);
            }
        }
        private void SetWageInput(string NewText)
        {
            NewText = NewText.Replace("$", "");
            double Wage = double.Parse(NewText);
            WageBox.Text = $"${Wage}";
            WageBox.CaretIndex = WageBox.GetLineLength(0);
        }
        private void PartBut_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(WageBox.Text.Replace("$", ""), out double income);
            WageToSalaryLab.Text = $"${Math.Round(Salary.PartTimeGross(income, Math.Round(Hour_Slider.Value)), 2)}";
        }

        private void FullBut_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(WageBox.Text.Replace("$", ""), out double income);
            WageToSalaryLab.Text = $"${Math.Round(Salary.FullTimeGross(income, Math.Round(Hour_Slider.Value)), 2)}";
        }

        private void Hour_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderLab.Content = $"Hours per week: {Math.Round(e.NewValue)}";
        }

        private void IncomeInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SetIncomeText("");
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = true;
                SetIncomeText(null);
            }
        }

        /*private void MoreBut_Click(object sender, RoutedEventArgs e)
        {
            Window x = new Window
            {
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Name = "MoreOptionsWin",
                Title = "More Options",
                Width = 500,
                Height = 400,
                
            };

            // Objects
            Grid MoreGrid = new Grid
            {
                Margin = new Thickness(100, 100, 100, 100),
                Width = 300,
                Height = 200,
            };
            StackPanel Stack = new StackPanel
            {
                Width = 300,
                Height = 100,

            };

            Slider OverTimeLimit = new Slider
            {
                Width = 200,
            };
            MoreGrid.Children.Add(OverTimeLimit);
            MoreGrid.Children.Add(Stack);
            x.Content = MoreGrid;
            x.Show();
        }*/

        
    }
}
