using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IPS.Interfaces;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using Microsoft.EntityFrameworkCore;
using CLASS = IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class IpsViewModel : ViewModel
    {
        /// <summary>
        /// репозитоий ИПС
        /// </summary>
        private readonly IRepository<CLASS.IPS> _RepositoryIPS;

        #region ИПС
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        private ObservableCollection<CLASS.IPS> _CollectionIPS;
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        public ObservableCollection<CLASS.IPS> CollectionIPS
        {
            get => _CollectionIPS;
            set => Set(ref _CollectionIPS, value);
        }
        #endregion
        #region LoadIpsCommand

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(Object p) => true;

        private async Task OnLoadIPSCommandExecuted(Object p) => CollectionIPS = new ObservableCollection<CLASS.IPS>(await _RepositoryIPS.Items.ToArrayAsync());

        #endregion
        public IpsViewModel(IRepository<CLASS.IPS> RepositoryIPS) => _RepositoryIPS = RepositoryIPS;

    }
}
