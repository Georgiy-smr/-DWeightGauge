using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Inftastructure.Mediatr
{
    internal class PropertyViewModelChanged : INotification
    {
        public string? PropertyName { get; set; }
        public object? ViewModel { get; set; }
    }
}
