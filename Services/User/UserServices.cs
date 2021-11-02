using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Services
{
    public class UserServices // : Service
    {
        protected readonly DataContext _dataContext;
        private UserServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
    }
}
