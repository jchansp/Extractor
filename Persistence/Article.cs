//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public System.Guid Id { get; set; }
        public string Link { get; set; }
        public byte[] Picture { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Title { get; set; }
        public Nullable<byte> Units { get; set; }
    }
}
