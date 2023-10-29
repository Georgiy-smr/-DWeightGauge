using IPS_CALC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace IPS_CALC.Services
{
    internal class EventService : IEventService
    {
        public event Action<string, object> SomeEvent;

        public void RaiseEvent(string PropName, object ViewModel)
        {
            SomeEvent?.Invoke(PropName, ViewModel);
        }
    }
}
