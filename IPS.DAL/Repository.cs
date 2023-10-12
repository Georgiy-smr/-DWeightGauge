using IPS.DAL.BASE;
using IPS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPS.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace IPS.DAL
{
    internal class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DBContext _DB;
        private readonly DbSet<T> _Set;
        public Repository(DBContext DB)
        {
            _DB = DB; 
            _Set = _DB.Set<T>();
        }

        public virtual IQueryable<T> Items => _Set;
        public T Get(int id) => Items.SingleOrDefault(x => x.Id == id);
        public async Task<T> GetAsync(int Id, CancellationToken cancellationToken = default) =>
            await Items.SingleOrDefaultAsync(x => x.Id == Id);

        public bool AutoSaveChanged { get; set; } = true;
        public T Add(T item)
        {
            if(item == null) throw new ArgumentNullException(nameof(item));
            _DB.Entry(item).State = EntityState.Added;
            if (AutoSaveChanged) _DB.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _DB.Entry(item).State = EntityState.Added;
            if (AutoSaveChanged) await _DB.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return item;
        }
        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _DB.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanged) _DB.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _DB.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanged) 
                await _DB.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            _DB.Remove(new T { Id = id });
            if (AutoSaveChanged) 
                _DB.SaveChanges();

        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            _DB.Remove(new T { Id = id });
            if (AutoSaveChanged)
               await _DB.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

    }

    internal class RepositoryIPS : Repository<IPS>
    {
        public override IQueryable<IPS> Items => base.Items.Include(item => item.IPS2Cargoes);
        public RepositoryIPS(DBContext DB) : base(DB) {}
    }
    internal class RepositoryCagro : Repository<Cargo>
    {
        public override IQueryable<Cargo> Items => base.Items.Include(item => item.IPS2Cargoes);
        public RepositoryCagro(DBContext DB) : base(DB) {}
    }



}
