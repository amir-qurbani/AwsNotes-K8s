using Microsoft.AspNetCore.Mvc;
using AwsNotes.Models;
using AwsNotes.Data;

namespace AwsNotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly NotesDbContext _context;

        public NotesController(NotesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NoteCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(NoteCreateViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Title))
            {
                ModelState.AddModelError(nameof(vm.Title), "Title är obligatorisk.");
                return View(vm);
            }

            var note = new NoteItem
            {
                Title = vm.Title,
                Note = vm.Note
            };

            _context.Notes.Add(note);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var items = _context.Notes.ToList();
            return View(items);
        }
    }
}
