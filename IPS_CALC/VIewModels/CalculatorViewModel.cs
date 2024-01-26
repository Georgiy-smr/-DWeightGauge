using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using IPS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IPS_CALC.VIewModels
{
    public class CalculatorViewModel : ViewModel
    {
        private readonly IRepository<IPS.DAL.IPS> _RepositoryIps;
        public CalculatorViewModel(IRepository<IPS.DAL.IPS> RepositoryIPS)
        {
            _RepositoryIps = RepositoryIPS;
        }
        
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        private ObservableCollection<IPS.DAL.IPS> _CollectionIPS;
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        public ObservableCollection<IPS.DAL.IPS> CollectionIPS
        {
            get => _CollectionIPS;
            set => Set(ref _CollectionIPS, value);
        }
        
        #region Команда запрашивающая коллекцию ИПС и Грузов

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(object p) => true;

        private async Task OnLoadIPSCommandExecuted(object p)
        {
            CollectionIPS = new ObservableCollection<IPS.DAL.IPS>(await _RepositoryIps.Items.ToArrayAsync());
        }

        #endregion
        
        
        
    }
}