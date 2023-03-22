using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Activities;

namespace API.Controllers
{
    //Base API controller already gives this class the APICotntroller and route
    //attributes
    public class ActivitiesController : BaseApiController
    {

        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get a list of all the activities we have
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities(){
            //Ver without mediator, goal is to get this controller as thin as possible
            //return await _context.Activities.ToListAsync(); 
            
            //Use send to send query to mediator
            //This returns all of the Activities we have as a list
            return await _mediator.Send(new List.Query());     
        }

        //Get single activity
        [HttpGet("{id}")] //api/activities/{whatever the id is, without curly brace}
        public async Task<ActionResult<Activity>> GetActivity(Guid id){
            //Curly braces is "object initializer syntax"
            //https://www.tutorialsteacher.com/csharp/csharp-object-initializer
            return await Mediator.Send(new Details.Query{Id = id});
            
            //return await _context.Activities.FindAsync(id); //Old version without mediator
        }

        //IActionResult gives us to HTTP response types (OK, Bad Request, Etc.)
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity){
            return Ok(await Mediator.Send(new Create.Command {Activity = activity}));
        }

        //GUID: Global Unique Identifier
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity){
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity=activity}));
        }


        //Curly brackets: root parameter
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id){
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }

    }
}