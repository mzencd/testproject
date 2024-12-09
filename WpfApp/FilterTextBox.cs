using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    class FilterTextBox : TextBox
    {
        private TextBox _textBox;
        public FilterTextBox(TextBox txtBox)
        {
            _textBox = txtBox;

            Loaded += FilterTextBox_Loaded;
        }

        private void FilterTextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("FilterTextBox loaded!");
        }
    }
}
