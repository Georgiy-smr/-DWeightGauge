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

        public IPSEditorViewModel(CLASSES.IPS IPS)
        {
            IpsName = IPS.Name;
            IPS_id = IPS.Id;
        }

    }
}
