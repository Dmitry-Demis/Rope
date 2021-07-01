using Rope.Model;
using System.Collections.Generic;

namespace Rope.Interfaces
{
    public interface IFileService
    {
        List<Parameter> Open(string fileName);
        void Save(string fileName, List<Parameter> parameters);
    }
}
