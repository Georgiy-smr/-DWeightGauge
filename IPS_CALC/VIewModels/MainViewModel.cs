using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.VIewModels
{
    internal class MainViewModel
    {
		private string _Title = "ТЕСТИРОВАНИЕ";

		public string Title
        {
			get => _Title;
			set
			{
				_Title = value;
			}
		}

	}
}
