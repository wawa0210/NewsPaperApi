using AutoMapper;
using PaperNewsService.Entity;
using PaperNewsService.Model;

namespace WebApi.FrameWork
{
    /// <summary>
    /// automapper 映射
    /// </summary>
    public class MapperInit
    {
        public static void InitMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TableNews, EntityNews>()
                   .ForMember(x => x.NewsId, y => y.MapFrom(z => z.NewsId))
                   .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                   .ForMember(x => x.ShortContent, y => y.MapFrom(z => z.ShortContent))
                   .ForMember(x => x.Content, y => y.MapFrom(z => z.NewsContent))
                   .ForMember(x => x.HrefUrl, y => y.MapFrom(z => z.HrefUrl))
                   .ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
                   .ForMember(x => x.UpdateTime, y => y.MapFrom(z => z.UpdateTime))
                   .ForMember(x => x.IsEnable, y => y.MapFrom(z => z.IsEnable))
                   .ForMember(x => x.Media, y => y.MapFrom(z => z.Media))
                   .ForMember(x => x.NewsType, y => y.MapFrom(z => z.NewsType))
                   .ForAllOtherMembers(x => x.Ignore());

                cfg.CreateMap<TableNews, EntityListNews>()
                 .ForMember(x => x.NewsId, y => y.MapFrom(z => z.NewsId))
                 .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                 .ForMember(x => x.ShortContent, y => y.MapFrom(z => z.ShortContent))
                 .ForMember(x => x.HrefUrl, y => y.MapFrom(z => z.HrefUrl))
                 .ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
                 .ForMember(x => x.IsEnable, y => y.MapFrom(z => z.IsEnable))
                 .ForMember(x => x.Media, y => y.MapFrom(z => z.Media))
                 .ForMember(x => x.NewsType, y => y.MapFrom(z => z.NewsType))
                 .ForAllOtherMembers(x => x.Ignore());

                cfg.CreateMap<TableVersions, EntityVersion>()
                  .ForMember(x => x.VersionId, y => y.MapFrom(z => z.VersionId))
                  .ForMember(x => x.VersionName, y => y.MapFrom(z => z.VersionName))
                  .ForMember(x => x.VersionStatus, y => y.MapFrom(z => z.VersionStatus))
                  .ForMember(x => x.Remark, y => y.MapFrom(z => z.Remark))
                  .ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
                  .ForAllOtherMembers(x => x.Ignore());
            }
            );
        }
    }
}
