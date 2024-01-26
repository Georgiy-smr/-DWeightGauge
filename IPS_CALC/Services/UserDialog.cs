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
        public bool Edit(object item, IDictinaryEnumConvertor dictinaryEnum = null)
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
                    return EditCargo(Cargo, dictinaryEnum);
            }
        }
        private bool EditCargo(Cargo cargo, IDictinaryEnumConvertor dictinaryEnum)
        {
            var cargoEditorViewModel = new CargoEditViewModel(cargo, dictinaryEnum);

            var cargo_Editor_Window = new CargoEditWindow
            {
                DataContext = cargoEditorViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            if (cargo_Editor_Window.ShowDialog() != true) return false;

            cargo.Name = cargoEditorViewModel.Name;
            cargo.Density = cargoEditorViewModel.Density;
            cargo.Weight = cargoEditorViewModel.Weight;
            cargo.Type = (int)cargoEditorViewModel.CargoTypeSelected;
            return true;
        }

        private bool EditIPS(CLASSES.IPS iPS)
        {
            var ips_editor_ViewModel = new IPSEditorViewModel(iPS);

            var ips_editor_window = new IPSEditorWindow
            {
                DataContext = ips_editor_ViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            if (ips_editor_window.ShowDialog() != true) return false;

            iPS.Name = ips_editor_ViewModel.IpsName;
            iPS.MaxLimit= ips_editor_ViewModel.MaxLimit;
            iPS.LowLimit = ips_editor_ViewModel.LowLimit;
            iPS.Square = ips_editor_ViewModel.Square;
            iPS.Weight= ips_editor_ViewModel.Weight;
            iPS.Density = ips_editor_ViewModel.Dencity;
            iPS.AlfaCoefficient = ips_editor_ViewModel.a_Coef;
            iPS.BettaCoefficient = ips_editor_ViewModel.b_Coef;
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
            if(selected_cargos is null) return false;
            if(!selected_cargos.Any()) return false;
            foreach (Cargo item in selected_cargos)
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
