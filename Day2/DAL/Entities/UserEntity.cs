using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public enum UserGender
    {
        male = 0,
        female = 1
    }
    [Serializable]
    public struct VisaRecord : IComparable
    {
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override bool Equals(object obj)
        {
            if (!ReferenceEquals(obj, null))
            {
                VisaRecord visa;
                if (obj is VisaRecord)
                {
                    visa = (VisaRecord)obj;
                    if (this.Country.Equals(visa.Country) && (this.StartDate.Equals(visa.StartDate)) && (this.EndDate.Equals(visa.EndDate))){
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Country.GetHashCode() ^ StartDate.GetHashCode() ^ EndDate.GetHashCode();
        }
        public int CompareTo(object obj)
        {
            if (!ReferenceEquals(obj, null))
            {
                VisaRecord comparedVisa;
                if(obj is VisaRecord)
                {
                    comparedVisa = (VisaRecord)obj;
                    if (this.Equals(comparedVisa))
                    {
                        return 0;
                    }
                    return this.Country.Length < comparedVisa.Country.Length ? -1 : 1;
                }
            }
            return -1;
        }
    }
    [Serializable]
    public class UserEntity : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserGender Gender { get; set; }
        public VisaRecord[] VisaRecords { get; set; }

        public override bool Equals(object obj)
        {
            if (!ReferenceEquals(obj, null))
            {
                UserEntity user = obj as UserEntity;
                if (!ReferenceEquals(user,null))
                {
                    if (FirstName.Equals(user.FirstName) && (LastName.Equals(user.LastName)) && (Gender.Equals(user.Gender)))
                    {
                        HashSet<VisaRecord> userHashSet = new HashSet<VisaRecord>(VisaRecords);
                        if (userHashSet.SetEquals(new HashSet<VisaRecord>(user.VisaRecords)))
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            List<VisaRecord> list = new List<VisaRecord>(VisaRecords);
            list.Sort();
            hashCode = Id ^ FirstName.Length ^ LastName.Length;
            foreach(var visa in list)
            {
                hashCode = hashCode ^ visa.GetHashCode();
            }
            return hashCode;
        }
    }
}
