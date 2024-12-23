using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Notes.API.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDBContext _context;

        public NotesController(NotesDBContext context)
        {
                _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            //Get the notes from the database
            var notes = await _context.Notes.ToListAsync();

            return Ok(notes);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
        {
            //Get the note from the database
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            //Add the note to the database
            note.Id = Guid.NewGuid();
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note note)
        {
            //Get the note from the database
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (existingNote == null)
            {
                return NotFound();
            }

            //Update the note
            existingNote.Title = note.Title;
            existingNote.Description = note.Description;
            existingNote.IsVisible = note.IsVisible;

            await _context.SaveChangesAsync();

            return Ok(existingNote);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            //Get the note from the database
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            //Delete the note
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
