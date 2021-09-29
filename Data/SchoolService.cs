using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolkeskoleSearchEngine.Data
{
    internal enum SchoolEnum
    {
        Name = 2,
        Street = 4,
        PostCode = 5,
        City = 6,
        Telephone = 7,
        Webaddress = 9,
        Type = 11
    }
    
    public class SchoolService
    {
        private List<School> _schoolList = new();
        
        public Task<List<School>> ListFromCsv(string path = "folkeskoler.csv")
        {
            _schoolList = new List<School>();
            var csvData = File.ReadAllLines(path);

            for(var i = 1; i < csvData.Length; i++)
            {
                if(!csvData[i].Contains(";"))
                    continue;
                
                var splitData = csvData[i].Split(";");
                _schoolList.Add(new School
                {
                    Navn = splitData[(int) SchoolEnum.Name],
                    Address = $"{splitData[(int) SchoolEnum.Street]}, {(int) SchoolEnum.PostCode} {(int) SchoolEnum.City}",
                    Telephone = splitData[(int) SchoolEnum.Telephone],
                    WebAddress = splitData[(int) SchoolEnum.Webaddress],
                    Type = splitData[(int) SchoolEnum.Type]
                });
            }

            return Task.FromResult(_schoolList);
        }

        public Task<List<School>> ListFromCsvWithNameFiltered(string userInput = "") 
        {
            var filteredSchoolList = _schoolList.Where(x => x.Navn.Contains(userInput)).ToList();
            
            return Task.FromResult(filteredSchoolList);
        }
    }
}