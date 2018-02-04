﻿using AutoMapper;
using PaperNewsService.Entity;
using PaperNewsService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                   .ForAllOtherMembers(x => x.Ignore());

                cfg.CreateMap<TableNews, EntityListNews>()
                 .ForMember(x => x.NewsId, y => y.MapFrom(z => z.NewsId))
                 .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                 .ForMember(x => x.ShortContent, y => y.MapFrom(z => z.ShortContent))
                 .ForMember(x => x.HrefUrl, y => y.MapFrom(z => z.HrefUrl))
                 .ForMember(x => x.CreateTime, y => y.MapFrom(z => z.CreateTime))
                 .ForMember(x => x.IsEnable, y => y.MapFrom(z => z.IsEnable))
                 .ForAllOtherMembers(x => x.Ignore());
            }
            );
        }
    }
}
