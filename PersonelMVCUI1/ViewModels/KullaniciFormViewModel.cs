using PersonelMVCUI1.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVCUI1.ViewModels
{
    public class KullaniciFormViewModel
    {

        public IEnumerable<Personel> Personeller { get; set; }
        public Kullanici Kullanici { get; set; }
    }
}