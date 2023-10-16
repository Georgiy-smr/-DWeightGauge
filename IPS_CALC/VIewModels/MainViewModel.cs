using IPS.DAL;
using IPS.Interfaces;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Classes = IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class MainViewModel : ViewModel
    {
        private IRepository<Classes.IPS> _RepositoryIPS;
        private IRepository<Cargo> _RepositoryCargo;

        /// <summary>
        /// Заголовок
        /// </summary>
        private string _Title = "ТЕСТИРОВАНИЕ";

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
			get => _Title;
			set
			{
				_Title = value;
			}
		}
        /// <summary>
        /// Текущая модель предстваления
        /// </summary>
        private ViewModel _CurrentViewModel;
        /// <summary>
        /// Текущая модель представления
        /// </summary>
        public ViewModel CurrentViewModel
        {
            get => _CurrentViewModel;
            set => Set(ref _CurrentViewModel, value);
        }
        #region ShowIpsViewCommnad
        private ICommand _CommandShowIPS;
        public ICommand ShowIpsViewCommnad
        {
            get => _CommandShowIPS != null ?
            _CommandShowIPS : new LambdaCommand(OnShowIpsViewCommnadExecuted, CanShowIpsViewCommnadExecute);
        }
        private bool CanShowIpsViewCommnadExecute(Object p) => true;

        private void OnShowIpsViewCommnadExecuted(Object p) => CurrentViewModel = new IpsViewModel(_RepositoryIPS);

        #endregion

        #region ShowCargosViewCommnad

        private ICommand _CommandShowCargos;
        public ICommand ShowCargosCommand
        {
            get => _CommandShowCargos != null ?
            _CommandShowCargos : new LambdaCommand(OnShowCargosCommandExecuted, CanShowCargosCommandExecute);
        }
        private bool CanShowCargosCommandExecute(Object p) => true;

        private void OnShowCargosCommandExecuted(Object p)
        {
            CurrentViewModel = new CargoViewModel(_RepositoryCargo);
        }

        #endregion
        public MainViewModel(IRepository<Classes.IPS> RepositoryIPS, IRepository<Classes.Cargo> RepositoryCargo)
		{
			_RepositoryIPS = RepositoryIPS;
			_RepositoryCargo = RepositoryCargo;

            var ips = _RepositoryIPS.Items.ToArray();
            var car = _RepositoryCargo.Items.ToArray();
        }
    }
}
