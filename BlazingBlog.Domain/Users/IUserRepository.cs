﻿namespace BlazingBlog.Domain.Users
{
    public interface IUserRepository
    {
        Task<IUser?> GetUserByIdAsync(string userId);
        Task<List<IUser>> GetAllUsersAsync();
    }
}
