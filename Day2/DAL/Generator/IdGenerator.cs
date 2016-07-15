using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomNumberGenerators;

namespace DAL.Generator
{
    public class IdGenerator : IGenerator
    {
        private IEnumerable<int> generatedId;
        private int currentPos;
        public IdGenerator(INumberGenerator numberGenerator, int startPosition)
        {
            generatedId = numberGenerator.GenerateNumberEnumerable();
            this.currentPos = startPosition;
        }
        public int GetId()
        {
            return generatedId.Skip(currentPos++).Take(1).FirstOrDefault();
        }
        public void SetIdPosition(int idPosition)
        {
            currentPos = idPosition;
        }
    }
}
