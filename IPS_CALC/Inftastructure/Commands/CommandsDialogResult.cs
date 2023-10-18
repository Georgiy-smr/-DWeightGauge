using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IPS_CALC.Inftastructure.Commands
{
    internal class CommandsDialogResult : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool? DialogResult { get; set; }

        public bool CanExecute(object parameter)
        => !(App.CurrentWindow is null);

        public void Execute(object parameter)
        {
            if (!(CanExecute(parameter))) return;

            var window = App.CurrentWindow;

            var dialog_result = DialogResult;

            if (parameter != null)
                dialog_result = Convert.ToBoolean(parameter);

            window.DialogResult = dialog_result;
            window.Close();
        }
    }
}
