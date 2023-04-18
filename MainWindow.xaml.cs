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

        

        private void IncomeInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double Income = 0;
            TextBox Box = sender as TextBox;
            if (e != null)
            {
                string Input = Box.Text + e.Text;
                Input.Replace("$", "");
                e.Handled = !double.TryParse(Input, out Income);
            }
            
            if (e == null || !e.Handled)
            {
                IncomeInput.Text = $"${Income}";
                NetIncomeLab.Content = $"Net Income: ${Math.Round(TaxReturn.NetIncome(Income), 2)}";
                LostMoneyLab.Content = $"Money Lost: ${TaxReturn.TaxedAmount(Income)}";
            }
        }

        private void WageBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _);
        }

        private void PartBut_Click(object sender, RoutedEventArgs e)
        {
            WageToSalaryLab.Text = $"${Math.Round(Salary.PartTimeGross(12, Math.Round(Hour_Slider.Value)), 2)}";
        }

        private void FullBut_Click(object sender, RoutedEventArgs e)
        {
            WageToSalaryLab.Text = $"${Math.Round(Salary.FullTimeGross(12, Math.Round(Hour_Slider.Value)), 2)}";
        }

        private void Hour_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderLab.Content = $"Hours per week: {Math.Round(e.NewValue)}";
        }

        private void IncomeInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                IncomeInput_PreviewTextInput(sender, null);
            }
        }
    }
}
