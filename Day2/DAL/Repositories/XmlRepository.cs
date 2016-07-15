using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.SearchCriterias;
using DAL.Generator;

namespace DAL.Repositories
{
    public  class XmlRepository : IFileRepository<SavedEntity>
    {
        private readonly string filePath;

        public XmlRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveState(SavedEntity entity)
        {
            throw new NotImplementedException();
        }
        public SavedEntity GetState()
        {
            throw new NotImplementedException();
        }
    }
}
