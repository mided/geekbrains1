using System;
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
    public class DeedController : ControllerBase
    {
        private readonly IPlannerLogicService _logicService;

        private readonly AutoMapper.Mapper _mapper;

        public DeedController(IPlannerLogicService logicService)
        {
            _logicService = logicService;
            _mapper = MapperFactory.Mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DeedOutDTO>> Get(int userId, DateTime? from, DateTime? to)
        {
            var deeds = _logicService.GetUserDeeds(userId, from, to);
            
            return deeds.Select(u => _mapper.Map<DeedOutDTO>(u)).ToList();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<DeedOutDTO> Create([FromBody] CreateDeedDTO dto)
        {
            var deed = _logicService.CreateDeed(dto);

            return _mapper.Map<DeedOutDTO>(deed);
        }

        [HttpPost]
        [Route("complete")]
        public ActionResult<DeedOutDTO> Complete(int userId, int deedId)
        {
            var deed = _logicService.CompleteDeed(deedId, userId);

            return _mapper.Map<DeedOutDTO>(deed);
        }

        [HttpPost]
        [Route("uncomplete")]
        public ActionResult<DeedOutDTO> Uncomplete(int userId, int deedId)
        {
            var deed = _logicService.CancelDeedCompletion(deedId, userId);

            return _mapper.Map<DeedOutDTO>(deed);
        }

        [HttpPost]
        [Route("addexecutioner")]
        public ActionResult<DeedOutDTO> AddExecutioner([FromBody] AddDeedExecutionerDTO dto)
        {
            var deed = _logicService.AddExecutioner(dto);

            return _mapper.Map<DeedOutDTO>(deed);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(int deedId)
        {
            _logicService.DeleteDeed(deedId);
            return Ok();
        }

        [HttpPost]
        [Route("delete-executioner")]
        public ActionResult<DeedOutDTO> DeleteExecutioner(int deedId, int userId)
        {
            var deed = _logicService.DeleteExecutioner(deedId, userId);
            return _mapper.Map<DeedOutDTO>(deed);
        }
    }
}
