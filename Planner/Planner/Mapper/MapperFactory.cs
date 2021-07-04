using System;
using AutoMapper;
using Planner.OutDTO;

namespace Planner.Mapper
{
    public static class MapperFactory
    {
        private static readonly Lazy<AutoMapper.Mapper> _mapper =
            new Lazy<AutoMapper.Mapper>(() =>
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<Domain.Entities.User, UserOutDTO>();
                    cfg.CreateMap<Domain.Entities.Deed, DeedOutDTO>();
                    cfg.CreateMap<Domain.Entities.Execution, DeedExecutionOutDTO>();
                });

                return new AutoMapper.Mapper(config);
            });

        public static AutoMapper.Mapper Mapper => _mapper.Value;
    }
}
