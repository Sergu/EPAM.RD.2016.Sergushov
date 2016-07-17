using System;

namespace Attributes
{
    // Should be applied to assembly only.
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class InstantiateAdvancedUserAttribute : InstantiateUserAttribute
    {
        public int? ExternalId { get; set; }


        public InstantiateAdvancedUserAttribute(int id, string firstName, string lastName) : base(id, firstName, lastName) { }
        public InstantiateAdvancedUserAttribute(int id,string firstName,string lastName ,int externalId) : this(id,firstName,lastName)
        {
            ExternalId = externalId;
        }
        public InstantiateAdvancedUserAttribute(string firstName,string lastName) : base(firstName, lastName) { }
    }
}
