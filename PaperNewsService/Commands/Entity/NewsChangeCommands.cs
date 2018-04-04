using MediatR;
using PaperNewsService.Entity;

namespace PaperNewsService.Commands.Entity
{
    public class NewsChangeCommands : IRequest<bool>
    {
        public EntityNews NewsInfo { get; private set; }

        public NewsChangeCommands(EntityNews newsInfo)
        {
            NewsInfo = newsInfo;
        }
    }
}
