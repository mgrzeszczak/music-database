using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository;
using Backend.Repository.Interface;
using Common.Model;

namespace Backend.Service.Interface
{
    public interface ICommentService : IBaseService<Comment, long, ICommentRepository>
    {

    }
}
