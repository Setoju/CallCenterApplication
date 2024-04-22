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

            MainCall();
        }

        public void MainCall()
        {
            Operator op1 = new Operator("Eugene", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, new Dictionary<string, int> { { "Communicating", 1 } }, 10);
            Operator op2 = new Operator("Sergei", new Dictionary<string, int> { { "Italian", 1 }, { "Spanish", 2 } }, new Dictionary<string, int> { { "Helping", 1 } }, 10);

            Caller call1 = new Caller("Sasa", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, "Communicating", 20);
            Caller call2 = new Caller("Sasha", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, "Helping", 20);
            Caller call3 = new Caller("Sashaasd", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, "Helping", 20);
            Caller call4 = new Caller("Sashaasasdd", new Dictionary<string, int> { { "English", 1 }, { "Spanish", 2 } }, "Helping", 20);

            List<Operator> operators = new List<Operator>() { op1, op2 };

            CallCenter callCenter = new CallCenter(operators);            
            callCenter.ReceiveCall(call1);
            callCenter.ReceiveCall(call2);
            callCenter.ReceiveCall(call3);
            callCenter.ReceiveCall(call4);
        }
    }
}