using IPS.DAL;
using IPS.Interfaces;
using IPS_CALC.VIewModels.Base;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.Inftastructure.Commands;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Windows.Data;
using IPS_CALC.Services.Interfaces;
using IPS_CALC.Services;
using System.Linq;

namespace IPS_CALC.VIewModels
{
    internal class CargoViewModel : ViewModel
    {
        private readonly IDictinaryEnumConvertor _DictinaryEnumConvertor;
        private IUserDialog _UserDialog;

        /// <summary>
        /// Репозиторий грузов
        /// </summary>
        private readonly IRepository<Cargo> _RepositoryCargo;
        /// <summary>
        /// Коллекция представления грузов
        /// </summary>
        private ObservableCollection<Cargo> _CargosCollections;
        /// <summary>
        /// Коллекция представления грузов
        /// </summary>
        public ObservableCollection<Cargo> CargosCollections
        {
            get => _CargosCollections;

            set
            {
                if(Set(ref _CargosCollections, value));
                _CargoViewSource.Source = value;
                OnPropertyChanged(nameof(CargosView));
            }
                
        }
        /// <summary>
        /// Объект WPF Выполняющий сортировку
        /// </summary>
        private readonly CollectionViewSource _CargoViewSource;
        /// <summary>
        /// Представление в разметке
        /// </summary>
        public ICollectionView CargosView => _CargoViewSource.View;

        /// <summary>
        /// Фильтр по имени 
        /// </summary>
        private string _CargoNameFilter;
        /// <summary>
        /// Фильтр по имени
        /// </summary>
        public string CargoNameFilter
        {
            get => _CargoNameFilter;
            set 
            {
                if (Set(ref _CargoNameFilter, value))
                    _CargoViewSource.View.Refresh();
            }
        }
        #region LoadCargoCommand

        private ICommand _CommandLoadCargo;
        public ICommand LoadCargoCommand => _CommandLoadCargo ?? new LambdaCommandAsync(OnLoadCargoCommandExecuted, CaLoadCargoCommandExecute);
        private bool CaLoadCargoCommandExecute(Object p) => true;

        private async Task OnLoadCargoCommandExecuted(Object p) => CargosCollections = new ObservableCollection<Cargo>(await _RepositoryCargo.Items.ToArrayAsync());

        #endregion

        #region Add Cargo

        private ICommand _CommandCargoAdded;
        public ICommand CommandCargoAdded => _CommandCargoAdded ?? new LambdaCommand(OnCargoAddCommandExecuted, CanCargoAddCommandExecute);

        private bool CanCargoAddCommandExecute(object p) => true;

        private void OnCargoAddCommandExecuted(object p)
        {
            var new_cargo = new Cargo();

            if (!_UserDialog.Edit(new_cargo, _DictinaryEnumConvertor)) return;

            CargosCollections.Add(_RepositoryCargo.Add(new_cargo));

            CargoSelected = new_cargo;
        }

        #endregion

        #region Redact Cargo

        private ICommand _Command_RedactCagroSelected;
        public ICommand RedactCargoSelectedCommand
        {
            get => _Command_RedactCagroSelected != null ?
            _Command_RedactCagroSelected : new LambdaCommand(On_RedactCargoSelected_CommandExecuted, Can_NAME_CommandExecute);
        }

        private bool Can_NAME_CommandExecute(Object p) => true;

        private void On_RedactCargoSelected_CommandExecuted(Object p)
        {
            var cargo_redact = CargoSelected;

            if (!_UserDialog.Edit(cargo_redact, _DictinaryEnumConvertor)) return;

            _RepositoryCargo.Update(cargo_redact);

            CargosCollections = new ObservableCollection<Cargo>(_RepositoryCargo.Items);

            CargoSelected = cargo_redact;
            OnPropertyChanged(PropertyName: nameof(CargoSelected));
        }


        #endregion

        public string CargoType => _CargoSelected != null ? _DictinaryEnumConvertor.CargoTypeToString(_CargoSelected.Type) : null;

        private Cargo _CargoSelected;
        /// <summary>
        /// Selected Cargo 
        /// </summary>
        public Cargo CargoSelected
        { 
            get => _CargoSelected;
            set 
            {
                OnPropertyChanged(PropertyName: nameof(CargoType));
                if (Set(ref _CargoSelected, value))
                    OnPropertyChanged(PropertyName: nameof(CargoType));
                
            }
        }

        #region Remove Cargo

        private ICommand _CommandCargoRemove;
        public ICommand CommandCargoRemove => _CommandCargoRemove ?? new LambdaCommand(OnCommandCargoRemoveExecuted, CanCommandCargoRemoveExecute);

        private bool CanCommandCargoRemoveExecute(object p) => !(CargoSelected is null) || !(p is null);

        private void OnCommandCargoRemoveExecuted(object p) 
        {
            var cargo_to_remove = p ?? CargoSelected;

            if (!_UserDialog.Confirm("Удаление груза", "Delite")) return;

            var cargo_remove = CargoSelected;

            _RepositoryCargo.Remove(cargo_remove.Id);
            CargosCollections.Remove(cargo_remove);

            if (ReferenceEquals(CargoSelected, cargo_to_remove))
                CargoSelected = null;
        }

        #endregion


        public CargoViewModel(
            IRepository<Cargo> RepositoryCargo, 
            IUserDialog UserDialog,
            IDictinaryEnumConvertor DictinaryEnumConvertor
            )
        {
            _DictinaryEnumConvertor = DictinaryEnumConvertor;
            _UserDialog = UserDialog;
            _RepositoryCargo = RepositoryCargo;
            _CargoViewSource = new CollectionViewSource
            {
                SortDescriptions = {
                    new SortDescription(nameof(Cargo.Name), ListSortDirection.Ascending) 
                }
            };
            _CargoViewSource.Filter += _CargoViewSource_Filter;
        }

        private void _CargoViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Cargo cargo) || string.IsNullOrEmpty(CargoNameFilter)) return;

            if (!(cargo.Name.Contains(CargoNameFilter)))
                e.Accepted = false;
        }
    }
}
