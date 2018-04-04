using MediatR;
using PaperNewsService.Commands.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaperNewsService.Commands.EventBus
{
    public class NewsChangeHandler : IRequestHandler<NewsChangeCommands, bool>
    {
        public async Task<bool> Handle(NewsChangeCommands request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
