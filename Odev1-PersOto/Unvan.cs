using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1_PersOto
{
    public enum Unvan
    {
        //To Do Comboboxta isimleri gösterme
        [Display(Name="Müdür")]
        Mudur,

        [Display(Name = "Yönetici")]
        Yonetici,

        CEO,

        [Display(Name = "Temizlik Personeli")]
        TemizlikPersoneli,

        [Display(Name = "Danışman")]
        Danisman,

        [Display(Name = "Müşteri Yöneticisi")]
        MusteriYoneticisi,

        [Display(Name = "Eğitmen")]
        Egitmen,

        Belirtilmedi
    }
}
