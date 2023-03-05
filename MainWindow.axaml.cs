using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NCalc;

namespace SimpleCalculator;

    public partial class MainWindow : Window
    {
        private string _backEndEquation;
        private string _result;
        private List<string> _equationList = new List<string>();
        private List<string> _backEndEquationList = new List<string>();
        private ObservableCollection<string> _previousCalculations = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            TextBox = this.FindControl<TextBox>("TextBox");
            DataContext = this;
            PreviousCalcList.Items = _previousCalculations;
        }
        
        private void FieldButton_Click(object? sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();

            if (SafetyCheck.Infinite(TextBox.Text) & tag!="Clear")
                return;
            
            switch (tag)
            {
                case "Numbers":
                    if (SafetyCheck.Number(TextBox.Text))
                        return;
                    break;
                case "Operators":
                    if (SafetyCheck.Operator(TextBox.Text))
                        return;
                    break;
                case "Comma":
                    if (SafetyCheck.Comma(TextBox.Text))
                        return;
                    break;
                case "CloseParenthesis":
                    if (SafetyCheck.ClosedParenthesis(TextBox.Text))
                        return;
                    break;
                case "OpenParenthesis":
                    if (SafetyCheck.OpenParenthesis(TextBox.Text))
                        return;
                    break;
                case "Percent":
                    if (SafetyCheck.Percent(TextBox.Text))
                        return;
                    PercentOperations percentOperations = new PercentOperations();
                    _backEndEquation = percentOperations.Transform(_backEndEquation);
                    TextBox.Text += button.Content;
                    return;
                case "Clear":
                    TextBox.Text = _backEndEquation = "";
                    return;
                case "Equals":
                    if (SafetyCheck.Equals(TextBox.Text))
                        return;
                    TextBox.Text = SolveEquation(); 
                    return;
            }
            StringConstructor(sender);
        }
        private void StringConstructor(object? sender)
        {
            var button = sender as Button;
            TextBox.Text += button.Content.ToString();
            _backEndEquation += button.Content.ToString();
        }
        
        private string SolveEquation()
        {
            if (Regex.IsMatch(_backEndEquation,@"[\+\-\*\/\.\(]$"))
                _backEndEquation = _backEndEquation.Substring(0, _backEndEquation.Length - 1);
            
            _result = EvaluateTextBoxText().ToString(CultureInfo.InvariantCulture);
            
            SaveEquationToList();
            
            _backEndEquation = _result;
            
            return _result;
        }

        private void SaveEquationToList()
        {
            _equationList.Add(TextBox.Text);
            _backEndEquationList.Add(_backEndEquation);
            _previousCalculations.Add($"{TextBox.Text}=\n" +
                                      $"{_result}");
        }
        
        private string EvaluateTextBoxText()
        {
            Expression expression = new Expression(_backEndEquation);
            var result = expression.Evaluate();
            if (result.ToString() is "∞" or "-∞")
                return result.ToString();
            
            return Convert.ToDecimal(result).ToString();
        }
        private void Button_ListToggle(object? sender, RoutedEventArgs e)
        {
            if (PreviousCalcListPanel.IsVisible == false)
            {
                RectangleSeparator.IsVisible = true;
                RectangleSeparator.Width += 2;
                PreviousCalcListPanel.IsVisible = true;
                CalculatorWindow.Width += 202;
                PreviousCalcListPanel.Width += 200;
            }
            else
            {
                RectangleSeparator.IsVisible = false;
                RectangleSeparator.Width -= 2;
                PreviousCalcListPanel.IsVisible = false;
                CalculatorWindow.Width -= 202;
                PreviousCalcListPanel.Width -= 200;
            }
        }
        private void PreviousCalcList_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (PreviousCalcList.SelectedItem == null)
                return;
            
            TextBox.Text = _equationList[PreviousCalcList.SelectedIndex];
            _backEndEquation = _backEndEquationList[PreviousCalcList.SelectedIndex];
            PreviousCalcList.SelectedItem = null;
        }
    }