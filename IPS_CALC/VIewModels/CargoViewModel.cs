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
            set => Set(ref _CargosCollections, value);
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
        }
    }
}
