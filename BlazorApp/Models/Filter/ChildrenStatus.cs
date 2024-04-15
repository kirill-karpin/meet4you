using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Filter
{
    public enum ChildrenStatus
    {
        [Display(Description = "Без детей")]
        Нет = 0,

        [Display(Description = "Есть дети")]
        Есть = 1,
    }
}
