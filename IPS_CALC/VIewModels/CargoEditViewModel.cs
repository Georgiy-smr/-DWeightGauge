using IPS.DAL;
using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.VIewModels
{
    internal class CargoEditViewModel : ViewModel
    {
        private string _Name;

        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        public int Id { get; set; }

        public CargoEditViewModel(Cargo Cargo)
        {
            Name = Cargo.Name;
            Id = Cargo.Id;
        }
    }
}
