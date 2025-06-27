using CSharpWebAppRazorDB.DTO;
using CSharpWebAppRazorDB.Models;
using CSharpWebAppRazorDB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSharpWebAppRazorDB.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentReadOnlyDTO> StudentsReadOnlyDTO { get; set; } = [];
        public Error ErrorObj { get; set; } = new();

        private readonly IStudentService _studentService;

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult OnGet()
        {
            try
            {
                ErrorObj = new();
                StudentsReadOnlyDTO = _studentService.GetAllStudents();
            }
            catch (Exception ex)
            {
                ErrorObj = new Error("", ex.Message, "");
            }
            return Page();
        }
    }
}
