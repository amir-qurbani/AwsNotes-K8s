using Microsoft.AspNetCore.Mvc;
using AwsNotes.Models;
using AwsNotes.Data;

namespace AwsNotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly MongoDbService _mongoService;

        public NotesController(MongoDbService mongoService) // <-- ändrad till public
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NoteCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateViewModel vm)
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

            await _mongoService.AddNoteAsync(note);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await _mongoService.GetNotesAsync();
            return View(items);
        }
    }
}
