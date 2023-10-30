using Blazorise;
using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DeanerySystem.Services
{
    public class PersonService : IPersonService
    {
        private readonly DeaneryContext _context;

        public PersonService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<MethodResult> SavePersonAsync(Person person)
        {
            try
            {
                if (person.Id > 0)
                {
                    _context.Update(person);
                }
                else
                {
                    await _context.AddAsync(person);
                }
                await _context.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            //return await _context.People.AsNoTracking().ToListAsync();
            var people = _context.People.AsNoTracking().Include("Group");
            return await people.ToListAsync();
        }

        public async Task<MethodResult> DeletePersonAsync(int personId)
        {
            try
            {
                var personToDelete = await _context.People.FindAsync(personId);
                _context.People.Remove(personToDelete);
                await _context.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch(Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public void CheckEntries(Person person)
        {
            var personEntry = _context.Entry(person);
            if (personEntry.State == EntityState.Modified)
            {
                personEntry.CurrentValues.SetValues(personEntry.OriginalValues);
                personEntry.State = EntityState.Unchanged;
            }
        }


    }
}
