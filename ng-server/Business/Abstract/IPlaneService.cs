using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.Entity;
using ng_server.Models;

namespace Business.Abstract
{
    public interface IPlaneService
    {
        List<Planes> GetAll();
        Planes GetById(int Id);
        List<Planes> GetByFilter(string outgoing, string incoming);
        void Create(PlaneModel model);
        void Delete(int Id);
        void Update(int id, PlaneModel model);
    }
}