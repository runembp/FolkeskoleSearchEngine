using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolkeskoleSearchEngine.Data
{
    public class SchoolService
    {
        private List<School> _schoolList = new();
        
        public Task<List<School>> ListFromCsv()
        {
            _schoolList = new List<School>();
            var csvData = File.ReadAllLines("folkeskoler.csv");

            for(var i = 1; i < csvData.Length; i++)
            {
                if(!csvData[i].Contains(";"))
                    continue;
                
                var splitData = csvData[i].Split(";");
                _schoolList.Add(new School()
                {
                    Id = splitData[1],
                    Navn = splitData[2],
                    Address = $"{splitData[4]}, {splitData[5]} {splitData[6]}",
                    Telephone = splitData[7],
                    WebAddress = splitData[9],
                    Type = splitData[11]
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