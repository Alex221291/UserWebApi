using Microsoft.EntityFrameworkCore;
using UserWebApi.Data;
using UserWebApi.Models;
using UserWebApi.ViewModels;

namespace UserWebApi.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel?>> GetUsersAsync();
        Task DeleteAsync(string id);
        Task CreateAsync(CreateUserViewModel model);
        Task<UserViewModel?> GetByIdAsync(string id);
        Task EditUserAsync(EditUserViewModel model);
        Task<List<UserViewModel?>> FilteredUsersAsync(FilterModel model);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserViewModel?>> GetUsersAsync()
        {
            return await _db.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Country = u.Country,
                    City = u.City,
                })
                .ToListAsync();
        }

        public async Task<UserViewModel?> GetByIdAsync(string id)
        {
            return await _db.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Country = u.Country,
                    City = u.City,
                })
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateAsync(CreateUserViewModel model)
        {
            await _db.Users.AddAsync(new User
            {
                Name = model.Name,
                Country = model.Country,
                City = model.City,
            });
            await _db.SaveChangesAsync();
        }

        public async Task EditUserAsync(EditUserViewModel model)
        {
            var editUser = await _db.Users.FirstOrDefaultAsync(u => u.Id.ToString() == model.Id);
            if (editUser != null)
            {
                editUser.Name = model.Name;
                editUser.Country = model.Country;
                editUser.City = model.City;

                _db.Users.Update(editUser);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<UserViewModel?>> FilteredUsersAsync(FilterModel model)
        {
            return (await _db.Users
                .Where(u => (model.City == null || model.City.ToLower().Contains(u.City.ToLower())) && 
                            (model.Name == null || u.Name.ToLower().Contains(model.Name.ToLower())) && 
                            (model.Country == null|| u.Country.ToLower().Contains(model.Country.ToLower())))
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Country = u.Country,
                    City = u.City,
                })
                .ToListAsync());
        }

        public async Task DeleteAsync(string id)
        {
            var deleteUser = await _db.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
            _db.Users.Remove(deleteUser);
            await _db.SaveChangesAsync();
        }

        
    }
}
