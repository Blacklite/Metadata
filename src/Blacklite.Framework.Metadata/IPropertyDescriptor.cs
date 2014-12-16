using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Blacklite.Framework.Metadata
{
    public interface IPropertyDescriptor
    {
        IEnumerable<IPropertyDescriber> Describe(Type type);
    }
}
