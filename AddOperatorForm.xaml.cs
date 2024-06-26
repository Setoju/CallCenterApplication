﻿using CallCenterApplication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CallCenterApplication
{
    public partial class AddOperatorForm
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
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || !IsNameValid(NameTextBox.Text))
            {
                MessageBox.Show("Please enter a valid name.");
                return;
            }

            string name = NameTextBox.Text;

            List<string> languages = new List<string>();
            foreach (CheckBox checkBox in LanguageCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    languages.Add(checkBox.Content.ToString());
                }
            }
            if (languages.Count == 0)
            {
                MessageBox.Show("Please select at least one language.");
                return;
            }

            List<string> skillSet = new List<string>();
            foreach (CheckBox checkBox in SkillCheckBoxes.Children)
            {
                if (checkBox.IsChecked == true)
                {
                    skillSet.Add(checkBox.Content.ToString());
                }
            }
            if (skillSet.Count == 0)
            {
                MessageBox.Show("Please select at least one skill.");
                return;
            }

            int performance = (int)PerformanceSlider.Value;
          
            _operator = new Operator(name, languages, skillSet, performance);

            foreach(Operator op in ViewModel._callCenter.Operators)
            {
                if (_operator.Equals(op))
                {
                    MessageBox.Show("Such operator already exists.");
                    return;
                }
            }

            ViewModel._callCenter.AddOperator(_operator);
            ViewModel._callCenter.CheckQueue();

            MessageBox.Show("Operator added successfully.");

            this.Close();
        }

        private bool IsNameValid(string name)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+(?:['-][a-zA-Z]+)*\s*[a-zA-Z]*$");
            return regex.IsMatch(name);
        }
    }
}
