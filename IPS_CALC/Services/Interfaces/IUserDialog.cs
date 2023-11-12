using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool Edit(object item, IDictinaryEnumConvertor DictinaryEnum = null);
        bool Confirm(string Message, string Caption, bool Exlamination = false);

        bool RedactToAdded(object item, IEnumerable<object> Additionally);
        bool RedactToRemoved(object item, IEnumerable<object> Removeitionally);
    }
}
