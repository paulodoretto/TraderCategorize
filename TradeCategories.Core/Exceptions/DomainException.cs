using System;
using System.Collections.Generic;
using System.Text;

namespace TradeCategories.Core.Exceptions
{
    public class DomainException : Exception
    {

        internal List<string> _erros;

        public IReadOnlyCollection<string> Erros => _erros;

        public DomainException()
        { }

        public DomainException(string message, List<string> errors) : base(message)
        {
            _erros = errors;
        }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
