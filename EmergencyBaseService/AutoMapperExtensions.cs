using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyBaseService
{
    public static class AutoMapperExtensions
    {
        public static T MapToObject<T, TU>(this TU model, T target)
        {
            return Mapper.Map<TU, T>(model);
        }
    }
}
