using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmergencyEntity.PageQuery;
using PaperNewsService.Entity;

namespace PaperNewsService.Application
{
    public class VersionService : IVersionService
    {
        public Task<EntityVersion> AddVersionAsync(EntityVersion entityVersion)
        {
            throw new NotImplementedException();
        }

        public Task<PageBase<EntityVersion>> GetPageVersionAsync(EntityVersionQuery entityVersionQuery)
        {
            throw new NotImplementedException();
        }

        public Task<EntityVersion> GetVersionAsync(string versionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVersionAsync(EntityVersion entityVersion)
        {
            throw new NotImplementedException();
        }
    }
}
