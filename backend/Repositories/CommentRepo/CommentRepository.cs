using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using backend.Model;

public class CommentRepository : ICommentRepository
{
    private RedBoschContext ctx;

    public CommentRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(Comentario obj)
    {
        await ctx.Comentarios.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(Comentario obj)
    {
        ctx.Comentarios.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<Comentario>> Filter(Expression<Func<Comentario, bool>> exp)
    {
        var query = ctx.Comentarios.Where(exp);
        return query.ToListAsync();
    }

    public async Task Save()
    {
        await this.ctx.SaveChangesAsync();
    }

    public async Task Update(Comentario obj)
    {   
        ctx.Comentarios.Update(obj);
        await ctx.SaveChangesAsync();
    }

    async Task<int> ICommentRepository.GetLastIndex()
    {
        var comentario = await ctx.Comentarios.OrderBy(comentario => comentario.Id).LastOrDefaultAsync();
        if(comentario != null)
            return comentario.Id;

        return 0; 
    }
}