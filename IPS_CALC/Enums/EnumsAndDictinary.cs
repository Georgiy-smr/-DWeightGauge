using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace IPS_CALC.EnumsAndDictinary
{
  public enum CargoType
    {
        [Display(Name = "Груз")]
        Cargo,
        [Display(Name = "Тарелка переходная")]
        PlateIsTransitional,
        [Display(Name = "Колокол")]
        Bell,
        [Display(Name = "Чаша")]
        Сup,
        [Display(Name = "Гиря")]
        Kettlebell
    }

}
