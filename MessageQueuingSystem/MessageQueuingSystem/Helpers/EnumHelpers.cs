using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.Helpers
{
    public enum Stage
    {
        Raw=0,
        Validated=1,
        Categorized=2,
        Processed=3,
        Failed=4,
        Sent=5
    }
}
