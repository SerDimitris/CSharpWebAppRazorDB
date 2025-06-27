using CSharpWebAppRazorDB.DTO;
using CSharpWebAppRazorDB.Models;
using CSharpWebAppRazorDB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSharpWebAppRazorDB.Pages.Students
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public StudentUpdateDTO StudentUpdateDTO { get; set; } = new();

        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        public UpdateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.GetStudent(id);
                StudentUpdateDTO = new()
                {
                    Id = studentReadOnlyDTO.Id,
                    Firstname = studentReadOnlyDTO.Firstname,
                    Lastname = studentReadOnlyDTO.Lastname,
                };
            } catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
            }
            return Page();
        }

        public void OnPost(int id)
        {
            try
            {
                StudentUpdateDTO.Id = id;
                _studentService.UpdateStudent(StudentUpdateDTO);
                Response.Redirect("/Students/getall");
            } catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
            }

        }
    }
}
