using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.VIewModels.Base;
using CLASSES = IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class IPSEditorViewModel : ViewModel
    {
        public int IPS_id { get; set; }

        private string _IpsName;
        public string IpsName
        {
            get => _IpsName;
            set => Set(ref _IpsName, value);
        }

        private decimal _MaxLimit;
        public decimal MaxLimit
        {
            get => _MaxLimit;
            set => Set(ref _MaxLimit, value);
        }
        private decimal _LowLimit;

        public decimal LowLimit
        {
            get => _LowLimit;
            set => Set(ref _LowLimit, value);
        }

        private decimal _Square;
        public decimal Square
        {
            get => _Square;
            set => Set(ref _Square, value);
        }

        private decimal _Weight;
        public decimal Weight
        {
            get => _Weight;
            set => Set(ref _Weight, value);
        }

        private decimal _Dencity;
        public decimal Dencity
        {
            get => _Dencity;
            set => Set(ref _Dencity, value);
        }

        private decimal _a_Coef;

        public decimal a_Coef
        {
            get => _a_Coef;
            set => Set(ref _a_Coef, value);
        }

        private decimal _b_Coef;

        public decimal b_Coef
        {
            get => _b_Coef;
            set => Set(ref _b_Coef, value);
        }

        public IPSEditorViewModel(CLASSES.IPS IPS)
        {
            IpsName = IPS.Name;
            IPS_id = IPS.Id;
            MaxLimit = IPS.MaxLimit;
            LowLimit = IPS.LowLimit;
            Square = IPS.Square;
            Weight = IPS.Weight;
            Dencity = IPS.Density;
            a_Coef = IPS.AlfaCoefficient;
            b_Coef = IPS.BettaCoefficient;
        }

    }
}
