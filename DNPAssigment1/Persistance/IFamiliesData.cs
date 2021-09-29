using System.Collections.Generic;
using DNPAssigment1.Models;
using Models;

namespace DNPAssigment1.Persistance
{
    public interface IFamiliesData
    {
        IList<T> ReadData<T>(string s);
        void SaveChanges();
        IList<Adult> LoadAdults(); 
        IList<Family> LoadFamilies();

        Family getFamily(int id);

        void Update(Family family);
        Adult getAdult(int id);

        void UpdateAdults(Adult adult);



    }
}