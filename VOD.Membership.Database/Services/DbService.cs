namespace VOD.Membership.Database.Services
{
    public class DbService : IDbService
    {
        private readonly VODContext db;
        private readonly IMapper mapper;

        public DbService(VODContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class, IEntity
            where TDto : class
        {
            var entities = await db.Set<TEntity>().ToListAsync();
            return mapper.Map<List<TDto>>(entities);
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entitites = await db.Set<TEntity>()
                .Where(expression)
                .ToListAsync();
            return mapper.Map<List<TDto>>(entitites);

        }

        private async Task<TEntity?> SingleAsync<TEntity>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity =>
            await db.Set<TEntity>().SingleOrDefaultAsync(expression);

        public async Task<TDto> SingleAsync<TEntity, TDto>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = await SingleAsync(expression);
            return mapper.Map<TDto>(entity);
        }

        public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entity = mapper.Map<TEntity>(dto);
            await db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> SaveChangesAsync() =>
        await db.SaveChangesAsync() >= 0;

        public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
        => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";

        public void Update<TEntity, TDto>(int id, TDto dto)
        where TEntity : class, IEntity
        where TDto : class
        {
            var entity = mapper.Map<TEntity>(dto);
            entity.Id = id;
            db.Set<TEntity>().Update(entity);
        }

        public async Task<bool> AnyAsync<TEntity>(
        Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity =>
            await db.Set<TEntity>().AnyAsync(expression);

        public async Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity
        {
            try
            {
                var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
                if (entity is null) return false;
                db.Remove(entity);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public void Include<TEntity>() where TEntity : class, IEntity
        {
            var propertyNames = db.Model.FindEntityType(typeof(TEntity))?
                .GetNavigations().Select(e => e.Name);

            if (propertyNames is null) return;

            foreach (var name in propertyNames)
                db.Set<TEntity>().Include(name).Load();
        }

    }
}

