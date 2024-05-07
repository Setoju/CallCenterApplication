﻿using CallCenterApplication;
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
            string name = NameTextBox.Text;

            Dictionary<string, int> languages = new Dictionary<string, int>();
            foreach (CheckBox checkBox in LanguageCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    languages.Add(checkBox.Content.ToString(), 1); // Set language level somehow
                }
            }

            Dictionary<string, int> skillSet = new Dictionary<string, int>();
            foreach (CheckBox checkBox in SkillCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    skillSet.Add(checkBox.Content.ToString(), 1); // Set skill level somehow
                }
            }

            int performance = (int)PerformanceSlider.Value;
          
            _operator = new Operator(name, languages, skillSet, performance);
            MainWindow._callCenter.AddOperator(_operator);

            MessageBox.Show("Operator added successfully.");

            this.Close();
        }
    }
}
