using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalStorageSystem.ViewModels
{
    public class SearchViewModels
    {
        public List<SearchResult> SearchResults { get; set; }
        public IEnumerable<Satis> SatislarHepsi { get; set; }
        public IEnumerable<Satis> SatislarElenmis { get; set; }

    }
}