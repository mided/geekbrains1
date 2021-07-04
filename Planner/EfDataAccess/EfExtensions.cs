using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EfDataAccess
{
    public static class EfExtensions
    {
        public static IQueryable<object> Set(this DbContext _context, Type t)
        {
            var set = _context.GetType().GetMethods().First(m => m.Name == "Set" && !m.GetParameters().Any());

            return (IQueryable<object>)set.MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
