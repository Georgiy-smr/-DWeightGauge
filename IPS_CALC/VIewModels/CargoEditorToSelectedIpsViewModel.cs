using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ClassIps = IPS.DAL;
using System.Linq;
using IPS_CALC.Inftastructure.Commands;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel;
using System.Windows.Data;
using System.Security.Claims;

namespace IPS_CALC.VIewModels
{
    internal class CargoEditorToSelectedIpsViewModel : ViewModel
    {
		private string _TitleWindow = "Добавление груза";

		public string TitleWindow
        {
			get => _TitleWindow;
			set => Set(ref _TitleWindow, value);
		}

		private string _IpsName;

		public string IpsName
		{
			get => _IpsName;
			set
			{
				if(Set(ref _IpsName, value))
				{
					TitleWindow = $"Добавление груза к {value}";
				}
			}
		}
        #region ViewFreeCargos
        /// <summary>
        /// Объект Сортировки и фильтра 
        /// </summary>
        private readonly CollectionViewSource _Colletcion_Cargos_ViewSourse;
        /// <summary>
        /// Отображение Объектра сортировки и фильтра
        /// </summary>
        public ICollectionView CargosСollectionView => _Colletcion_Cargos_ViewSourse.View;
        /// <summary>
        /// Отображение свободных грузов 
        /// </summary>
        private ObservableCollection<IPS.DAL.Cargo> _FreeCargos;
        /// <summary>
        /// Отображение свободных грузов 
        /// </summary>
        public ObservableCollection<IPS.DAL.Cargo> FreeCargos
        {
            get => _FreeCargos;
            set
            {
                if (Set(ref _FreeCargos, value))
                {
                    _Colletcion_Cargos_ViewSourse.Source = value;
                    OnPropertyChanged(nameof(CargosСollectionView));
                }
            }
        }
        #endregion

        #region ViewSelectedCargos
        /// <summary>
        /// Объект Сортировки и фильтрации выбранных грузов
        /// </summary>
        private readonly CollectionViewSource _Colletcion_Cargos_Selected_ViewSourse;
            
        /// <summary>
        /// Отображение сортировки и фильтра
        /// </summary>
        public ICollectionView Cargos_Selected_СollectionView => _Colletcion_Cargos_Selected_ViewSourse.View;
        /// <summary>
        /// Отображение выбранных грузов
        /// </summary>
        private ObservableCollection<IPS.DAL.Cargo> _SelectedCargos;
        /// <summary>
        /// Отображение выбранных грузов
        /// </summary>
        public ObservableCollection<IPS.DAL.Cargo> SelectedCargos
        {
            get => _SelectedCargos;
            set
            {
                if(Set(ref _SelectedCargos, value))
                {
                    _Colletcion_Cargos_Selected_ViewSourse.Source = value;
                    OnPropertyChanged(nameof(Cargos_Selected_СollectionView));
                }
            } 
        }
        #endregion


        /// <summary>
        /// Фильтруемое слово
        /// </summary>
        private string _FilterKey;
        /// <summary>
        /// Фильтруемое слово
        /// </summary>
        public string FilterKey
        {
            get => _FilterKey;
            set
            {
                if(Set(ref _FilterKey, value))
                {
                    _Colletcion_Cargos_ViewSourse.View.Refresh();
                    _Colletcion_Cargos_Selected_ViewSourse.View.Refresh();
                }
                    
            }
        }


        /// <summary>
        /// Выбранный груз для добавления
        /// </summary>
        private IPS.DAL.Cargo _SelectedCargo;
		/// <summary>
		/// Выбранный груз для добавления
		/// </summary>
		public IPS.DAL.Cargo SelectedCargo
        {
			get => _SelectedCargo;
			set => Set(ref _SelectedCargo, value);
		}
        /// <summary>
        /// Выбранный груз для удаления
        /// </summary>
        private IPS.DAL.Cargo _SelectedRemoveCargo;
        /// <summary>
        /// Выбранный груз для удаления
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
        private bool Can_AddCargoToSelectedCargos_CommandExecute(Object p) => SelectedCargo != null;

        private void On_AddCargoToSelectedCargos_CommandExecuted(Object p)
        {

            var cargo_To_Add = SelectedCargo;

            if (SelectedCargos is null)
                SelectedCargos = new ObservableCollection<IPS.DAL.Cargo>();

            SelectedCargos.Add(cargo_To_Add);
            FreeCargos.Remove(SelectedCargo);

            SelectedCargo = FreeCargos.FirstOrDefault();

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
            FreeCargos.Add(cargo_To_Add);

            SelectedRemoveCargo = SelectedCargos.FirstOrDefault();
        }
        #endregion

