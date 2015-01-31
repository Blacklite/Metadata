﻿using Blacklite.Framework.Metadata.Metadatums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blacklite.Framework.Metadata.Modeling.Metadatums
{
    public class EditFormat : IMetadatum
    {
        public EditFormat(string format, bool htmlEncode = false)
        {
            Format = format;
            HtmlEncode = htmlEncode;
        }

        public string Format { get; }

        public bool HtmlEncode { get; }
    }
}