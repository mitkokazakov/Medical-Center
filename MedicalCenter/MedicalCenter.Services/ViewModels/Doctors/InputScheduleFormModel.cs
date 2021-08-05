
using MedicalCenter.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalCenter.Services.ViewModels.Doctors
{
    public class InputScheduleFormModel
    {
        [Required]
        [Display(Name = "Pick Up Day And Hour")]
        [DateAttribute(ErrorMessage = "Invalid date")]
        public DateTime Date { get; set; }

    }
}
