using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class SavedEntity
    {
        public IEnumerable<UserEntity> Users { get; set; }
        public int GeneratorPosition { get; set; }
        public SavedEntity(IEnumerable<UserEntity> users, int genetatorPosition)
        {
            this.Users = users;
            this.GeneratorPosition = genetatorPosition;
        }
    }
}
