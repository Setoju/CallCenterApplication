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
using System.Windows.Shapes;

namespace CallCenterApplication
{
    /// <summary>
    /// Interaction logic for CallerForm.xaml
    /// </summary>
    public partial class CallerForm
    {
        Caller? _caller = null;
        public CallerForm()
        {
            InitializeComponent();
        }
        private void PerformanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PerformanceValueTextBlock != null)
            {
                PerformanceValueTextBlock.Text = ((int)PerformanceSlider.Value).ToString();
            }
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter a name.");
                return;
            }

            string name = NameTextBox.Text;

            Dictionary<string, int> languages = new Dictionary<string, int>();
            foreach (CheckBox checkBox in LanguageCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    languages.Add(checkBox.Content.ToString(), 1); // Set language level somehow
                }
            }
            if (languages.Count == 0)
            {
                MessageBox.Show("Please select at least one language.");
                return;
            }

            string callType = string.Empty;
            foreach (RadioButton radioButton in SkillRadioButtons.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    callType = radioButton.Content.ToString();
                    break; 
                }
            }
            if (callType == string.Empty)
            {
                MessageBox.Show("Please select call type.");
                return;
            }

            int timeComplexity = (int)PerformanceSlider.Value;

            _caller = new Caller(name, languages, callType, timeComplexity);
            this.Close();

            ViewModel._callCenter.ReceiveCall(_caller);                        
        }
    }
}
