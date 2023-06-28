using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;
using backend.Model;


public class ImageRepository : IRepository<ImageDatum>
{
    private RedBoschContext ctx;

    public ImageRepository(RedBoschContext ctx)
        => this.ctx = ctx;

    public async Task Add(ImageDatum obj)
    {
        await ctx.ImageData.AddAsync(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Delete(ImageDatum obj)
    {
        ctx.ImageData.Remove(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task Update(ImageDatum obj)
    {
        ctx.ImageData.Update(obj);
        await ctx.SaveChangesAsync();
    }

    public async Task<List<ImageDatum>> Filter(Expression<Func<ImageDatum, bool>> exp)
    {
        var query = ctx.ImageData.Where(exp);
        return await query.ToListAsync();
    }

    public async Task Save()
    {
        await ctx.SaveChangesAsync();
    }
}