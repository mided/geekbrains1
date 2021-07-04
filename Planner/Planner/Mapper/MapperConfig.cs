using System;
using AutoMapper;
using Planner.OutDtos;

namespace Planner.Mapper
{
    public static class MapperConfig
    {
        private static readonly Lazy<AutoMapper.Mapper> _mapper =
            new Lazy<AutoMapper.Mapper>(() =>
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<Domain.Entities.User, UserOutDTO>();
                });

                return new AutoMapper.Mapper(config);
            });

        public static AutoMapper.Mapper Mapper => _mapper.Value;
    }
}
