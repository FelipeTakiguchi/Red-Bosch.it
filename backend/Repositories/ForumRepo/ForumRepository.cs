using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using backend.Model;

public class ForumRepository : IForumRepository
{
    private RedBoschContext ctx;

    public ForumRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(Forum obj)
    {
        await ctx.Forums.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Forum obj)
    {
        ctx.Forums.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
    {
        var query = ctx.Forums.Where(exp);
        return query.ToListAsync();
    }

    public async Task Save()
    {
        await this.ctx.SaveChangesAsync();
    }

    public async Task Update(Forum obj)
    {   
        ctx.Forums.Update(obj);
        await ctx.SaveChangesAsync();
    }

    Task<int> IForumRepository.GetLastIndex()
    {
        throw new NotImplementedException();
    }
}