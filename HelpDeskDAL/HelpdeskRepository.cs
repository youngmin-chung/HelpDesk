using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpDeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T : HelpdeskEntity
    {
        private HelpdeskContext ctx = null;

        public HelpdeskRepository(HelpdeskContext context = null)
        {
            ctx = context != null ? context : new HelpdeskContext();
        }

        public List<T> GetAll()
        {
            return ctx.Set<T>().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> lambdaExp)
        {
            return ctx.Set<T>().Where(lambdaExp).ToList();
        }

        public T Add(T entity)
        {
            ctx.Set<T>().Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                HelpdeskEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                ctx.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                ctx.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
                if (ctx.SaveChanges() == 1)
                {
                    opStatus = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                opStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + " " + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return opStatus;
        }
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            ctx.Set<T>().Remove(currentEntity);
            return ctx.SaveChanges();
        }
    }
}
