using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Models = IPS.DAL;
using IPS_CALC.Inftastructure.Commands;
using System.Windows.Input;

namespace IPS_CALC.VIewModels
{
    internal class CargoRemovedToSelectedIpsViewModel : ViewModel
    {
        private string _TitleWindow = "Удаление груза";

        public string TitleWindow
        {
            get => _TitleWindow;
            set => Set(ref _TitleWindow, value);
        }
        private string _IpsName;

        public string IpsName
        {
            get => _IpsName;
            set
            {
                if (Set(ref _IpsName, value))
                {
                    TitleWindow = $"Удаление грузов у {value}";
                }
            }
        }
        /// <summary>
		/// Грузы редактируемой ИПС
		/// </summary>
		private ObservableCollection<Models.Cargo> _Cargos;
        /// <summary>
        /// Грузы редактируемой ИПС
        /// </summary>
        public ObservableCollection<Models.Cargo> Cargos
        {
            get => _Cargos;
            set => Set(ref _Cargos, value);
        }
        /// <summary>
        /// Выбранный груз на удаление
        /// </summary>
        private Models.Cargo _SelectedCargo;
        /// <summary>
        /// Выбранный груз на удаление
        /// </summary>
        public Models.Cargo SelectedCargo
        {
            get => _SelectedCargo;
            set => Set(ref _SelectedCargo, value);
        }
        /// <summary>
        /// Грузы который выбрали для ИПС
        /// </summary>
        private ObservableCollection<Models.Cargo> _SelectedCargos;
        /// <summary>
        /// Грузы который выбрали для ИПС
        /// </summary>
        public ObservableCollection<Models.Cargo> SelectedCargos
        {
            get => _SelectedCargos;
            set => Set(ref _SelectedCargos, value);
        }
        /// <summary>
        /// Выбранный груз в коллекции грузов на удаление
        /// </summary>
        private Models.Cargo _SelectedRemoveCargo;
        /// <summary>
        /// Выбранный груз в коллекции грузов на удаление
        /// </summary>
        public Models.Cargo SelectedRemoveCargo
        {
            get => _SelectedRemoveCargo;
            set => Set(ref _SelectedRemoveCargo, value);
        }


        #region Перенисти груз в коллекцию для добавления

        private ICommand _CommandAddCargoToSelectedCargos;
        public ICommand CommandAddCargoToSelectedCargos
        {
            get => _CommandAddCargoToSelectedCargos != null ?
            _CommandAddCargoToSelectedCargos : new LambdaCommand(On_AddCargoToSelectedCargos_CommandExecuted, Can_AddCargoToSelectedCargos_CommandExecute);
        }
        private bool Can_AddCargoToSelectedCargos_CommandExecute(Object p) => SelectedCargo != null;

        private void On_AddCargoToSelectedCargos_CommandExecuted(Object p)
        {
            var cargo_To_Add = SelectedCargo;

            if (SelectedCargos is null)
                SelectedCargos = new ObservableCollection<Models.Cargo>();

            SelectedCargos.Add(cargo_To_Add);
            Cargos.Remove(SelectedCargo);
        }
        #endregion
        #region Убрать из коллекции добавления
        private ICommand _Command_RemoveCargoSelected;
        public ICommand RemoveCargoSelectedCommand
        {
            get => _Command_RemoveCargoSelected != null ?
            _Command_RemoveCargoSelected : new LambdaCommand(On_RemoveCargoSelected_CommandExecuted, Can_RemoveCargoSelected_CommandExecute);
        }

        private bool Can_RemoveCargoSelected_CommandExecute(Object p) => SelectedRemoveCargo != null;

        private void On_RemoveCargoSelected_CommandExecuted(Object p)
        {
            var cargo_To_Add = SelectedRemoveCargo;

            if (SelectedCargos is null)
                SelectedCargos = new ObservableCollection<Models.Cargo>();

            SelectedCargos.Remove(cargo_To_Add);
            Cargos.Add(cargo_To_Add);
        }
        #endregion

        public CargoRemovedToSelectedIpsViewModel(Models.IPS IPS)
        {
            IpsName = IPS.Name;

            var cargoes = IPS.IPS2Cargoes.Select(c => c.Cargo).ToArray();

            if (cargoes.Length == 0) throw new ArgumentNullException("Нет свободных грузов на удаление");

            Cargos = new ObservableCollection<Models.Cargo>(cargoes);
        }

        
    }
}
