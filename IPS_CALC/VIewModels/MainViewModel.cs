using IPS.DAL;
using IPS.Interfaces;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.Inftastructure.Mediatr;
using IPS_CALC.Services;
using IPS_CALC.Services.Interfaces;
using IPS_CALC.VIewModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Classes = IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class MainViewModel : ViewModel,
        INotificationHandler<PropertyViewModelChanged>
    {
        private IUserDialog _UserDialog;
        private IRepository<Classes.IPS> _RepositoryIPS;
        private IRepository<Cargo> _RepositoryCargo;
        private readonly IMediator _Mediator;
        private readonly IEventService _EventService;
        private string _Title = "Калькулятор ГПМ";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            
        }

        private string _testPropy;

        public string TestPropy
        {
            get => _testPropy;
            set => _testPropy = value;
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
            set
            {
                Set(ref _CurrentViewModel, value);
                    //Title = $"{CurrentViewModel.GetType()}";
            }
        }
        #region ShowIpsViewCommnad

        private ICommand _CommandShowIPS;
        public ICommand ShowIpsViewCommnad
        {
            get => _CommandShowIPS != null ?
            _CommandShowIPS : new LambdaCommand(OnShowIpsViewCommnadExecuted, CanShowIpsViewCommnadExecute);
        }
        private bool CanShowIpsViewCommnadExecute(Object p) => !(CurrentViewModel is IpsViewModel);

        private void OnShowIpsViewCommnadExecuted(Object p) => CurrentViewModel = new IpsViewModel(
            RepositoryIPS: _RepositoryIPS,
            UserDialog: _UserDialog,
            RepositoryCargos: _RepositoryCargo,
            EventService: _EventService);

        #endregion

        #region ShowCargosViewCommnad

        private ICommand _CommandShowCargos;
        public ICommand ShowCargosCommand
        {
            get => _CommandShowCargos != null ?
            _CommandShowCargos : new LambdaCommand(OnShowCargosCommandExecuted, CanShowCargosCommandExecute);
        }
        private bool CanShowCargosCommandExecute(Object p) => !(CurrentViewModel is CargoViewModel);

        private void OnShowCargosCommandExecuted(Object p) => CurrentViewModel = new CargoViewModel(_RepositoryCargo, _UserDialog);

        #endregion

        public async Task Handle(PropertyViewModelChanged notification, CancellationToken cancellationToken)
        {


            //await Task.Run(() => _Dispatcher.InvokeAsync(()=> HandeNotificationAsync(viewmodel.TestTitle)));
            //await HandeNotificationAsync(viewmodel.TestTitle);
            //HandeNotificationAsync(viewmodel.TestTitle)).ConfigureAwait(false)
            await _Dispatcher.InvokeAsync(() => HandeNotificationAsync(((IpsViewModel)notification.ViewModel).TestTitle));

        }
        private async Task HandeNotificationAsync(string title) => await Task.Run(() => Title = title);

        public MainViewModel(IRepository<Classes.IPS> RepositoryIPS,
                             IRepository<Classes.Cargo> RepositoryCargo,
                             IUserDialog UserDialog,
                             IMediator Mediator,
                             Dispatcher Dispatcher,
                             IEventService EventService)
		{
            _UserDialog = UserDialog;
			_RepositoryIPS = RepositoryIPS;
			_RepositoryCargo = RepositoryCargo;
            _Mediator = Mediator;
            _Dispatcher = Dispatcher;
            _EventService = EventService;
            _EventService.SomeEvent += _EventService_SomeEvent;
        }

        private async void _EventService_SomeEvent(string PropName, object VeiwModel)
        {
            var view_model = VeiwModel;
            switch (view_model)
            {
                case IpsViewModel ipsViewModel:
                    await IpsViewModelHandeNotificationAsync(ipsViewModel, PropName).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
           
            //Title = view_model.TestTitle;
        }
        private async Task IpsViewModelHandeNotificationAsync(IpsViewModel ipsViewModel, string propertyname)
        {
            switch (propertyname)
            {
                case "TestTitle":
                    await Task.Run(() => Title = ipsViewModel.TestTitle).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }
    }
}
