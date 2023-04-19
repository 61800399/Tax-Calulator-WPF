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
        }

        
        private void SetIncomeText(string NewText)
        {
            
            // Add $ to start of text
            string InputText = IncomeInput.Text + NewText;
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
                WageBox.Text = $"${NewText.Replace("$", "")}";
                WageBox.CaretIndex = WageBox.GetLineLength(0);
            }
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
                SetIncomeText(null);
            }

        }
    }
}
