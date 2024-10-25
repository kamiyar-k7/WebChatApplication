﻿using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using InfruStructure.WebChatDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfruStructure.Repository.UserPart;

public class UserRepository : IUserRepository
{
    private readonly ChatDbContext _DbContext;
    #region Ctor
    public UserRepository(ChatDbContext chatDbContext)
    {
        _DbContext = chatDbContext;
    }
    #endregion

    public async Task SaveChanges()
    {
        await _DbContext.SaveChangesAsync();
    }

    #region Auth Services

    public async Task AddUser(User user)
    {
        await _DbContext.Users.AddAsync(user);
        await SaveChanges();
    }

    public async Task<User?> SignIn(string email, string password)
    {
        return await _DbContext.Users.Where(user => user.UserEmail == email && user.Password == password).Select(x => new User
        {
            Id = x.Id,
            UserEmail = x.UserEmail,
            UserName = x.UserName,
            CreatedAt = DateTime.Now,
        }).FirstOrDefaultAsync();

    }

    public async Task<bool> IsExist(string email)
    {
        return await _DbContext.Users.AnyAsync(x => x.UserEmail == email);
    }
    #endregion


    // Search 
    public async Task<List<User>> FindUsers(string UserName)
    {

        return await _DbContext.Users
     .Where(u => u.UserName.Contains(UserName))
     .OrderByDescending(u => u.UserName.StartsWith(UserName))
     .ThenBy(u => u.UserName)
     .Select(x => new User
     {
         UserName = x.UserName,
         Id = x.Id,
     })
     .AsQueryable().AsNoTracking().ToListAsync();
    }



}



