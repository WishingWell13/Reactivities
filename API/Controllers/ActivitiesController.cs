using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    //Base API controller already gives this class the APICotntroller and route
    //attributes
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _context;

        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities(){
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] //api/activities/{whatever the id is, without curly brace}
        public async Task<ActionResult<Activity>> GetActivity(Guid id){
            return await _context.Activities.FindAsync(id);
        }
    }
}