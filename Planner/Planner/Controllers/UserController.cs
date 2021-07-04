using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Planner.Mapper;
using Planner.OutDtos;

namespace Planner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IPlannerLogicService _logicService;

        private readonly AutoMapper.Mapper _mapper;

        public UserController(ILogger<UserController> logger, IPlannerLogicService logicService)
        {
            _logger = logger;
            _logicService = logicService;
            _mapper = MapperConfig.Mapper;
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
    }
}
