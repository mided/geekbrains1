using System;
using AutoMapper;

namespace EfDataAccess
{
    public static class MapperConfig
    {
        private static readonly Lazy<AutoMapper.Mapper> _mapper =
            new Lazy<AutoMapper.Mapper>(() =>
            {
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<EfDataAccess.User, Domain.Entities.User>();
                    cfg.CreateMap<Domain.Entities.User, EfDataAccess.User>();

                    cfg.CreateMap<EfDataAccess.Deed, Domain.Entities.Deed>();
                    cfg.CreateMap<Domain.Entities.Deed, EfDataAccess.Deed>();

                    cfg.CreateMap<EfDataAccess.Execution, Domain.Entities.Execution>();
                    cfg.CreateMap<Domain.Entities.Execution, EfDataAccess.Execution>();
                });

                return new AutoMapper.Mapper(config);
            });

        public static AutoMapper.Mapper Mapper => _mapper.Value;
    }
}
