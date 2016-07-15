using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomNumberGenerators;

namespace DAL.Generator
{
    public class IdGenerator : IGenerator, IGeneratorTracker
    {
        private IEnumerable<int> generatedId;
        private int currentPos;
        public IdGenerator(INumberGenerator numberGenerator)
        {
            generatedId = numberGenerator.GenerateNumberEnumerable();
            this.currentPos = 0;
        }
        public int GenerateId()
        {
            return generatedId.Skip(currentPos++).Take(1).FirstOrDefault();
        }
        public void SetIdPosition(int idPosition)
        {
            currentPos = idPosition;
        }
        public int GetCurrentPosition()
        {
            return currentPos;
        }
    }
}
