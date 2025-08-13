using Pomni.Data;
using Pomni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomni.Repositories
{
    public class NoteRepository : INoteRepository
    {
        public IEnumerable<Note> GetAll()
        {
            using (var db = new PomniDbContext())
                return db.Notes.AsNoTracking().ToList();
        }

        public Note GetById(int id)
        {
            using (var db = new PomniDbContext())
                return db.Notes.Find(id);
        }

        public void Add(Note note)
        {
            using (var db = new PomniDbContext())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }    
        }

        public void Update(Note note)
        {
            using (var db = new PomniDbContext())
            {
                db.Entry(note).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new PomniDbContext())
            {
                var note = db.Notes.Find(id);
                if (note != null)
                {
                    db.Notes.Remove(note);
                    db.SaveChanges();
                }
            }
        }
    }
}
