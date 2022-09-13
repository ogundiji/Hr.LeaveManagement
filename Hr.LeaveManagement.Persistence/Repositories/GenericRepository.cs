using HR.LeaveManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly LeaveManagementDbContext leaveManagementDbContext;
        public GenericRepository(LeaveManagementDbContext leaveManagementDbContext)
        {
            this.leaveManagementDbContext = leaveManagementDbContext;
        }

        public async Task<T> Add(T entity)
        {
            await leaveManagementDbContext.AddAsync(entity);
            return entity;

        }

        public async Task Delete(T entity)
        {
            leaveManagementDbContext.Set<T>().Remove(entity);
           
        }

        public async Task<T> Get(int id)
        {
            return await leaveManagementDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await leaveManagementDbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> isExist(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task Update(T entity)
        {
            leaveManagementDbContext.Entry(entity).State = EntityState.Modified;
            
        }
    }
}
