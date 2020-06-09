using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Person
    {
        public string name
        {
            get
            {
                return familyName + givenName;
            }
        }

        [JsonProperty]
        internal string familyName;

        [JsonProperty]
        internal string givenName;

        internal double attitude
        {
            get
            {
                return attitudeDetail.Sum(x => x.value);
            }
        }

        internal List<(string name, double value)> attitudeDetail
        {
            get
            {
                var rslt = new List<(string name, double value)>();
                rslt.Add((Mod.GetLocalString("BACKGROUBD_TO_ATTITUDE", this.family.background.name, GMData.inst.taishou.background.name),
                          family.background.relation[GMData.inst.taishou.background.name]));
                return rslt;
            }
        }


        internal Family family
        {
            get
            {
                return GMData.inst.families.Single(x => x.name == familyName);
            }
        }

        public Person(string name)
        {
            this.familyName = name;
            this.givenName = PersonName.EnumGiven()
                                       .OrderBy(a => Guid.NewGuid())
                                       .First(x => family.persons.All(y => y.givenName != x));
        }
    }
}
