using System.Collections;
using System.Linq;

using UnityEngine;

namespace TaisEngine
{
    internal class Taishou
    {
        internal string name;
        internal int age;

        internal BackgroundDef background
        {
            get
            {
                return Mod.EnumerateBackground().Single(x=>x.name == _background);
            }
        }

        public Taishou(string name, int age, string background)
        {
            this.name = name;
            this.age = age;
            this._background = background;
        }

        private string _background;
    }
}
