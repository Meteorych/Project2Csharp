using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigHandlerLibraries;

namespace XMLandJSON.Repository
{
    public interface IRepositoryOperations
    {
        public void Delete(string userName);
        public void Update(string updateFilePath, IConfigurable config);
    }
}
