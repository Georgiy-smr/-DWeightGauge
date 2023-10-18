using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using IPS_CALC.Services.Interfaces;
using IPS_CALC.Veiws.Windows;
using IPS_CALC.VIewModels;
using CLASSES = IPS.DAL;

namespace IPS_CALC.Services
{
    internal class UserDialog : IUserDialog
    {
        public bool Edit(object item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            switch (item)
            {
                default:
                    throw new NotSupportedException(
                        $"редактирование элемента не возможно");

                case CLASSES.IPS IPS:
                    return EditIPS(IPS);
            }
        }

        private bool EditIPS(CLASSES.IPS iPS)
        {
            var ips_editor_ViewModel = new IPSEditorViewModel(iPS);

            var ips_editor_window = new IPSEditorWindow
            {
                DataContext = ips_editor_ViewModel
            };

            if(ips_editor_window.ShowDialog() != true) return false;

            iPS.Name = ips_editor_ViewModel.IpsName;

            return true;
        }

        public bool Confirm(string Message,
                            string Caption,
                            bool Exlamination = false) =>
            MessageBox.Show(Message,
                            Caption,
                            MessageBoxButton.YesNo,
                            Exlamination ? MessageBoxImage.Exclamation :
                                           MessageBoxImage.Question)
            == MessageBoxResult.Yes;

    }
}
