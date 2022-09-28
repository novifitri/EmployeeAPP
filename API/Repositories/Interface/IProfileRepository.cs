using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IProfileRepository
    {
        List<Profile> Get();
        Profile Get(int id);
        int Post(Profile profile);
        int Put(Profile profile);
        int Delete(int id);
    }
}
