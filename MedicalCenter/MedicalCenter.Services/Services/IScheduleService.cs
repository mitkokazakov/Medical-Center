
using MedicalCenter.Services.ViewModels.Schedules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IScheduleService
    {
        Task AddFreeHour(string doctorId, InputScheduleFormModel model);

        ICollection<ListAllSchedulesViewModel> ListAllFreeHours(string doctorId);
    }
}
