using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.SearchCriterias;
using DAL.Generator;
using System.Xml.Serialization;
using DAL.Exceptions;
using System.IO;

namespace DAL.Repositories
{
    [Serializable]
    public class XmlRepository : IFileRepository<SavedEntity>
    {
        private readonly string filePath;

        public XmlRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveState(SavedEntity entity)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SavedEntity));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, entity);
                }
            }
            catch (Exception ex)
            {
                throw new XmlException(ex.Message);
            }
        }
        public SavedEntity GetState()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SavedEntity));
            SavedEntity savedState = null;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    savedState = (SavedEntity)serializer.Deserialize(fs);
                }
            }
            catch(Exception ex)
            {
                throw new XmlException(ex.Message);
            }
            return savedState;
        }
    }
}
