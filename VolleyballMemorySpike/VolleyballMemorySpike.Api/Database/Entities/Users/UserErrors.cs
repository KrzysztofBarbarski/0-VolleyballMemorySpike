using VolleyballMemorySpike.Api.Abstraction;

namespace VolleyballMemorySpike.Database.Entities.Users;

public static class UserErrors
{
    public static Error DuplicateEmail = new(
        "User.DuplicateEmail",
        "Email is already in use.");

    public static Error UserIdDontExist = new(
        "User.UserIdNotFound",
        "User ID wasn't found");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid credentials.");
}
