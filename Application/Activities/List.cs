using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        //Query forms request and passes to handler
        public class Query : IRequest<List<Activity>>{

        }

        //What you return is the second paramater
        //Handler returns data we are looking for based on Irequest Interface
        //First var in <var1, var2> is input, var2 is output
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
                
            }
            
            //A task is a asynchronous operation
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities.ToListAsync();
            }

        }
    }
}