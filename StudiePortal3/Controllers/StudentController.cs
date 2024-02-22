using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiePortal3.Data;
using StudiePortal3.Models;
using StudiePortal3.Models.Entities;

namespace StudiePortal3.Controllers
{
    public class StudentController : Controller
    {
        private readonly BloggieDbContext dbContext;

        public StudentController(BloggieDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public IActionResult Add()  //vil retunere en visning 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddStudieViewModel viewModel)  //dataene blive sendt som en HTTP POST-anmodning til og
                                                                            //- og gemmer derefter denne nye studerende i databasen.
        {

            var Student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };

            await dbContext.AddAsync(Student);
            dbContext.SaveChanges();
            return View();  
        }

     

        [HttpGet]
        public async Task<IActionResult> StudentListe()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);

        }


        public async Task<IActionResult> Edit(Guid id)
        {
          var student =  await dbContext.Students.FindAsync(id);


            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {


        }
    }
}
