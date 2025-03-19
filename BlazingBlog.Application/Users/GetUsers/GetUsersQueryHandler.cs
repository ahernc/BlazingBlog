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
            var response = users.Adapt<List<UserResponse>>();
            return response;
        }
    }
}
