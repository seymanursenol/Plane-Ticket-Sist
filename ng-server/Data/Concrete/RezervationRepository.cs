using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;

namespace ng_server.Data.Concrete
{
#nullable disable
    public class RezervationRepository : Repository<Rezervation, IdentityContext>, IRezervationRepository
    {

        public void AddToRez(string userId, int PlanesId, double price)
        {
            throw new NotImplementedException();
        }

        public RezervationItem GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}