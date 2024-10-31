using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.Entity;

namespace ng_server.Data.Abstract
{
    public interface IRezervationRepository: IRepository<Rezervation>
    {
        RezervationItem GetByUserId(string userId);
        void AddToRez (string userId, int PlanesId, double price);
    }
}