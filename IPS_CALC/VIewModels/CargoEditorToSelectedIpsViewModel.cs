using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Models = IPS.DAL;
using System.Linq;

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
		/// <summary>
		/// Грузы для добавления 
		/// </summary>
		private ObservableCollection<Models.Cargo> _Cargos;
        /// <summary>
        /// Грузы для добавления 
        /// </summary>
        public ObservableCollection<Models.Cargo> Cargos
        {
			get => _Cargos;
			set => Set(ref _Cargos, value);
		}

		private Models.Cargo _SelectedCargo;
		public Models.Cargo SelectedCargo
        {
			get => _SelectedCargo;
			set => Set(ref _SelectedCargo, value);
		}




		public CargoEditorToSelectedIpsViewModel(Models.IPS Ips, IEnumerable<Models.Cargo> Cargos)
		{
			IpsName = Ips.Name;
			var CargosFilt = Cargos.Where(c => !Ips.IPS2Cargoes.Select(g => g.Cargo).Contains(c)).ToArray();

            if (CargosFilt.Length == 0) throw new ArgumentNullException("Нет свободных грузов для добавления");

            //Фильтр грузов из репозитория по грузам которые уже есть у нашей ипс
            this.Cargos = new ObservableCollection<Models.Cargo>(CargosFilt);

        }


	}
}
