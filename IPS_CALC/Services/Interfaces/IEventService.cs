using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Services.Interfaces
{
    internal interface IEventService
    {
        /// <summary>
        /// событе
        /// </summary>
        event Action<string, object> SomeEvent;
        /// <summary>
        /// вызов события
        /// </summary>
        /// <param name="data"></param>
        void RaiseEvent(string PropName, object ViewModel);

    }
}
