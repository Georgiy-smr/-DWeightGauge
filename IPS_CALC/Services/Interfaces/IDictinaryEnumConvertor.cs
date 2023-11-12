using IPS_CALC.EnumsAndDictinary;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Services.Interfaces
{
    internal interface IDictinaryEnumConvertor
    {
        Dictionary<CargoType, string> CargoEnumDictinary { get; }

        CargoType CargoTypeToEnum(int CargoIntType);
        string CargoTypeToString(int CargoIntType);
    }
}
