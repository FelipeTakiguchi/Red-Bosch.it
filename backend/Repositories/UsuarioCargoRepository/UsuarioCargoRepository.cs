using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;

public class UsuarioCargoRepository : IUsuarioCargoRepository
{
    private RedBoschContext ctx;

    public UsuarioCargoRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(UsuarioCargo obj)
    {
        await ctx.UsuarioCargos.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(UsuarioCargo obj)
    {
        ctx.UsuarioCargos.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public Task<List<UsuarioCargo>> Filter(Expression<Func<UsuarioCargo, bool>> exp)
    {
        var query = ctx.UsuarioCargos.Where(exp);
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

    public async Task Update(UsuarioCargo obj)
    {
        ctx.UsuarioCargos.Update(obj);
        await ctx.SaveChangesAsync();
    }
}