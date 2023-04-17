using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace NMEA.Wpf.Services
{
    public interface IAutoMapperService
    {
        IMapper GetMapper();
    }

    public class AutoMapperService : IAutoMapperService
    {
        private readonly MapperConfiguration _configuration;

        public AutoMapperService()
        {
            _configuration = new MapperConfiguration(configure => {
                // configure.CreateMap<User, UserDto>().ReverseMap();
            });
        }

        public IMapper GetMapper()
        {
            return _configuration.CreateMapper();
        }
    }
}
