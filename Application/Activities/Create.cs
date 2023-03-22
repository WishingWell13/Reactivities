using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {

        //Commands do not return data, queries do
        public class Command : IRequest
        {
            //We pass an activity as a parameter when we create Command
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            //Data context is used to persist our changes/data
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
            _context = context;
                
            }

            //request should hold an activity object (is a parameter in class)
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //Make entity framework say we have made a change in context in memory
                _context.Activities.Add(request.Activity);

                //Actually save the change
                await _context.SaveChangesAsync();

                //Equivalent to nothing, Task<Unit> is built into a library
                return Unit.Value;
            }
        }
    }
}