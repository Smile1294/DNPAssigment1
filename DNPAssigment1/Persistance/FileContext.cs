using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DNPAssigment1.Models;
using Models;

namespace DNPAssigment1.Persistance 
{
    public class FileContext : IFamiliesData
    {
        public IList<Family> Families { get; private set; }
        public IList<Adult> Adults { get; private set; }

        private readonly string familiesFile = "families.json";
        private readonly string adultsFile = "adults.json";

        public FileContext()
        {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
        }

        public Adult getAdult(int id)
        {
            return Adults.FirstOrDefault(t => t.Id == id);
        }

        public void UpdateAdults(Adult adultd)
        {
            Adult adult = Adults.First(t => t.Id == adultd.Id);
            adult = adultd;
            SaveChanges();  
        }

        public void AddAdult(Adult adult)
        {
            int max = Adults.Max(adult => adult.Id);
            adult.Id = (++max);
            Adults.Add(adult);
            SaveChanges();
        }

        public void Update(Family family)
        {
            Family familytoupdate = Families.First(t => t.Id == family.Id);
            familytoupdate = family;
            SaveChanges();
        }

        public Family getFamily(int id)
        {
            return Families.FirstOrDefault(t => t.Id == id);
        }


        public IList<Adult> LoadAdults()
        {
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            return Adults;
        }

        public IList<Family> LoadFamilies()
        {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            return Families;
        }
        public IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges()
        {
            // storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(familiesFile, false))
            {
                outputFile.Write(jsonFamilies);
            }

            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
            }
        }
    }
}