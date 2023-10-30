using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using IPS.DAL;
using IPS_CALC.Services.Interfaces;
using IPS_CALC.Veiws.Windows;
using IPS_CALC.VIewModels;
using CLASSES = IPS.DAL;
using System.Linq;

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

                case Cargo Cargo:
                    return EditCargo(Cargo);
            }
        }
        private bool EditCargo(Cargo cargo)
        {
            var cargoEditorViewModel = new CargoEditViewModel(cargo);

            var cargo_Editor_Window = new CargoEditWindow
            {
                DataContext = cargoEditorViewModel
            };

            if (cargo_Editor_Window.ShowDialog() != true) return false;

            cargo.Name = cargoEditorViewModel.Name;

            return true;
        }

        private bool EditIPS(CLASSES.IPS iPS)
        {
            var ips_editor_ViewModel = new IPSEditorViewModel(iPS);

            var ips_editor_window = new IPSEditorWindow
            {
                DataContext = ips_editor_ViewModel
            };

            if (ips_editor_window.ShowDialog() != true) return false;

            iPS.Name = ips_editor_ViewModel.IpsName;

            return true;
        }

        public bool RedactToAdded(object item, IEnumerable<object> add)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            switch (item)
            {
                default:
                    throw new NotSupportedException(
                        $"редактирование элемента не возможно");

                case CLASSES.IPS IPS:
                    return AddCargoToTheSelectedIps(IPS, (ObservableCollection<Cargo>)add);
            }
        }
        private bool AddCargoToTheSelectedIps(CLASSES.IPS IPS, ObservableCollection<Cargo> Cargos)
        {

            var cargoEditorToSelectedIpsViewModel = new CargoEditorToSelectedIpsViewModel(IPS, Cargos);
            var cargoEditToSelectedIpsWindow = new CargoEditToSelectedIpsWindow()
            {
                DataContext = cargoEditorToSelectedIpsViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            if (cargoEditToSelectedIpsWindow.ShowDialog() != true) return false;

            var selected_cargos = cargoEditorToSelectedIpsViewModel.SelectedCargos;
            if(!selected_cargos.Any()) return false;
            foreach (var item in selected_cargos)
            {
                var ipsToCargo = new IPS2Cargo()
                {
                    Cargo = item,
                    IPS = IPS
                };
                IPS.IPS2Cargoes.Add(ipsToCargo);
            }

            return true;
        }

        public bool RedactToRemoved(object item, IEnumerable<object> Removeitionally)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            switch (item)
            {
                default:
                    throw new NotSupportedException(
                        $"редактирование элемента не возможно");

                case CLASSES.IPS IPS:
                    return RemoveCargoToTheSelectedIps(IPS, (ObservableCollection<Cargo>)Removeitionally);
            }
        }
        private bool RemoveCargoToTheSelectedIps(CLASSES.IPS IPS, ObservableCollection<Cargo> Cargos)
        {

            var cargoEditorToSelectedIpsViewModel = new CargoRemovedToSelectedIpsViewModel(IPS);
            var cargoEditToSelectedIpsWindow = new CargoRemoveToSelectedIpsWindow()
            {
                DataContext = cargoEditorToSelectedIpsViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            if (cargoEditToSelectedIpsWindow.ShowDialog() != true) return false;


            var selected_cargos = cargoEditorToSelectedIpsViewModel.SelectedCargos;
            if (selected_cargos is null) return false;

            foreach (var item in selected_cargos)
            {
                var item_to_remove = IPS.IPS2Cargoes.FirstOrDefault(carg => carg.Cargo == item && carg.IPS == IPS);
                IPS.IPS2Cargoes.Remove(item_to_remove);
            }

            return true;
        }

        public bool Confirm(string Message,
                            string Caption,
                            bool Exlamination = false) => MessageBox.Show(Message,
                                                                          Caption,
                                                                          MessageBoxButton.YesNo,
                                                                          Exlamination ? MessageBoxImage.Exclamation : MessageBoxImage.Question) == MessageBoxResult.Yes;

    }
}
