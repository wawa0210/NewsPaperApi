using AutoMapper;
using PaperNewsService.Entity;
using PaperNewsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.FrameWork.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TableNews, EntityNews>();
            //.ForMember(x => x.NewsId, y => y.MapFrom(z => z.NewsId))
            //.ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            //.ForMember(x => x.ShortContent, y => y.MapFrom(z => z.ShortContent))
            //.ForMember(x => x.Content, y => y.MapFrom(z => z.NewsContent))
            //.ForMember(x => x.HrefUrl, y => y.MapFrom(z => z.HrefUrl))
            //.ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
            //.ForMember(x => x.UpdateTime, y => y.MapFrom(z => z.UpdateTime))
            //.ForMember(x => x.IsEnable, y => y.MapFrom(z => z.IsEnable))
            //.ForMember(x => x.Media, y => y.MapFrom(z => z.Media))
            //.ForMember(x => x.NewsType, y => y.MapFrom(z => z.NewsType))
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<TableNews, EntityListNews>();
            //.ForMember(x => x.NewsId, y => y.MapFrom(z => z.NewsId))
            //.ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
            //.ForMember(x => x.ShortContent, y => y.MapFrom(z => z.ShortContent))
            //.ForMember(x => x.HrefUrl, y => y.MapFrom(z => z.HrefUrl))
            //.ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
            //.ForMember(x => x.IsEnable, y => y.MapFrom(z => z.IsEnable))
            //.ForMember(x => x.Media, y => y.MapFrom(z => z.Media))
            //.ForMember(x => x.NewsType, y => y.MapFrom(z => z.NewsType))
            //.ForAllOtherMembers(x => x.Ignore());
        }
    }
}
