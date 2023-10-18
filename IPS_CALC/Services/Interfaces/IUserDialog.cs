using System;
using System.Collections.Generic;
using System.Text;

namespace IPS_CALC.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool Edit(object item);
        bool Confirm(string Message, string Caption, bool Exlamination = false);
    }
}
