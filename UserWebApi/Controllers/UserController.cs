using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserWebApi.Services;
using UserWebApi.ViewModels;

namespace UserWebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Route("")]
        public async Task<ObjectResult> Get()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        /*[HttpGet("{id}")]
        //[Route("")]
        public async Task<ObjectResult> GetById(string id)
        {
            var users = await _userService.GetByIdAsync(id);
            return Ok(users);
        }*/

        [HttpPost]
        [Route("create")]
        public async Task<ObjectResult> Create(CreateUserViewModel model)
        {
            try
            {
                await _userService.CreateAsync(model);
                return Ok("User created");
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("{id}")]
        //[Route("")]
        public async Task<ObjectResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok("User deleted");
            }
            catch
            {
                return BadRequest("Error");
            }
        }

        /*[HttpPut]
        [Route("edit")]
        public async Task<ObjectResult> Edit(EditUserViewModel model)
        {
            try
            {
                await _userService.EditUserAsync(model);
                return Ok("User updated");
            }
            catch
            {
                return BadRequest("Error");
            }
        }*/

        [HttpPost]
        [Route("filter")]
        public async Task<ObjectResult> GetFilteredUsers(FilterModel model)
        {
            var users = await _userService.FilteredUsersAsync(model);
            return Ok(users);
        }
    }
}
