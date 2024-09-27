namespace Books.DataAccessLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        void Remove(T entity);
        //Task DeleteAsync(int id);
    }
}
