using IPS.DAL;
using IPS_CALC.EnumsAndDictinary;
using IPS_CALC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPS_CALC.Services
{
    internal class DictinaryEnumConvertor : IDictinaryEnumConvertor
    {
        public Dictionary<CargoType, string> CargoEnumDictinary { get; } = new Dictionary<CargoType, string>()
        {
            { CargoType.Cargo, "Груз" },
            { CargoType.PlateIsTransitional, "Тарелка переходная" },
            { CargoType.Bell, "Колокол" },
            { CargoType.Сup, "Чаша" },
            { CargoType.Kettlebell, "Гиря" }
        };
        public string CargoTypeToString(int CargoIntType) =>
            CargoIntType >= 0  && CargoEnumDictinary.Count - 1 >= CargoIntType ? 
            CargoEnumDictinary.ElementAt(CargoIntType).Value : null;
        public CargoType CargoTypeToEnum(int CargoIntType) => (CargoType)CargoIntType;
    }
}
