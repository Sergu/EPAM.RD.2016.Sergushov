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
    public  class XmlRepository : IFileRepository<UserEntity>
    {
        private readonly string filePath;

        public XmlRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveAll(IEnumerable<UserEntity> entities)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
