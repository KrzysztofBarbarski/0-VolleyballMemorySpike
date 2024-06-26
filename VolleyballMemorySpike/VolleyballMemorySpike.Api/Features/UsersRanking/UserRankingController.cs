﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolleyballMemorySpike.Api.Features.UsersRanking
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRankingController : ControllerBase
    {
        private readonly ISender _sender;

        public UserRankingController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetUsersRanking")]
        public async Task<IActionResult> Get()
        {
            var query = new GetUsersScores.Query();

            var result = await _sender.Send(query, CancellationToken.None);

            return Ok(result.Value);
        }
    }
}
