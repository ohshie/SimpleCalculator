using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NCalc;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    private int _numberA;
    private int _numberB;
    private int Tempnumber;
    
    public MainWindow()
    {
        InitializeComponent();
        TextBox = this.FindControl<TextBox>("TextBox");
    }
    
    private void ButtonZero_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "0";
    }
    
    private void ButtonOne_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "1";
    }

    private void ButtonTwo_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "2";
    }

    private void ButtonTree_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "3";
    }
    private void ButtonFour_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "4";
    }

    private void ButtonFive_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "5";
    }

    private void ButtonSix_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "6";
    }
    private void ButtonSeven_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "7";
    }

    private void ButtonEight_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "8";
    }

    private void ButtonNine_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "9";
    }

    private void ButtonMult_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "*";
    }
    
    private void ButtonClear_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text = "";
    }
    
    private void ButtonDivide_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "/";
    }
    
    private void ButtonMinus_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "-";
    }
    
    private void ButtonSum_OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox.Text += "+";
    }
    
    private void ButtonEqual_OnClick(object? sender, RoutedEventArgs e)
    {
        Expression expression = new Expression(TextBox.Text);
        TextBox.Text = expression.Evaluate().ToString();
    }
}
