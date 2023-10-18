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
using IPS_CALC.Services.Interfaces;
using IPS_CALC.Services;

namespace IPS_CALC.VIewModels
{
    internal class IpsViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;

        /// <summary>
        /// репозитоий ИПС
        /// </summary>
        private readonly IRepository<CLASS.IPS> _RepositoryIPS;

        #region Логика связанная с ИСП
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

        #region Команда запрашивающая грузы по загрузке окна

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(Object p) => true;

        private async Task OnLoadIPSCommandExecuted(Object p) => CollectionIPS = new ObservableCollection<CLASS.IPS>(await _RepositoryIPS.Items.ToArrayAsync());

        #endregion

        #region Команда добавления ИПС 

        private ICommand _CommandAddIPS;
        public ICommand CommandAddIPS => _CommandAddIPS ??
            new LambdaCommand(OnAddIPSCommandExecuted,
                              CanAddIPSCommandExecute);
        private bool CanAddIPSCommandExecute(Object p) => true;
        private void OnAddIPSCommandExecuted(Object p)
        {
            var new_ips = new CLASS.IPS();

            if (!_UserDialog.Edit(new_ips)) return;

            CollectionIPS.Add(_RepositoryIPS.Add(new_ips));

            SelectedIps = new_ips;

        }

        #endregion

        #region Команда удаления ИПC
        private ICommand _CommandRemoveIPS;
        public ICommand CommandRemoveIPS => _CommandRemoveIPS ?? 
            new LambdaCommand(
            OnCommandRemoveIPSExecuted,
            CanCommandRemoveIPSExecute);
        private bool CanCommandRemoveIPSExecute(Object p) => !(p is null) || !(SelectedIps is null);

        private void OnCommandRemoveIPSExecuted(Object p)
        {
            var ips_to_remove = p ?? SelectedIps;

            var remove_book = SelectedIps;
            if (!_UserDialog.Confirm(
                $"Желаете удалить ИПС {remove_book.Name}", "Удаление"))
                return;

            _RepositoryIPS.Remove(remove_book.Id);
            CollectionIPS.Remove(remove_book);

            if (ReferenceEquals(SelectedIps, ips_to_remove))
                SelectedIps = null;
        }

        #endregion

        public IpsViewModel(IRepository<CLASS.IPS> RepositoryIPS,
                            IUserDialog UserDialog) 
        {
            _UserDialog = UserDialog;

            _RepositoryIPS = RepositoryIPS;
            _Colletcion_IPS_ViewSourse = new CollectionViewSource
            {
               SortDescriptions =
                {
                    new SortDescription(
                        nameof(CLASS.IPS.Name),
                        ListSortDirection.Ascending)
                }
            };
            _Colletcion_IPS_ViewSourse.Filter += _Colletcion_IPS_ViewSourse_Filter;
        }

        /// <summary>
        /// Фильтрация 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Colletcion_IPS_ViewSourse_Filter(object sender,
                                                       FilterEventArgs e)
        {
           
            if (!(e.Item is CLASS.IPS ips)
                || string.IsNullOrEmpty(FilterNameIPS)) return;

            if (!(ips.Name.Contains(FilterNameIPS)))
                e.Accepted = false;
        }
    }
}
