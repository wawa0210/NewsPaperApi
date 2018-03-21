using AutoMapper;

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
