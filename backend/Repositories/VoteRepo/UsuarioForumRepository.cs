using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;

public class VoteRepository : IVoteRepository
{
    private RedBoschContext ctx;

    public VoteRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(Vote obj)
    {
        await ctx.Votes.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Vote obj)
    {
        ctx.Votes.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<Vote>> Filter(Expression<Func<Vote, bool>> exp)
    {
        var query = ctx.Votes.Where(exp);
        return query.ToListAsync();
    }

    public async Task<int> GetLastIndex()
    {
        var Vote = await ctx.Votes.OrderBy(vote => vote.Id).LastOrDefaultAsync();
        if(Vote != null)
            return Vote.Id;

        return 0; 
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public async Task Update(Vote obj)
    {
        ctx.Votes.Update(obj);
        await ctx.SaveChangesAsync();
    }
}