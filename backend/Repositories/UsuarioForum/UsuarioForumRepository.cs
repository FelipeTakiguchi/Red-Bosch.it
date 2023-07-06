using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;

public class UsuarioForumRepository : IUsuarioForumRepository
{
    private RedBoschContext ctx;

    public UsuarioForumRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(UsuarioForum obj)
    {
        await ctx.UsuarioForums.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(UsuarioForum obj)
    {
        ctx.UsuarioForums.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<UsuarioForum>> Filter(Expression<Func<UsuarioForum, bool>> exp)
    {
        var query = ctx.UsuarioForums.Where(exp);
        return query.ToListAsync();
    }

    public async Task<int> GetLastIndex()
    {
        var UsuarioForum = await ctx.UsuarioForums.OrderBy(usuarioForum => usuarioForum.Id).LastOrDefaultAsync();
        if(UsuarioForum != null)
            return UsuarioForum.Id;

        return 0; 
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public async Task Update(UsuarioForum obj)
    {
        ctx.UsuarioForums.Update(obj);
        await ctx.SaveChangesAsync();
    }
}