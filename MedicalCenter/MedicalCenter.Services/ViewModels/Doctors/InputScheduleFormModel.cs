
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class InputScheduleFormModel
    {
            [Display(Name = "Pick Up Day And Hour")]
            public DateTime Date { get; set; }
        
    }
}
