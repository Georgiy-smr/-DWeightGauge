using IPS.DAL;
using IPS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes = IPS.DAL;

namespace IPS_CALC.VIewModels
{
    internal class MainViewModel
    {
        private IRepository<Classes.IPS> _RepositoryIPS;
        private IRepository<Cargo> _RepositoryCargo;

        /// <summary>
        /// Заголовок
        /// </summary>
        private string _Title = "ТЕСТИРОВАНИЕ";

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
			get => _Title;
			set
			{
				_Title = value;
			}
		}

		public MainViewModel(IRepository<Classes.IPS> RepositoryIPS, IRepository<Classes.Cargo> RepositoryCargo)
		{
			_RepositoryIPS = RepositoryIPS;
			_RepositoryCargo = RepositoryCargo;

            var ips = _RepositoryIPS.Items.ToArray();
            var car = _RepositoryCargo.Items.ToArray();
        }
    }
}
