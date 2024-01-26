using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Models = IPS.DAL;
using IPS_CALC.Inftastructure.Commands;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Internal;
using IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class CargoRemovedToSelectedIpsViewModel : ViewModel
    {
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _TitleWindow = "Удаление груза";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string TitleWindow
        {
            get => _TitleWindow;
            set => Set(ref _TitleWindow, value);
        }
        #region Свойства ИПС
        /// <summary>
        /// Свойсво ИПС: Название
        /// </summary>
        private string _IpsName;
        /// <summary>
        /// Свойсво ИПС: Название
        /// </summary>
        public string IpsName
        {
            get => _IpsName;
            set
            {
                if (Set(ref _IpsName, value))
                    TitleWindow = $"Удаление грузов у {value}";

            }
        }
        #endregion

        #region Логика отображения, фильрация, сортировка грузов в составе ипс
        /// <summary>
        /// Коллекция грузов в состве редактипуемой ИПС. 
        /// Через set указывается источник данных для сортрировки и фильтра.
        /// </summary>
        private ObservableCollection<IPS.DAL.Cargo> _Cargos;
        /// <summary>
        /// Коллекция грузов в состве редактипуемой ИПС. 
        /// Через set указывается источник данных для сортрировки и фильтра.
        /// </summary>
        public ObservableCollection<IPS.DAL.Cargo> Cargos
        {
            get => _Cargos;
            set
            {
                if(Set(ref _Cargos, value))
                {
                    _Sort_Filt_CollectionCargos.Source = value;
                    OnPropertyChanged(nameof(CargosViewCollection));
                }
            }
        }
        /// <summary>
        /// Обьект фильрации и сортировки,
        /// через который проходит коллекция грузов Cargos
        /// </summary>
        private readonly CollectionViewSource _Sort_Filt_CollectionCargos;
        /// <summary>
        /// Коллекция грузов для отображения в разметке
        /// </summary>
        public ICollectionView CargosViewCollection => _Sort_Filt_CollectionCargos.View;
        #endregion

        #region Логика отображения грузов в коллекции на удаление
        /// <summary>
        /// Коллекция грузов на удаление из состава ИПС.
        /// По set обновалят источник фильрации и сортировки.
        /// </summary>
        private ObservableCollection<IPS.DAL.Cargo> _SelectedCargos;
        /// <summary>
        /// Коллекция грузов на удаление из состава ИПС.
        /// По set обновалят источник фильрации и сортировки.
        /// </summary>
        public ObservableCollection<IPS.DAL.Cargo> SelectedCargos
        {
            get => _SelectedCargos;
            set
            {
                if(Set(ref _SelectedCargos, value))
                {
                    _Sort_Filt_CollectionSelectedCargos.Source = value;
                    OnPropertyChanged(nameof(SelectedCargosView));
                }
            }
        }
        /// <summary>
        /// Объект фильрации и сортировки
        /// </summary>
        private readonly CollectionViewSource _Sort_Filt_CollectionSelectedCargos;
        /// <summary>
        /// Отображение выбранных грузов в коллекциб на удаление 
        /// </summary>
        public ICollectionView SelectedCargosView => _Sort_Filt_CollectionSelectedCargos.View;
        #endregion

        /// <summary>
        /// Выбранный груз в составе ИПС
        /// </summary>
        private IPS.DAL.Cargo _SelectedCargo;
        /// <summary>
        /// Выбранный груз в составе ИПС
        /// </summary>
        public IPS.DAL.Cargo SelectedCargo
        {
            get => _SelectedCargo;
            set => Set(ref _SelectedCargo, value);
        }
        /// <summary>
        /// Выбранный груз в коллекции грузов на удаление
        /// </summary>
        private IPS.DAL.Cargo _SelectedRemoveCargo;
        /// <summary>
        /// Выбранный груз в коллекции грузов на удаление
        /// </summary>
        public IPS.DAL.Cargo SelectedRemoveCargo
        {
            get => _SelectedRemoveCargo;
            set => Set(ref _SelectedRemoveCargo, value);
        }

        #region Перенисти груз в коллекцию для добавления

        private ICommand _CommandAddCargoToSelectedCargos;
        public ICommand CommandAddCargoToSelectedCargos
        {
            get => _CommandAddCargoToSelectedCargos != null ?
            _CommandAddCargoToSelectedCargos : new LambdaCommand(On_AddCargoToSelectedCargos_CommandExecuted, Can_AddCargoToSelectedCargos_CommandExecute);
        }
        private bool Can_AddCargoToSelectedCargos_CommandExecute(object p) => SelectedCargo != null;

        private void On_AddCargoToSelectedCargos_CommandExecuted(object p)
        {
            //var e = p as MouseButtonEventArgs;
            //var mouse = e.MouseDevice;
            
            var cargo_To_Add = SelectedCargo;

            if (SelectedCargos is null)
                SelectedCargos = new ObservableCollection<IPS.DAL.Cargo>();

            SelectedCargos.Add(cargo_To_Add);
            Cargos.Remove(SelectedCargo);
        }

        private void But_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Убрать из коллекции добавления
        private ICommand _Command_RemoveCargoSelected;
        public ICommand RemoveCargoSelectedCommand
        {
            get => _Command_RemoveCargoSelected != null ?
            _Command_RemoveCargoSelected : new LambdaCommand(On_RemoveCargoSelected_CommandExecuted, Can_RemoveCargoSelected_CommandExecute);
        }

        private bool Can_RemoveCargoSelected_CommandExecute(Object p) => SelectedRemoveCargo != null;

        private void On_RemoveCargoSelected_CommandExecuted(Object p)
        {
            var cargo_To_Add = SelectedRemoveCargo;

            if (SelectedCargos is null)
                SelectedCargos = new ObservableCollection<IPS.DAL.Cargo>();

            SelectedCargos.Remove(cargo_To_Add);
            Cargos.Add(cargo_To_Add);
        }
        #endregion

        #region Перенести все грузы в коллекцию добавления

        private ICommand _CommandAddAllCargoToSelectedCargos;
        public ICommand AddAllCargoToSelectedCargosCommand => _CommandAddAllCargoToSelectedCargos
            ?? new LambdaCommand(On_AddAllCargoToSelectedCargos_CommandExecuted, Can_AddAllCargoToSelectedCargos_CommandExecute);

        private bool Can_AddAllCargoToSelectedCargos_CommandExecute(object p) => _Sort_Filt_CollectionCargos.View.Any();

        private void On_AddAllCargoToSelectedCargos_CommandExecuted(object p) 
        {
            if (Cargos == null || Cargos.Count == 0) return;

            var cargos_to_selected = new ObservableCollection<IPS.DAL.Cargo>(SelectedCargos
                ?? new ObservableCollection<IPS.DAL.Cargo>());
            var temp_coolections_cargo = new List<IPS.DAL.Cargo>(Cargos);

            foreach (var item in _Sort_Filt_CollectionCargos.View)
            {
                var cargo = item as IPS.DAL.Cargo;
                if (cargo != null)
                    cargos_to_selected.Add(cargo);
            }
            SelectedCargos = cargos_to_selected;
            Cargos = new ObservableCollection<Cargo>(temp_coolections_cargo
                .Where(x => !cargos_to_selected.Contains(x)).ToList());
        }

        #endregion

        #region Перенести все грузы из коллекции добавления в коллекцию грузов в составе ИПС


        private ICommand _CommandRemoveAllCargoToSelectedCargos;
        public ICommand CommandRemoveAllCargoCommand
        {
            get => _CommandRemoveAllCargoToSelectedCargos != null ?
            _CommandRemoveAllCargoToSelectedCargos : new LambdaCommand(On_RemoveAllCargoCommand_CommandExecuted, Can_RemoveAllCargoCommand_CommandExecute);
        }

        private bool Can_RemoveAllCargoCommand_CommandExecute(object p) => _Sort_Filt_CollectionSelectedCargos.View.Any();

        private void On_RemoveAllCargoCommand_CommandExecuted(object p) 
        {
            if (SelectedCargos == null || SelectedCargos.Count == 0) return;

            var cargos = new ObservableCollection<IPS.DAL.Cargo>(Cargos
                                                                 ?? new ObservableCollection<IPS.DAL.Cargo>());
            var temp_coolections_cargo = new List<IPS.DAL.Cargo>(Cargos);

            foreach (var item in _Sort_Filt_CollectionSelectedCargos.View)
            {
                var cargo = item as IPS.DAL.Cargo;
                if (cargo != null)
                    cargos.Add(cargo);
            }
            Cargos = cargos;
            SelectedCargos = new ObservableCollection<Cargo>(temp_coolections_cargo
                .Where(x => !cargos.Contains(x)).ToList());
        }

        #endregion
        public CargoRemovedToSelectedIpsViewModel(IPS.DAL.IPS IPS)
        {
            _Sort_Filt_CollectionCargos = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(
                        nameof(Cargo.Name),
                        ListSortDirection.Ascending)
                }
            };
            _Sort_Filt_CollectionSelectedCargos = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(
                        nameof(Cargo.Name),
                        ListSortDirection.Ascending)
                }
            };

            _Sort_Filt_CollectionCargos.Filter += _FilterCagros;
            _Sort_Filt_CollectionSelectedCargos.Filter += _FilterCagros;

            IpsName = IPS.Name;

            var cargoes = IPS.IPS2Cargoes.Select(c => c.Cargo).ToArray();

            //if (cargoes.Length == 0) throw new ArgumentNullException("Нет свободных грузов на удаление");

            Cargos = new ObservableCollection<Cargo>(cargoes);
            SelectedCargos = new ObservableCollection<Cargo>();
        }

        /// <summary>
        /// Фильтруемый ключ.
        /// </summary>
        private string _FilterKey;
        /// <summary>
        /// Фильтруемый ключ.
        /// </summary>
        public string FilterKey
        {
            get => _FilterKey;
            set
            {
                if(Set(ref _FilterKey, value))
                {
                    _Sort_Filt_CollectionCargos.View.Refresh();
                    _Sort_Filt_CollectionSelectedCargos.View.Refresh();
                }
            }
        }
        /// <summary>
        /// Метод оброботки фильтрации. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _FilterCagros(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Cargo Cargo) || string.IsNullOrEmpty(FilterKey)) return;

            if (!(Cargo.Name.Contains(FilterKey)))
                e.Accepted = false;
        }
    }
}
