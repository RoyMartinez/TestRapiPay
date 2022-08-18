using Domain.Interfaces.Repositories;
using Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IConfiguration _config;
        public BaseRepository
        (
            IConfiguration configuration
        )
        {
            _config = configuration;
        }

        /// <summary>
        /// Add a Entity to the Database
        /// </summary>
        /// <param name="entity">Entidad a agregar</param>
        public void Add(T entity)
        {
            using (var _context = new RapidPayContext(_config))
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Finds Entities in the DataBase
        /// </summary>
        /// <param name="predicate">Expresion para buscar las entidades</param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query;
            using (var _context = new RapidPayContext(_config))
            {
                query = _context.Set<T>().Where(predicate).ToList().AsQueryable();
            }
            return query;
        }

        /// <summary>
        /// Get all the Entities in the Database
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query;
            using (var _context = new RapidPayContext(_config))
            {
                query = (from p in _context.Set<T>() select p).ToList()
                        .AsQueryable();
            }
            return query;
        }


        /// <summary>
        /// Metodo que edita una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a editar</param>
        public async void Edit(T entity)
        {
            using (var _context = new RapidPayContext(_config))
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
