using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Interfaces;
using Planner.Mapper;
using Planner.OutDTO;

namespace Planner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IPlannerLogicService _logicService;

        private readonly AutoMapper.Mapper _mapper;

        public UserController(IPlannerLogicService logicService)
        {
            _logicService = logicService;
            _mapper = MapperFactory.Mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserOutDTO>> Get(int? id, string name)
        {
            if (id.HasValue || !string.IsNullOrEmpty(name))
            {
                var user = _logicService.GetUser(id, name);
                return new List<UserOutDTO> {_mapper.Map<UserOutDTO>(user)};
            }

            var users = _logicService.GetUsers();

            return users.Select(u => _mapper.Map<UserOutDTO>(u)).ToList();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<UserOutDTO> Register([FromBody] RegisterUserDTO dto)
        {
            var user = _logicService.RegisterUser(dto);
            return _mapper.Map<UserOutDTO>(user);
        }
    }
}
