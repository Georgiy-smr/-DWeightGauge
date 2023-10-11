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

        public T Add(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }





        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T item, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
