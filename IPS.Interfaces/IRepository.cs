using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPS.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Обеспечение доступа к БД
        /// </summary>
        IQueryable<T> Items { get; }
        /// <summary>
        /// Возвращает сущность по индификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Возвращает сущность по индификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int Id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавляет сущность и возвращает её
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Add(T item);
        /// <summary>
        /// Добавляет сущность и возвращает её
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> AddAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Обновляет репозитория
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);    
        /// <summary>
        /// Удаление из репозитория
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        void Remove(int id);
        /// <summary>
        /// Удаление из репозитория
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);


    }
}
