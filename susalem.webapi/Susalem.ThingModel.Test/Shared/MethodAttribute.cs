using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.ThingModel.Test.Shared
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class MethodAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public MethodAttribute(string name, string description = "")
        {
            Name = name;
            Description = description;
        }
    }
}
