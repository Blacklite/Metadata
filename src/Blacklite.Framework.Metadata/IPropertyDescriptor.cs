


using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{



    public interface IPropertyDescriptor
    {
        IEnumerable<IPropertyDescriber> Describe(Type type);
    }
}
