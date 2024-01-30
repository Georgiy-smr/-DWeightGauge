using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using IPS.Interfaces;
using Microsoft.EntityFrameworkCore;
using IPS_CALC.Models;
using IPS_CALC.Services.Interfaces;
using System.Linq;
using IPS_CALC.EnumsAndDictinary;
using System;

namespace IPS_CALC.VIewModels
{
    public class CalculatorViewModel : ViewModel
    {
        private readonly ICalculate<CalculationResultCargo> _CalculatorCargo;
        private readonly ICalculate<CalculationResultPressure> _CalculatorPressure;
        private readonly IRepository<IPS.DAL.IPS> _RepositoryIps;

        public CalculatorViewModel(
            IRepository<IPS.DAL.IPS> RepositoryIPS,
            ICalculate<CalculationResultCargo> calculatorCargo,
            ICalculate<CalculationResultPressure> calculatorPressure)
        {
            _CalculatorCargo = calculatorCargo;
            _RepositoryIps = RepositoryIPS;
            _CalculatorPressure = calculatorPressure;
        }
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        private ObservableCollection<IPS.DAL.IPS> _CollectionIPS;
        /// <summary>
        /// Коллекция отображения ИПС
        /// </summary>
        public ObservableCollection<IPS.DAL.IPS> CollectionIPS
        {
            get => _CollectionIPS;
            set => Set(ref _CollectionIPS, value);
        }
        /// <summary>
        /// Выбранная ИПС
        /// </summary>
        private IPS.DAL.IPS _IpsSelected;
        /// <summary>
        /// Выбранная ИПС
        /// </summary>
        public IPS.DAL.IPS IpsSelected
        {
            get => _IpsSelected;
            set
            {
                if (Set(ref _IpsSelected, value))
                    TargetPressure = 0;
            }
        }
        /// <summary>
        /// Температура
        /// </summary>
        private double _Temperature = 20;
        /// <summary>
        /// Температура
        /// </summary>
        public double Temperature
        {
            get => _Temperature;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Temperature, newvalue))
                    return;
                OnPropertyChanged(nameof(ActualPressureWhereNoKettlebell));
                OnPropertyChanged(nameof(ActualPressureAllCargoes));
            }
        }
        /// <summary>
        /// Влажность
        /// </summary>
        private double _Humidity = 45;
        /// <summary>
        /// Влажность
        /// </summary>
        public double Humidity 
        {
            get => _Humidity;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Humidity, newvalue))
                    return;

                OnPropertyChanged(nameof(ActualPressureWhereNoKettlebell));
                OnPropertyChanged(nameof(ActualPressureAllCargoes));
            }
        }
        /// <summary>
        /// Барометрическое давление
        /// </summary>
        private double _Baro = 99.96;
        /// <summary>
        /// Барометрическое давление
        /// </summary>
        public double Baro
        {
            get => _Baro;
            set
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if (!Set(ref _Baro, newvalue))
                    return;

                OnPropertyChanged(nameof(ActualPressureWhereNoKettlebell));
                OnPropertyChanged(nameof(ActualPressureAllCargoes));
            }
        }

        /// <summary>
        /// Необходимое давление
        /// </summary>
        private double _TargetPressure;
        /// <summary>
        /// Необходимое давление
        /// </summary>
        public double TargetPressure
        {
            get => _TargetPressure;
            set 
            {
                if (!double.TryParse(value.ToString(), out double newvalue))
                    return;
                if(!Set(ref _TargetPressure, newvalue))
                    return;
                if (_TargetPressure == 0)
                    return;
                OnPropertyChanged(nameof(Result));
            }
        }

        /// <summary>
        /// Результат расчета
        /// </summary>
        public CalculationResultCargo Result
        {
            get
            {
                if(IpsSelected is null)
                    return null;

                CalculationResultCargo result_Cargoes = _CalculatorCargo.Calc(new EnvironmentalСonditions
                {
                    Temperature = this.Temperature,
                    Humidity = this.Humidity,
                    Baro = this.Baro
                }, _TargetPressure, IpsSelected);

                KettlebellCargoes = (double)Math.Round(result_Cargoes.CargoesWhereKettlebell.Sum(x => x.Weight) * 1000, 3);
                AllCargoesWeight = (double)(result_Cargoes.Cargoes.Sum(x => x.Weight) + IpsSelected.Weight);
                CargoesWeightWhereNoKettlebell = (double)(result_Cargoes.CargoesWhereNOKettlebell.Sum(x => x.Weight) + IpsSelected.Weight);
                

                return result_Cargoes;
            }
        }
        /// <summary>
        /// Масса подобранных грузов
        /// </summary>
        private double _AllCargoesWeight;
        /// <summary>
        /// Масса подобранных грузов
        /// </summary>
        public double AllCargoesWeight
        {
            get => _AllCargoesWeight;
            set 
            {
                if (!Set(ref _AllCargoesWeight, value))
                    return;
                OnPropertyChanged(nameof(ActualPressureAllCargoes));
            } 
        }

        /// <summary>
        /// Масса без добавночных грузов
        /// </summary>
        private double _CargoesWeightWhereNoKettlebell;
        /// <summary>
        /// Масса без добавочных грузов
        /// </summary>
        public double CargoesWeightWhereNoKettlebell
        {
            get => _CargoesWeightWhereNoKettlebell;
            set
            {
                if(!Set(ref _CargoesWeightWhereNoKettlebell, value))
                    return;
                OnPropertyChanged(nameof(ActualPressureWhereNoKettlebell));
            }
        }

        /// <summary>
        /// Масса гирь
        /// </summary>
        private double _KettlebellCargoes;
        /// <summary>
        /// Масса гирь
        /// </summary>
        public double KettlebellCargoes
        {
            get => _KettlebellCargoes;
            set => Set(ref _KettlebellCargoes, value);
        }

        /// <summary>
        /// Актуальное давление без гирь
        /// </summary>
        public double ActualPressureWhereNoKettlebell
        {
            get
            {
                var env = new EnvironmentalСonditions
                {
                    Temperature = this.Temperature,
                    Humidity = this.Humidity,
                    Baro = this.Baro
                };

                var actual_Press = _CargoesWeightWhereNoKettlebell == 0 ? 0 :
                    _CalculatorPressure.Calc(env, _CargoesWeightWhereNoKettlebell, SelectedIps: IpsSelected).ActualPressure;
                return Math.Round(actual_Press, 6);
            }
        }
        /// <summary>
        /// Актуальное давление со всеми гирями
        /// </summary>
        public double ActualPressureAllCargoes
        {
            get
            {
                var env = new EnvironmentalСonditions
                {
                    Temperature = this.Temperature,
                    Humidity = this.Humidity,
                    Baro = this.Baro
                };
                var actualPress = _AllCargoesWeight == 0 || KettlebellCargoes == 0 ? 0 :
                    _CalculatorPressure.Calc(env, _AllCargoesWeight, SelectedIps: IpsSelected).ActualPressure;

                return Math.Round(actualPress, 6);
            }
        }

        #region Команда запрашивающая коллекцию ИПС и Грузов

        private ICommand _CommandLoadIPS;
        public ICommand LoadIPSCommand => _CommandLoadIPS ?? 
            new LambdaCommandAsync(OnLoadIPSCommandExecuted, CaLoadIPSCommandExecute);
        private bool CaLoadIPSCommandExecute(object p) => true;

        private async Task OnLoadIPSCommandExecuted(object p)
        {
            CollectionIPS = new ObservableCollection<IPS.DAL.IPS>(await _RepositoryIps.Items.ToArrayAsync());
        }

        #endregion
        
    }
}