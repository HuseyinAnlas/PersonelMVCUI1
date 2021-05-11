using PersonelMVCUI1.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVCUI1.ViewModels
{
    public class PersonelFormViewModel
    {
        public IEnumerable<Departman> Departmanlar { get; set; }
        public Personel Personel { get; set; }
    }
}