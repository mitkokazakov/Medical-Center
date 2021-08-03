
using System.Collections.Generic;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class ListAllSchedulesViewModel
    {
        public string NameOfDay { get; set; }

        public List<ListAllHoursViewModel> Hours { get; set; }
    }
}
