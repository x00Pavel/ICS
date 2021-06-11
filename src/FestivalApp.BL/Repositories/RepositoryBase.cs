using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Repositories;
using FestivalApp.DAL;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

public class RepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
{
    private FestivalDbContext _context;
    private DbSet<TEntity> _dbSet;

    public RepositoryBase(FestivalDbContext context)
    {
        this._context = context;
        this._dbSet = context.Set<TEntity>();
    }

    public void Delete(TEntity entity)
    {
        this._dbSet.Remove(entity);
        this._context.SaveChanges();
    }

    public void DeleteById(Guid entityId)
    {
        Delete(GetById(entityId));
    }

    public TEntity GetById(Guid entityId)
        => this._dbSet.SingleOrDefault(entity => entity.Id.Equals(entityId));

    public IQueryable<TEntity> GetAll()
            => this._dbSet;

    public TEntity InsertOrUpdate(TEntity entity)
    {
        if (entity.GetType() == typeof(PerformanceEntity))
        {
            //Collision check has been moved to Application Layer
            if (IsTimeCollision(Cast<PerformanceEntity>(entity)))
                throw new ArgumentOutOfRangeException($"There is a colision on performances, shooce another time slot");
        }


        _context.Update<TEntity>(entity);
        SynchronizeCollections(entity);
        _context.SaveChanges();
        return entity;
    }

    private bool IsTimeCollision(PerformanceEntity newPerformance)
    {
        return _dbSet.Cast<PerformanceEntity>().Any(perf => (perf.Id != newPerformance.Id)
                                                            && (perf.BandId == newPerformance.BandId ||
                                                                 perf.StageId == newPerformance.StageId)
                                                                &&
                                                                (
                                                                    perf.From <= newPerformance.From && newPerformance.From < perf.To ||
                                                                    perf.From < newPerformance.To && newPerformance.To <= perf.To
                                                                ));
    }

    private void SynchronizeCollections(TEntity entity)
    {
        var collectionsToBeSynchronized = typeof(TEntity).GetProperties().Where(i =>
            i.PropertyType.IsGenericType && i.PropertyType.GetGenericTypeDefinition() == typeof(ICollection));

        if (!collectionsToBeSynchronized?.Any() ?? false)
        {
            return;
        }

        var entityInDb = GetById(entity.Id);
        if (entityInDb == null)
        {
            return;
        }

        foreach (var collectionSelector in collectionsToBeSynchronized)
        {
            var updatedCollection = ((collectionSelector.GetValue(entity) as ICollection<IEntity>) ?? throw new InvalidOperationException()).ToArray();
            var collectionInDb = ((collectionSelector.GetValue(entityInDb) as ICollection<IEntity>) ?? throw new InvalidOperationException()).ToArray();

            foreach (var item in collectionInDb)
            {
                if (!updatedCollection.Contains(item, PrimaryKeyComparers.IdComparer))
                {
                    this.DeleteById(item.Id);
                }
            }
        }
    }

    private static T Cast<T>(Object o)
    {
        return (T)o;
    }
}
