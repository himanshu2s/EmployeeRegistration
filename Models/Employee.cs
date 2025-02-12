using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime DOB { get; set; }
        public DateTime StartingDate { get; set; }
        public string Mobile { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string MaritalStatus { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Image { get; set; } = default!; // Will caputre Image URL here.
    }

    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }

    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(); // Done
        Task<Employee> GetByIdAsync(int id); // Done
        Task<Employee> GetByNameAsync(string name);
        Task<Employee> GetByEmailAsync(string email);
        Task<Employee> GetByLastNameAsync(string lastName);
        Task<Employee> GetByFirstNameAsync(string firstName);
        Task<Employee> AddAsync(Employee entity); // Done
        Task<Employee> UpdateAsync(Employee entity); // Done
        Task<bool> DeleteAsync(int id); // Done
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employee.ToListAsync();
        public async Task<Employee> GetByIdAsync(int employeeId) => await _context.Employee.FindAsync(employeeId);

        public async Task<Employee> AddAsync(Employee entity)
        {
            await _context.Employee.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);

                if (employee != null)
                {
                    _context.Employee.Remove(employee);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<Employee> GetByEmailAsync(string email)
        {
            // We can implement this section as per need. Not implementing as of now.
            throw new NotImplementedException();
        }

        public Task<Employee> GetByFirstNameAsync(string firstName)
        {
            // We can implement this section as per need. Not implementing as of now.
            throw new NotImplementedException();
        }

        public Task<Employee> GetByLastNameAsync(string lastName)
        {
            // We can implement this section as per need. Not implementing as of now.
            throw new NotImplementedException();
        }

        public Task<Employee> GetByNameAsync(string name)
        {
            // We can implement this section as per need. Not implementing as of now.
            throw new NotImplementedException();
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            try
            {
                _context.Employee.Update(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
