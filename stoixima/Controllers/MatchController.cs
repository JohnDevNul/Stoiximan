using Microsoft.AspNetCore.Mvc;
using Stoixima.Data;
using Stoixima.Data.Db;
using Stoixima.Dtos;
using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepo _match;
        private readonly ITeamRepo _team;
        private readonly ITimeFlowRepo _timeFlow;

        public MatchController(IMatchRepo matchRepo, ITeamRepo teamRepo, ITimeFlowRepo timeFlowRepo)
        {
            _match = matchRepo;

            _team = teamRepo;

            _timeFlow = timeFlowRepo;
        }
        
        [HttpGet]
        public ActionResult <IEnumerable<MatchModel>> GetAllMatches()
        {
            var matches = _match.GetAllMatches();

            return Ok(matches); 
        }

        [HttpGet("{id}")]
        public ActionResult <MatchModel> GetMatchById(int id)
        {
            var match = _match.GetMatchById(id);

            return Ok(match);
        }

        [HttpPost]
        public ActionResult <MatchModel> CreateMatch(MatchDto match)
        {
            var matchModel = new MatchModel();

            matchModel.Time = match.Time;
            matchModel.Home = _team.GetTeamById(match.HomeTeamId);
            matchModel.Away = _team.GetTeamById(match.AwayTeamId);
            matchModel.Goals = match.Goals;
            matchModel.State = match.State;
            matchModel.Corners = match.Corners;
            matchModel.Cards = match.Cards;
            matchModel.StartTime = match.StartTime;

            var matchItem = _match.CreateMatch(matchModel);

            return Ok(matchItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMatch(int id, MatchModel match)
        {
            _match.UpdateMatch(id, match);

            return Ok();
        }

        public ActionResult DeleteMatch(int id)
        {
            _match.DeleteMatch(id);

            return NoContent();
        }
    }
}
