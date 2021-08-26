using Microsoft.AspNetCore.Mvc;
using Stoixima.Data;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepo _repository;

        public TeamController(ITeamRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult <IEnumerable<TeamModel>> GetAllTeams()
        {
            var teamItems = _repository.GetAllTeams();

            return Ok(teamItems);
        }

        [HttpGet("{id}")]
        public ActionResult <TeamModel> GetTeamById(int id)
        {
            var teamItem = _repository.GetTeamById(id);

            return Ok(teamItem);
        }

        [HttpPost]
        public ActionResult <TeamModel> AddTeam(TeamModel team)
        {
            var teamItem = _repository.CreateTeam(team);

            return Ok(teamItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTeam(int id, TeamModel team)
        {
            _repository.UpdateTeam(id, team);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTeam(int id)
        {
            _repository.DeleteTeam(id);

            return NoContent();
        }
    }
}
