using CallCenterApplication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CallCenterApplication
{
    public partial class AddOperatorForm : Window
    {
        private Operator? _operator = null;

        public AddOperatorForm()
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

            Dictionary<string, int> skillSet = new Dictionary<string, int>();
            foreach (CheckBox checkBox in SkillCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    skillSet.Add(checkBox.Content.ToString(), 1); // Set skill level somehow
                }
            }
            if (skillSet.Count == 0)
            {
                MessageBox.Show("Please select at least one skill.");
                return;
            }

            int performance = (int)PerformanceSlider.Value;
          
            _operator = new Operator(name, languages, skillSet, performance);
            MainWindow._callCenter.AddOperator(_operator);
            MainWindow._callCenter.CheckQueue();

            MessageBox.Show("Operator added successfully.");

            this.Close();
        }
    }
}
