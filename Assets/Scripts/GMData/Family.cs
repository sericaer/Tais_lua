using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaisEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Family
    {
        [JsonProperty]
        internal string name;

        [JsonProperty]
        internal List<Person> persons = new List<Person>();

        internal BackgroundDef.Interface background
        {
            get
            {
                return BackgroundDef.Find(_background);
            }
        }

        internal double attitude
        {
            get
            {
                return persons.Sum(x => x.attitude) / persons.Count();
            }
        }

        internal static Family Generate(string background)
        {
            var family = new Family(PersonName.EnumFamily()
                                              .OrderBy(a => Guid.NewGuid())
                                              .First(x => GMData.inst.families.All(y => y.name != x)),
                                    background);
            return family;
        }

        public Family()
        {

        }

        private Family(string name, string background)
        {
            GMData.inst.families.Add(this);

            this.name = name;
            this._background = background;

            for (int i=0; i<10; i++)
            {
                persons.Add(new Person(name));
            }
        }

        [JsonProperty]
        private string _background;
    }
}
