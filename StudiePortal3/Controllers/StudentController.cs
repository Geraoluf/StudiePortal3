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

            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("StudentListe", "student");
        }




        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            dbContext.Students.Remove(viewModel);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("StudentListe", "Student");
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            


            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);


            if (student is not null)
            {
                dbContext.Students.Remove(student); // Fjern det hentede student-objekt, ikke viewModel
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("StudentListe", "Student"); // Omdiriger til siden med student-listen
        }
        */
    }
}
