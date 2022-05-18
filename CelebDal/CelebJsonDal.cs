using System;
using System.Collections.Generic;
using System.IO;
using CelebContracts;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using DIContracts;

namespace CelebDal
{
    [Register(Policy.Singleton, typeof(ICelebDal))]
    public class CelebJsonDal : ICelebDal
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options;
        public CelebJsonDal()
        {
            try
            {
                String workingDirectory = Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                _path = Path.Combine(projectDirectory, "CelebDB.txt");
            }
            catch (NullReferenceException)
            {
                _path = "CelebsDB.txt";
            }
            _options = new JsonSerializerOptions { WriteIndented = true };
        }
        public bool DbExists()
        {
            return File.Exists(_path);
        }
        public void ResetDb(Dictionary<string, CelebDto> celebs)
        {
            string jsonString = JsonSerializer.Serialize(celebs, _options);
            File.WriteAllText(_path, jsonString);
        }

        public Dictionary<string, CelebDto> GetAll()
        {
            string jsonString = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<Dictionary<string, CelebDto>>(jsonString);
        }

        public void Remove(string key)
        {
            Dictionary<string, CelebDto> celebs= GetAll();
            celebs.Remove(key);
            string jsonString = JsonSerializer.Serialize(celebs, _options);
            File.WriteAllText(_path, jsonString);
        }

        public void RemoveAll()
        {
            Dictionary<string, CelebDto> emptyDb = new Dictionary<string, CelebDto>();
            string jsonString = JsonSerializer.Serialize(emptyDb);
            File.WriteAllText(_path, jsonString);
        }

        
    }
}
