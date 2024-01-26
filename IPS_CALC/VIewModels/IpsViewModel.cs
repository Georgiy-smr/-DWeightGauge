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
using IPS.DAL;
using System.Linq;
using MediatR;
using IPS_CALC.Inftastructure.Mediatr;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IPS_CALC.VIewModels
{
    internal class IpsViewModel : ViewModel
    {
        private string _TestTitle = "Какое то название";
        public string TestTitle
        {
            get => _TestTitle;
            set
            {
                if(Set(ref _TestTitle, value))
                {
                    _EventService.RaiseEvent(nameof(TestTitle), this);
                }
            }
        }



        private readonly IUserDialog _UserDialog;
        private readonly IEventService _EventService;
        private readonly IMediator _Mediator;

        /// <summary>
        /// репозитоий ИПС
        /// </summary>
        private readonly IRepository<CLASS.IPS> _RepositoryIPS;
        private readonly IRepository<Cargo> _RepositoryCargos;


        #region Логика связанная с ИПС
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

        #region Команда запрашивающая коллекцию ИПС и Грузов

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(object p) => true;

        private async Task OnLoadIPSCommandExecuted(object p)
        {
            CollectionIPS = new ObservableCollection<CLASS.IPS>(await _RepositoryIPS.Items.ToArrayAsync());
            CollectionCargos = new ObservableCollection<Cargo>(await _RepositoryCargos.Items.ToArrayAsync());
        }

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

        #region Команда удаления грузов выбранный ИПС
        private ICommand _Command_RemoveCargoSelectedIPS;
        public ICommand RemoveCargoSelectedIPSCommand
        {
            get => _Command_RemoveCargoSelectedIPS != null ?
            _Command_RemoveCargoSelectedIPS : new LambdaCommand(On_Command_RemoveCargoSelectedIPS_CommandExecuted, Can_Command_RemoveCargoSelectedIPS_CommandExecute);
        }

        private bool Can_Command_RemoveCargoSelectedIPS_CommandExecute(Object p) => true;

        private void On_Command_RemoveCargoSelectedIPS_CommandExecuted(Object p)
        {
            var selected_ips = SelectedIps;

            if (!(_UserDialog.RedactToRemoved(selected_ips, CollectionCargos))) return;


            _RepositoryIPS.Update(selected_ips);

            CollectionIPS = new ObservableCollection<CLASS.IPS>(_RepositoryIPS.Items.ToArray());

            SelectedIps = selected_ips;

        }
        #endregion

        #region Команда добавления грузов выбранной ИПС

        private ICommand _CommandAddCargoToTheSelectedIps;
        public ICommand CommandAddCargoToTheSelectedIps
        {
            get => _CommandAddCargoToTheSelectedIps != null ?
            _CommandAddCargoToTheSelectedIps : new LambdaCommand(On_AddCargoToTheSelectedIps_CommandExecuted, Can_AddCargoToTheSelectedIps_CommandExecute);
        }
        private bool Can_AddCargoToTheSelectedIps_CommandExecute(object p) => SelectedIps != null;

        private void On_AddCargoToTheSelectedIps_CommandExecuted(object p)
        {
            var selected_ips = SelectedIps;
            if (!_UserDialog.RedactToAdded(selected_ips, CollectionCargos)) return;

            _RepositoryIPS.Update(selected_ips);

            CollectionIPS = new ObservableCollection<CLASS.IPS>(_RepositoryIPS.Items.ToArray());

            SelectedIps = selected_ips;
        }

        #endregion

        #region Команда редактирования ИПС

        private ICommand _CommandRedactSelectedIps;
        public ICommand RedactSelectedIpsCommand
        {
            get => _CommandRedactSelectedIps != null ?
            _CommandRedactSelectedIps : new LambdaCommand(On_RedactSelectedIpsCommand_CommandExecuted, Can_RedactSelectedIpsCommand_CommandExecute);
        }
        private bool Can_RedactSelectedIpsCommand_CommandExecute(object p) => !(SelectedIps is null);

        private void On_RedactSelectedIpsCommand_CommandExecuted(object p)
        {
            var edit_ips = SelectedIps;

            if (!_UserDialog.Edit(edit_ips)) return;

            _RepositoryIPS.Update(edit_ips);

            CollectionIPS = new ObservableCollection<CLASS.IPS>(_RepositoryIPS.Items.ToArray());

            SelectedIps = edit_ips;
        }




        #endregion

        /// <summary>
        /// Коллекция грузов выбранной ипс
        /// </summary>
        private ObservableCollection<Cargo> _CollectionCargos;
        /// <summary>
        /// Коллекция грузов выбранной ипс
        /// </summary>
        public ObservableCollection<Cargo> CollectionCargos
        {
            get => _CollectionCargos;
            set => Set(ref _CollectionCargos, value);
        }

        public IpsViewModel(IRepository<CLASS.IPS> RepositoryIPS,
                            IUserDialog UserDialog,
                            IRepository<Cargo> RepositoryCargos,
                            IEventService EventService) 
        {
            _UserDialog = UserDialog;
            _EventService = EventService;
            _RepositoryIPS = RepositoryIPS;
            _RepositoryCargos = RepositoryCargos;
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
