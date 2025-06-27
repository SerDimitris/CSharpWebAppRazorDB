using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpWebAppRazorDB.DTO;
using CSharpWebAppRazorDB.Models;
using CSharpWebAppRazorDB.Services;

namespace CSharpWebAppRazorDB.Pages.Students
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public StudentInsertDTO StudentInsertDTO { get; set; } = new();

        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        public CreateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void OnGet()
        {
            //return Page();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.InsertStudent(StudentInsertDTO);
                Response.Redirect("/Students/getall");
            }
            catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
                return;
            }
        }
    }
}
