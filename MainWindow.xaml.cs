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
using CallCenterApplication.Operators;

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

            MainCall();
        }

        public void MainCall()
        {
            Operator op1 = new Operator("Eugene", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, new Dictionary<string, int> { { "Communicating", 1 } }, 10);
            Operator op2 = new Operator("Sergei", new Dictionary<string, int> { { "Italian", 1 }, { "Spanish", 2 } }, new Dictionary<string, int> { { "Helping", 1 } }, 10);

            Call call1 = new Call("Helping", 20, "Italian");
            Call call2 = new Call("Helping", 20, "Spanish");

            List<Operator> operators = new List<Operator>() { op1, op2 };

            CallCenter callCenter = new CallCenter(operators);
            callCenter.ReceiveCall(call1);
            callCenter.ReceiveCall(call2);
        }
    }
}