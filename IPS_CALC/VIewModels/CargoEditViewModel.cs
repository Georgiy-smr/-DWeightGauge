using IPS.DAL;
using IPS_CALC.VIewModels.Base;
using IPS_CALC.VIewModels.StackPanels;
using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.EnumsAndDictinary;
using IPS_CALC.Services.Interfaces;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;

namespace IPS_CALC.VIewModels
{
    internal class CargoEditViewModel : ViewModel
    {
        private string _Name;
        private readonly IDictinaryEnumConvertor _EnumConvertor;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private decimal _Weight;

        public decimal Weight
        {
            get => _Weight;
            set => Set(ref _Weight, value);
        }

        private decimal _Density;

        public decimal Density
        {
            get => _Density;
            set => Set(ref _Density, value);
        }

        private CargoType _CargoTypeSelected;
        public CargoType CargoTypeSelected
        {
            get => _CargoTypeSelected;
            set => Set(ref _CargoTypeSelected, value);
        }

        public Dictionary<CargoType, string> CargoEnumDictionary => _EnumConvertor.CargoEnumDictionary;

        public int Id { get; set; }

        public CargoEditViewModel(Cargo Cargo, IDictinaryEnumConvertor EnumConvertor)
        {
            _EnumConvertor = EnumConvertor;

            Name = Cargo.Name;
            Id = Cargo.Id;
            Weight = Cargo.Weight;
            Density = Cargo.Density;
            CargoTypeSelected = _EnumConvertor.CargoTypeToEnum(Cargo.Type);

            foreach (var e in Enum.GetValues(typeof(CargoType)).Cast<CargoType>())
            {
                EnumTypeCollections.Add(new BaseStackPanelViewModel<CargoType>() { Element = e });
            }

            foreach (var item in EnumTypeCollections)
            {
                var CargoType = item as BaseStackPanelViewModel<CargoType>;
                if (CargoType != null) 
                    CargoType.PropertyChanged += NameTypeVm_PropertyChanged;
            }

            EnumTypeCollections.CollectionChanged += EnumTypeCollections_CollectionChanged;


        }

        private void EnumTypeCollections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newVms = e.NewItems;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    // Проверяем на наличие новых элементов
                    if (newVms != null)
                    {
                        // Прогоняем все новые элементы коллекции
                        foreach (var item in newVms)
                        {
                            var nameTypeVm = item as BaseStackPanelViewModel<CargoType>;
                            if (nameTypeVm != null)
                                nameTypeVm.PropertyChanged += NameTypeVm_PropertyChanged;
                        }
                    }
                    break;
            }
        }

        public ObservableCollection<BaseStackPanelViewModel<CargoType>> EnumTypeCollections { get; set; } 
            = new ObservableCollection<BaseStackPanelViewModel<CargoType>>();

        /// <summary>
        /// Свойство Vm изменилось
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTypeVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var nameTypeVm = sender as BaseStackPanelViewModel<CargoType>;
            if (nameTypeVm != null)
            {
                // Проверяем событие нажатия на кнопку через свойство
                if (e.PropertyName == "IsClicked")
                {
                    CargoTypeSelected = nameTypeVm.Element!;
                    IsNameTypeSbOpened = false;
                    EnumTypeCollections.Remove(EnumTypeCollections.FirstOrDefault());
                }
            }
        }
        private bool _isNameTypeSbOpened;
        public bool IsNameTypeSbOpened
        {
            get => _isNameTypeSbOpened;
            set => Set(ref _isNameTypeSbOpened, value);
        }







    }
}
