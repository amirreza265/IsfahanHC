using IsfahanHC.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Data.Repository
{
    public interface IEditTicketRepository
    {
        string CreateTicket(double ExpireMinuts,string value = "");
        string VarifyTicket(string code);
    }

    public class EditTicketRepository : IEditTicketRepository
    {
        IsfahanHCDbContext _context;

        public EditTicketRepository(IsfahanHCDbContext context)
        {
            _context = context;
            DeleteExpiredTickets();
        }

        public string CreateTicket(double ExpireMinuts, string value)
        {
            var ticket = new EditTicket
            {
                CreateTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddMinutes(ExpireMinuts),
                Value = value,
                EditCode = Guid.NewGuid().ToString().Replace("-","a")
            };
            _context.EditTickets.Add(ticket);
            _context.SaveChanges();
            return ticket.EditCode;
        }

        private void DeleteExpiredTickets()
        {
            var ticks = _context.EditTickets
                .Where(t => t.ExpirationTime < DateTime.Now);

            _context.EditTickets.RemoveRange(ticks);
        }

        /// <summary>
        /// retutn Ticket value and delete ticket
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string VarifyTicket(string code)
        {
            var ticket = _context.EditTickets
                .Where(t => t.ExpirationTime > DateTime.Now && t.EditCode == code)
                .FirstOrDefault();

            if (ticket == null)
                return "Invalid Code!";

            var val = ticket.Value;
            _context.Remove(ticket);
            _context.SaveChanges();
            return val;
        }
    }
}
