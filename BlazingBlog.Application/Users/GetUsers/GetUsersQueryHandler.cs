using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Users.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserResponse>>
    {

        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (!await _userService.IsCurrentUserInRoleAsync("Admin"))
            {
                return Result.Fail<List<UserResponse>>("You are not allowed to see all users");
            }
            var users = await _userRepository.GetAllUsersAsync();

            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                var userResponse = user.Adapt<UserResponse>();
                userResponse.Roles = string.Join(", ", await _userService.GetUserRolesAsync(user.Id));
                response.Add(userResponse);
            }

            return response;
        }
    }
}
