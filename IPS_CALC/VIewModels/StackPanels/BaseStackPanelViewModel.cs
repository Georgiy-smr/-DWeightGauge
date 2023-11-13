using IPS_CALC.Inftastructure.Commands;
using IPS_CALC.VIewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IPS_CALC.VIewModels.StackPanels
{
    internal class BaseStackPanelViewModel<T> : ViewModel
    {
        #region Private Members

        /// <summary>
        /// Наименование
        /// <summary>
        private T _element;

        /// <summary>
        /// Нажатие на кнопку
        /// </summary>
        private bool _isClicked = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// Наименование
        /// <summary>
        public T Element
        {
            get => _element;
            set => Set(ref _element, value);
        }

        /// <summary>
        /// Нажатие на кнопку
        /// </summary>
        public bool IsClicked
        {
            get
            {
                if (_isClicked)
                    _isClicked = false;
                return _isClicked;
            }
            set
            {
                _isClicked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        #endregion

        private ICommand _ElementChangedCommand;
        public ICommand ElementChangedCommand => _ElementChangedCommand ?? new LambdaCommand(() => IsClicked = true);

    }
}
