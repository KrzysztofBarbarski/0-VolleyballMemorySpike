using VolleyballMemorySpike.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolleyballMemorySpike.Authentication;
using VolleyballMemorySpike.Database.Entities.Users;
using User = VolleyballMemorySpike.Shared.Models.User;

namespace VolleyballMemorySpike.Api.Features.Users;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly AuthenticationService _authService;

    public UsersController(ISender sender, AuthenticationService authService)
    {
        _sender = sender;
        _authService = authService;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> Get(string userAzureId)
    {
        var query = new GetUserByAzureId.Query(userAzureId);
        
        var result = await _sender.Send(query, CancellationToken.None);

        var output = new ServiceResponse<User>
        {
            Value = result.Value,
            IsSuccess = result.IsSuccess
        };

        return Ok(output);
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(CancellationToken cancellationToken)
    {
        var currentUser = _authService.GetCurrentUser();

        if (currentUser == null)
        {
            return BadRequest(UserErrors.UserIdDontExist);
        }

        var command = new AddUser.Command(currentUser);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

}
