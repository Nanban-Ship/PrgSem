using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RGBmixer
{
    public partial class MainWindow : Window
    {
        private bool _isUpdating = false;

        public MainWindow()
        {
            InitializeComponent();

            sldRed.Value = 255;
            sldGreen.Value = 255;
            sldBlue.Value = 255;

            UpdateColor();
        }

        private void UpdateColor()
        {
            byte r = (byte)sldRed.Value;
            byte g = (byte)sldGreen.Value;
            byte b = (byte)sldBlue.Value;

            Color currentColor = Color.FromRgb(r, g, b);
            rectColor.Fill = new SolidColorBrush(currentColor);

            lblHex.Content = $"#{r:X2}{g:X2}{b:X2}";
        }

        private void Sld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating) return;

            _isUpdating = true;

            if (sender == sldRed) txtRed.Text = ((int)sldRed.Value).ToString();
            if (sender == sldGreen) txtGreen.Text = ((int)sldGreen.Value).ToString();
            if (sender == sldBlue) txtBlue.Text = ((int)sldBlue.Value).ToString();

            UpdateColor();

            _isUpdating = false; 
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating) return;

            TextBox currentTextBox = sender as TextBox;
            if (string.IsNullOrEmpty(currentTextBox.Text)) return;

            _isUpdating = true;

            if (int.TryParse(currentTextBox.Text, out int val))
            {
                if (val > 255)
                {
                    MessageBox.Show("Můžete zadávat jen hodnoty v rozmezí 0-255", "Chyba rozsahu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    val = 255;
                    currentTextBox.Text = "255";
                    currentTextBox.CaretIndex = currentTextBox.Text.Length;
                }

                if (currentTextBox == txtRed) sldRed.Value = val;
                if (currentTextBox == txtGreen) sldGreen.Value = val;
                if (currentTextBox == txtBlue) sldBlue.Value = val;

                UpdateColor();
            }

            _isUpdating = false;
        }
        private void Integer(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
            base.OnPreviewTextInput(e);
        }
    }
}