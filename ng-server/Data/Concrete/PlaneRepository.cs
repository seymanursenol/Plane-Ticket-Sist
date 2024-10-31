using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;

namespace ng_server.Data.Concrete
{
    public class PlaneRepository : Repository<Planes, IdentityContext>, IPlanesRepository
    {
        public List<Planes> GetByFilter(string outgoing, string incoming)
        {
            using (var context = new IdentityContext())
            {
                return context.Set<Planes>().Where(p => p.Outgoing == outgoing && p.Incoming == incoming).ToList();
            }
        }
    }

}