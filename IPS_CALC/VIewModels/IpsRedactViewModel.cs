using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.VIewModels
{
    internal class IpsRedactViewModel : ViewModel
    {
		private string _Title;

		public string Title
        {
			get => _Title;
			set => Set(ref _Title, value);
		}
		private string _NameIps;

		public string NameIps
        {
			get => _NameIps;
			set => Set(ref _NameIps, value);
		}

	}
}
