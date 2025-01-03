using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalStorageSystem.ViewModels
{
    public class SatislarFormViewModels
    {
        public IEnumerable<Ilac> Ilaclar { get; set; }
        public IEnumerable<Musteri> Musteriler { get; set; }
        public IEnumerable<Satis> Satislar { get; set; }
    }
}