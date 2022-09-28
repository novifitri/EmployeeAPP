using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class ProfileRepository : IProfileRepository
    {
        MyContext myContext;

        public ProfileRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.Profile.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Profile> Get()
        {
            var data = myContext.Profile.Include(x => x.Karyawan).ToList();
            return data;
        }

        public Profile Get(int id)
        {
            var data = myContext.Profile.Include(x => x.Karyawan).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public int Post(Profile profile)
        {
            myContext.Profile.Add(profile);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Profile profile)
        {
            var data = Get(profile.Id);
            if (data == null)
                return -1;
            data.Username = profile.Username;
            data.Email = profile.Email;
            data.Password = profile.Password;
            data.Karyawan_Id = profile.Karyawan_Id;
            myContext.Profile.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
