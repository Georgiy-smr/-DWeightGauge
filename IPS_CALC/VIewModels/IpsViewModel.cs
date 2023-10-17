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
using System.Data;
using System.Windows.Data;
using System.ComponentModel;

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
            set
            {
                if (Set(ref _CollectionIPS, value))
                    _Colletcion_IPS_ViewSourse.Source = value;
                OnPropertyChanged(nameof(IPScollectionView));
            } 
        }
        /// <summary>
        /// Объект Сортировки и фильтра
        /// </summary>
        private readonly CollectionViewSource _Colletcion_IPS_ViewSourse;
        /// <summary>
        /// Отображение Объектра сортировки и фильтра
        /// </summary>
        public ICollectionView IPScollectionView => _Colletcion_IPS_ViewSourse.View;
        /// <summary>
        /// фильтр
        /// </summary>
        private string _FilterNameIPS;
        /// <summary>
        /// фильтр
        /// </summary>
        public string FilterNameIPS
        {
            get => _FilterNameIPS;

            set
            {
                if(Set(ref _FilterNameIPS, value))
                    _Colletcion_IPS_ViewSourse.View.Refresh();
                
            }
        }
        /// <summary>
        /// Выбранная ипс
        /// </summary>
        private CLASS.IPS _SelectedIps;
        /// <summary>
        /// Выбранная ипс
        /// </summary>
        public CLASS.IPS SelectedIps
        {
            get => _SelectedIps;
            set => Set(ref _SelectedIps, value);
        }

        #endregion
        #region LoadIpsCommand

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(Object p) => true;

        private async Task OnLoadIPSCommandExecuted(Object p) => CollectionIPS = new ObservableCollection<CLASS.IPS>(await _RepositoryIPS.Items.ToArrayAsync());

        #endregion
        public IpsViewModel(IRepository<CLASS.IPS> RepositoryIPS) 
        {
            _RepositoryIPS = RepositoryIPS;
            _Colletcion_IPS_ViewSourse = new CollectionViewSource
            {
               SortDescriptions =
                {
                    new SortDescription(nameof(CLASS.IPS.Name), ListSortDirection.Ascending)
                }
            };
            _Colletcion_IPS_ViewSourse.Filter += _Colletcion_IPS_ViewSourse_Filter;
        }

        private void _Colletcion_IPS_ViewSourse_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is CLASS.IPS ips) || string.IsNullOrEmpty(FilterNameIPS)) return;

            if (!(ips.Name.Contains(FilterNameIPS)))
                e.Accepted = false;
        }
    }
}
