using IPLCODE.DAO;
using IPLCODE.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPLCODE.Controllers
{
    [Route("api/ipl")]
    [ApiController]

    /*
     IMatchDAO:
    Type: Interface.
    Purpose: Defines methods for data access. Used to interact with data sources through dependency injection.
    string:
    Type: Primitive type.
    Purpose: Holds text data. In this case, it is used for storing a database connection string.
     */
    public class MatchController : Controller
    {
        private readonly IMatchDAO _matchDAO;

        public MatchController(IMatchDAO matchDAO)
        {
            _matchDAO = matchDAO;
        }

        [HttpGet("details")]

        // ActionResult: A type that encapsulates the result of an action method.
        // It allows you to return various HTTP response types, including status codes and data.
        public async Task<ActionResult<IEnumerable<MatchDetails>>> GetMatchDetails()
        {
            var matchDetails = await _matchDAO.GetMatchDetailsWithEngagementsAsync();
            return Ok(matchDetails);
        }


        [HttpGet("top-players")]
        public async Task<ActionResult<IEnumerable<PlayerStats>>> GetTopPlayers()
        {
            var playerStats = await _matchDAO.GetTop5PlayersByMatchesAndEngagementsAsync();
            return Ok(playerStats);
        }

        [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<MatchDetails>>> GetMatchesByDateRange([FromQuery] DateTime startDate,[FromQuery] DateTime endDate)
        {
            if (endDate < startDate)
            {
                return BadRequest("End date must be after or equal to start date.");
            }

            var matchDetails = await _matchDAO.GetMatchesByDateRangeAsync(startDate, endDate);
            return Ok(matchDetails);
        }


        /// <summary>
        /// Inserts a new player into the database.
        /// </summary>
        /// <param name="createPlayerRequest">The details of the player to be added.</param>
        /// <returns>Action result indicating the success or failure of the operation.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddPlayer([FromBody] CreatePlayerRequest createPlayerRequest)
        {
            if (createPlayerRequest == null)
            {
                return BadRequest("Player data must be provided.");
            }

            // Optionally, add validation for the player data here

            await _matchDAO.AddPlayerAsync(createPlayerRequest);
            return Ok("Player added successfully.");
        }


    }
}
