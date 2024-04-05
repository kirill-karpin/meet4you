using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Filter
{
    public enum FamilyStatus
    {
        Свободен = 0,

        [Display(Description = "В браке")]
        ВБраке = 1,

        [Display(Description = "В разводе")]
        Разведён = 3
    }
}
