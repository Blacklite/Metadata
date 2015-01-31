using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class Order : SimpleMetadatum<int>
    {
        public Order(int value) : base(value) { }
    }
}