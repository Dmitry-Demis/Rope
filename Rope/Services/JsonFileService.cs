using Rope.Interfaces;
using Rope.Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Rope.Services
{
    public class JsonFileService : IFileService
    {
        public List<Parameter> Open(string fileName)
        {
            List<Parameter> parameters = new List<Parameter>();
            DataContractJsonSerializer jsonFormatter =
                 new DataContractJsonSerializer(typeof(List<Parameter>));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                parameters = jsonFormatter.ReadObject(fs) as List<Parameter>;
            }
            return parameters;
        }

        public void Save(string fileName, List<Parameter> parameters)
        {
            DataContractJsonSerializer jsonFormatter =
               new DataContractJsonSerializer(typeof(List<Parameter>));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, parameters);
            }
        }
    }
}
