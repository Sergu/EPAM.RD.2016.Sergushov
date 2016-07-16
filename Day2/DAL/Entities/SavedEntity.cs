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
        public List<UserEntity> Users { get; set; }
        public int GeneratorPosition { get; set; }
    }
}
