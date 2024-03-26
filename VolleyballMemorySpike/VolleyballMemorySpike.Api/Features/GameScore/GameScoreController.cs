using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolleyballMemorySpike.Authentication;
using VolleyballMemorySpike.Contracts;
using VolleyballMemorySpike.Database.Entities.Users;
using VolleyballMemorySpike.Features.GameScore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GameScoreController : ControllerBase
{
    private readonly ISender _sender;
    private readonly AuthenticationService _authService;

    public GameScoreController(
        ISender sender, AuthenticationService authService)
    {
        _sender = sender;
        _authService = authService;
    }

    [HttpPost("AddUserScore")]
    public async Task<IActionResult> AddUserScore(AddUserScoreRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = await _authService.GetCurrentUserId();

        if(currentUserId == null)
        {
            return BadRequest(UserErrors.UserIdDontExist);
        }

        var command = new AddUserScore.Command(
            request.SessionId,
            request.Score,
            currentUserId.Value);
        
        var result = await _sender.Send(command, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
