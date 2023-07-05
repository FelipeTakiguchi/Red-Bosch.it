using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;

public class PostRepository : IPostRepository
{
    private RedBoschContext ctx;

    public PostRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(Post obj)
    {
        await ctx.Posts.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Post obj)
    {
        ctx.Posts.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<Post>> Filter(Expression<Func<Post, bool>> exp)
    {
        var query = ctx.Posts.Where(exp);
        return query.ToListAsync();
    }

    public Task<int> GetLastIndex()
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public async Task Update(Post obj)
    {
        ctx.Posts.Update(obj);
        await ctx.SaveChangesAsync();
    }
}