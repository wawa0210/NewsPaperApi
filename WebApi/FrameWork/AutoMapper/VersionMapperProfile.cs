using AutoMapper;
using PaperNewsService.Entity;
using PaperNewsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.FrameWork.AutoMapper
{
    public class VersionMapperProfile : Profile
    {
        public VersionMapperProfile()
        {
            CreateMap<TableVersions, EntityVersion>()
              .ForMember(x => x.VersionId, y => y.MapFrom(z => z.VersionId))
              .ForMember(x => x.VersionName, y => y.MapFrom(z => z.VersionName))
              .ForMember(x => x.VersionStatus, y => y.MapFrom(z => z.VersionStatus))
              .ForMember(x => x.Remark, y => y.MapFrom(z => z.Remark))
              .ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
              .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
