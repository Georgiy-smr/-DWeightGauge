﻿using System;
using System.Collections.Generic;
using System.Text;
using IPS_CALC.Inftastructure.Commands.Base;

namespace IPS_CALC.Inftastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        protected override void Execute(object parameter)
            => _execute(parameter);
        private readonly Func<object, bool> _canExecute;
        protected override bool CanExecute(object parameter) =>
            _canExecute?.Invoke(parameter) ?? true;

        public LambdaCommand(Action<object> Execute,
            Func<object, bool> CanExecute = null)
        {
            _execute = Execute ?? throw new ArgumentException(
                nameof(Execute));
            _canExecute = CanExecute;
        }

        public LambdaCommand(Action Execute, Func<bool> CanExecute = null)
            : this(Execute: p => Execute(), CanExecute: CanExecute is null ? (Func<object, bool>)null : p => CanExecute()) { }
    }
}
