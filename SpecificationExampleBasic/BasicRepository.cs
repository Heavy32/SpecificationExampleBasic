using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SpecificationExampleBasic.Models;
using SpecificationExampleBasic.Specifications;

namespace SpecificationExampleBasic
{
    public class BasicRepository<T> where T : CommonEntity
    {
        private readonly List<T> _entities;

        public BasicRepository(List<T> entities)
        {
            _entities = entities ?? new List<T>();
        }

        public List<T> GetAll()
        {
            return _entities.ToList();
        }

        public T? GetById(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public List<T> GetBySpecification(ISpecification<T> specification)
        {
            var query = _entities.AsQueryable();
            
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            foreach (var include in specification.Includes)
            {
                // тут типа идёт пробег по всем вложенным сущностям
            }

            return query.ToList();
        }

        public T? GetFirstBySpecification(ISpecification<T> specification)
        {
            return GetBySpecification(specification).FirstOrDefault();
        }

        public int CountBySpecification(ISpecification<T> specification)
        {
            var query = _entities.AsQueryable();
            
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            return query.Count();
        }

        public void Add(T entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = _entities.Count > 0 ? _entities.Max(e => e.Id) + 1 : 1;
            }
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            var existing = _entities.FirstOrDefault(e => e.Id == entity.Id);
            if (existing != null)
            {
                var index = _entities.IndexOf(existing);
                _entities[index] = entity;
            }
        }

        public void Delete(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }
}