        #region Перенести все элементы из коллекции грузов в коллекцию на добавление

        private ICommand _CommandAddAllCargo;
        public ICommand AddAllCargoCommand
        {
            get => _CommandAddAllCargo != null ?
            _CommandAddAllCargo : new LambdaCommand(OnAddAllCargoCommandExecuted, CanAddAllCargoCommandExecute);
        }

        private bool CanAddAllCargoCommandExecute(object p) => !(FreeCargos is null);

        private void OnAddAllCargoCommandExecuted(object p) 
        {
            if(!CargosСollectionView.Any()) return;


            SelectedCargos = SelectedCargos ?? new ObservableCollection<IPS.DAL.Cargo>();

            var cargos = CargosСollectionView;

            var temp_coolections_cargo = new List<IPS.DAL.Cargo>(FreeCargos);

            foreach (var item in cargos)
            {
                var cargo = item as IPS.DAL.Cargo;
                if(cargo != null)
                    SelectedCargos.Add(cargo);

                temp_coolections_cargo.Remove(cargo);

            }

            this.FreeCargos = new ObservableCollection<IPS.DAL.Cargo>(FreeCargos.Where(c => temp_coolections_cargo.Contains(c)).ToArray());

        }

        #endregion

        #region Убрать все грузы из списка добавление
        private ICommand _CommandRemoveAllCargoSelectedCollections;
        public ICommand RemoveAllCargoSelectedCollectionsCommand
        {
            get => _CommandRemoveAllCargoSelectedCollections != null ?
            _CommandRemoveAllCargoSelectedCollections : new LambdaCommand(OnRemoveAllCargoSelectedCollectionsCommandExecuted, CanRemoveAllCargoSelectedCollectionsCommandExecute);
        }
        private bool CanRemoveAllCargoSelectedCollectionsCommandExecute(object p) => SelectedCargos != null;

        private void OnRemoveAllCargoSelectedCollectionsCommandExecuted(object p) 
        {
            //if (!SelectedCargos.Any())
            //    return;

            //foreach (var item in SelectedCargos)
            //    FreeCargos.Add(item);

            //SelectedCargos.Clear();

            if (!Cargos_Selected_СollectionView.Any()) return;

            var cargos = Cargos_Selected_СollectionView;

            var temp_coolections_cargo = new List<IPS.DAL.Cargo>(SelectedCargos);

            foreach (var item in cargos)
            {
                var cargo = item as IPS.DAL.Cargo;
                if (cargo != null && !FreeCargos.Contains(cargo))
                    FreeCargos.Add(cargo);
                
                temp_coolections_cargo.Remove(cargo);

            }

            //this.SelectedCargos = new ObservableCollection<Models.Cargo>(SelectedCargos.Where(c => temp_coolections_cargo.Contains(c)).ToArray());
            this.SelectedCargos = new ObservableCollection<IPS.DAL.Cargo>(temp_coolections_cargo);

        }

        #endregion



        public CargoEditorToSelectedIpsViewModel(IPS.DAL.IPS Ips, IEnumerable<IPS.DAL.Cargo> Cargos)
		{
			IpsName = Ips.Name;
            var CargosFilt = Cargos.Where(c => !Ips.IPS2Cargoes.Select(g => g.Cargo).Contains(c)).ToArray();


            _Colletcion_Cargos_ViewSourse = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(
                        nameof(IPS.DAL.Cargo.Name),
                        ListSortDirection.Ascending)
                }
            };
            _Colletcion_Cargos_ViewSourse.Filter += _Colletcion_Cargos_ViewSourse_Filter;

            _Colletcion_Cargos_Selected_ViewSourse = new CollectionViewSource
            {
                SortDescriptions =
                {
                    new SortDescription(
                        nameof(IPS.DAL.Cargo.Name),
                        ListSortDirection.Ascending)
                }
            };

            _Colletcion_Cargos_Selected_ViewSourse.Filter += _Colletcion_Cargos_ViewSourse_Filter;

            FreeCargos = new ObservableCollection<IPS.DAL.Cargo>(CargosFilt);
            SelectedCargos = new ObservableCollection<IPS.DAL.Cargo>();
        }
        
        private void _Colletcion_Cargos_ViewSourse_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is IPS.DAL.Cargo Cargo) || string.IsNullOrEmpty(FilterKey)) return;

            if (!(Cargo.Name.Contains(FilterKey)))
                e.Accepted = false;
        }
    }
}