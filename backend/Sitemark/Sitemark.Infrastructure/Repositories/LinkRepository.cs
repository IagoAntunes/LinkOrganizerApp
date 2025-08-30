using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sitemark.Domain.Entities;
using Sitemark.Domain.Repositories;
using Sitemark.Infrastructure.Data;

namespace Sitemark.Infrastructure.Repositories
{
    internal class LinkRepository : ILinkRepository
    {
        private readonly SitemarkDbContext dbContext;
        private readonly IMapper mapper;

        public LinkRepository(
            SitemarkDbContext dbContext,
            IMapper mapper
            )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Result<LinkEntity>> CreateLinkAsync(LinkEntity link)
        {
            var result = await dbContext.Links.AddAsync(link);
            await dbContext.SaveChangesAsync();

            return Result.Success(result.Entity);
        }

        public async Task<Result<LinkEntity>> DeleteLinkAsync(Guid linkId)
        {
            var linkToDelete = await dbContext.Links.FirstOrDefaultAsync(link => link.Id == linkId);
            if(linkToDelete == null)
            {
                return Result.Failure<LinkEntity>(new Error("LinkNotFound", "Link not found."));
            }
            dbContext.Links.Remove(linkToDelete);
            await dbContext.SaveChangesAsync();
            return Result.Success(linkToDelete);
        }

        public async Task<Result<List<LinkEntity>>> GetLinksAsync(Guid userId)
        {
            var links = await dbContext.Links
                .Where(link => link.UserId == userId.ToString())
                .Include(link => link.User)
                .ToListAsync();

           return Result.Success(links);
        }
    }
}
