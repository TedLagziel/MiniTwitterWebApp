using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniTwitterWebApp.Data;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Services
{
    public class ProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Profile FindProfileById(int id)
        {
            return null;
        }

        public void CreateNewProfile(string displayName)
        {

        }

    }
}
