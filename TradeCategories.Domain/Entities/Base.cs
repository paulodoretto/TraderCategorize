using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TradeCategories.Domain
{
    public abstract class Base
    {

        public long Id { get; set; }

        
        internal List<string> _errors;

        [Description("ignore")]
        public IReadOnlyCollection<string> Errors => _errors;

        public abstract bool Validate();

    }
}
