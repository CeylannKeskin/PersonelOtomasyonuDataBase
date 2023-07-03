using System;
using System.Collections.Generic;

namespace Odev1_PersOto.Models
{
    public partial class PersonelBilgi
    {
        public int PersonelId { get; set; }
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string Tckn { get; set; } = null!;
        public DateTime? DogumTarihi { get; set; }
        public string? Email { get; set; }
        public string? TelefonNumarasi { get; set; }
        public string? Adres { get; set; }
        public DateTime? IseGirisTarihi { get; set; }
        public string? Unvan { get; set; }
        public byte[]? PersonelResmi { get; set; }
    }
}
