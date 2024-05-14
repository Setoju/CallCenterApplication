using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallCenterApplication 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel viewModel = new ViewModel(operatorsListView);
            DataContext = viewModel;
        }

        //private void AddOperatorButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddOperatorForm addOperatorForm = new AddOperatorForm();
        //    addOperatorForm.Show();
        //}
        //private void FireOperatorButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (operatorsListView.SelectedItem != null)
        //    {
        //        Operator selectedOperator = (Operator)operatorsListView.SelectedItem;
        //        _callCenter.RemoveOperator(selectedOperator);
        //        operatorsListView.Items.Refresh();
        //    }
        //}

        //private void CallButton_Click(object sender, RoutedEventArgs e)
        //{
        //    CallerForm callerForm = new CallerForm();
        //    callerForm.Show();
        //}
    }
}