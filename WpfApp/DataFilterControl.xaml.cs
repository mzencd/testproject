using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for DataFilterControl.xaml
    /// Use dependency property to manage filter text binding
    /// Use ListCollectionView to handle filter operation of ListBox
    /// </summary>
    public partial class DataFilterControl : UserControl
    {
        #region Initialization
        public TextBox TextBox { get; set; }
        public ListBox ListBox { get; set; }

        public DataFilterControl()
        {
            InitializeComponent();

            TextBox = txtFilter;
            ListBox = lstFilter;
        }
        #endregion

        #region Data source binding, with "ListCollectionView"
        public ListCollectionView BindingView;

        private ListCollectionView SetBindingView(object source)
        {
            return BindingView = CollectionViewSource.GetDefaultView(source) as ListCollectionView;
        }

        public object DataSource
        {
            get { return BindingView.SourceCollection; }
            set { ListBox.ItemsSource = SetBindingView(value); }
        }
        #endregion

        #region Dependency property
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); OnFilterTextChanged(value); }
        }

        public static readonly DependencyProperty FilterTextProperty = DependencyProperty.Register("FilterText", typeof(string), typeof(DataFilterControl), new PropertyMetadata(default(string), DataFilterChangedCallback), ValidateFilterValueCallback);

        public static void DataFilterChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataFilterControl dataFilterControl = d as DataFilterControl;
            if (dataFilterControl != null)
            {
                dataFilterControl.OnFilterTextChanged(e.NewValue.ToString() ?? string.Empty);
            }
        }

        public static bool ValidateFilterValueCallback(object value)
        {
            return true;
        }
        #endregion

        #region Filter event handler
        public void OnFilterTextChanged(string filterText)
        {
            BindingView.Filter = ((text) =>
            {
                return text.ToString().Contains(FilterText);
            });
        }
        #endregion
    };
}
