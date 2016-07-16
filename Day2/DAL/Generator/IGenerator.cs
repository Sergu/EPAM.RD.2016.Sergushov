using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generator
{
    public interface IGenerator
    {
        int GenerateId();
        void SetIdPosition(int idPosition);
        int GetCurrentPosition();
    }
}
