using IPS.DAL;
using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.EnumsAndDictinary;
using IPS_CALC.Services.Interfaces;
using System.ComponentModel;


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

        public Dictionary<CargoType, string> CargoEnumDictinary => _EnumConvertor.CargoEnumDictinary;

        public int Id { get; set; }

        public CargoEditViewModel(Cargo Cargo, IDictinaryEnumConvertor EnumConvertor)
        {
            _EnumConvertor = EnumConvertor;

            Name = Cargo.Name;
            Id = Cargo.Id;
            Weight = Cargo.Weight;
            Density = Cargo.Density;
            CargoTypeSelected = _EnumConvertor.CargoTypeToEnum(Cargo.Type);
        }
    }
}
