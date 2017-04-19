namespace Sightseer.Models.ViewModels.Admin
{
    using System.Collections.Generic;
    using Attractions;

    public class AdminPageVm
    {
        public IEnumerable<AttractionVm> Attractions { get; set; }

        public IEnumerable<AdminPageUserVm> Users { get; set; }
    }
}
