using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;
using ng_server.Models;

namespace Business.Concrete
{
    public class PlaneManager : IPlaneService
    {
        private IPlanesRepository _planeRepository;
        private readonly IdentityContext _context;
        public PlaneManager(IPlanesRepository planeRepository, IdentityContext context){
            _planeRepository = planeRepository;
            _context= context;
        }

        public void Create(PlaneModel model)
        {
            var entity = new Planes
                {
                    Outgoing = model.Outgoing,
                    Incoming = model.Incoming,
                    Price = model.Price,
                    TicketTotal = model.TicketTotal,
                    Time = Convert.ToDateTime(model.Time),
                    PlaneState= EnumPlaneState.waiting
                };
                _planeRepository.Create(entity);
        }

        public void Delete(int Id)
        {
            var entity = _planeRepository.GetById(Id);

            if (entity != null)
            {
                entity.PlaneState = EnumPlaneState.cancel;
                Console.WriteLine("state"+ entity.PlaneState);
                _planeRepository.Update(entity);
                _context.SaveChanges();
            }
        }

        public List<Planes> GetAll()
        {
            return _planeRepository.GetAll();
        }

        public List<Planes> GetByFilter(string outgoing, string incoming)
        {
            return _planeRepository.GetByFilter(outgoing,incoming);
        }

        public Planes GetById(int Id)
        {
            return _planeRepository.GetById(Id);
        }

        public void Update(int id, PlaneModel model)
        {

                var entity = _planeRepository.GetById(id);

                if (entity == null)
                {
                    Console.WriteLine("Entity Boş");
                }

                if (string.IsNullOrEmpty(model.Incoming))
                {
                    Console.WriteLine("Entity Boş");
                }

                entity.Outgoing = model.Outgoing;
                entity.Incoming = model.Incoming;
                entity.Price = model.Price;
                entity.TicketTotal = model.TicketTotal;
                entity.Time = Convert.ToDateTime(model.Time);

                try
                {
                    _planeRepository.Update(entity);
                    _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating plane: {ex.Message}");
                }

            Console.WriteLine("ModelState is invalid:");

        }
    }
}