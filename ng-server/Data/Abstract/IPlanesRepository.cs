using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.Entity;

namespace ng_server.Data.Abstract
{
    public interface IPlanesRepository: IRepository<Planes>
    {
        List<Planes> GetByFilter(string outgoing, string incoming);
    }
}