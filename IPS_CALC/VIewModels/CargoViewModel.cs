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

namespace IPS_CALC.VIewModels
{
    internal class CargoViewModel : ViewModel
    {
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

        public CargoViewModel(IRepository<Cargo> RepositoryCargo)
        {
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
