using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CallCenterApplication
{
    public class ViewModel
    {
        public readonly static CallCenter _callCenter = new CallCenter();
        private ListView _operatorsListView = new ListView();

        public ViewModel(ListView operatorsListView)
        {
            _operatorsListView = operatorsListView;
        }

        public ObservableCollection<Operator> Operators
        {
            get { return _callCenter.Operators; }
        }
        public ObservableCollection<Caller> CallsQueue
        {
            get { return _callCenter.CallsQueue; }
        }

        private ICommand addOperatorCommand;
        public ICommand AddOperatorCommand
        {
            get
            {
                if (addOperatorCommand == null)
                {
                    addOperatorCommand = new RelayCommand(param => this.AddOperator(), null);
                }
                return addOperatorCommand;
            }
        }

        private ICommand fireOperatorCommand;
        public ICommand FireOperatorCommand
        {
            get
            {
                if (fireOperatorCommand == null)
                {
                    fireOperatorCommand = new RelayCommand(param => this.FireOperator(), null);
                }
                return fireOperatorCommand;
            }
        }

        private ICommand callCommand;
        public ICommand CallCommand
        {
            get
            {
                if (callCommand == null)
                {
                    callCommand = new RelayCommand(param => this.Call(), null);
                }
                return callCommand;
            }
        }

        private void AddOperator()
        {
            AddOperatorForm addOperatorForm = new AddOperatorForm();
            addOperatorForm.Show();
        }

        private void FireOperator()
        {
            if (_operatorsListView.SelectedItem != null)
            {
                Operator selectedOperator = (Operator)_operatorsListView.SelectedItem;
                _callCenter.RemoveOperator(selectedOperator);
                _operatorsListView.Items.Refresh();
            }
        }

        private void Call()
        {
            CallerForm callerForm = new CallerForm();
            callerForm.Show();
        }
    }
}
