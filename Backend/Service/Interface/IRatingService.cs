using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Model;

namespace Backend.Service.Interface
{
    public interface IRatingService : IBaseService<Rating, long, IRatingRepository>
    {
    }
}
