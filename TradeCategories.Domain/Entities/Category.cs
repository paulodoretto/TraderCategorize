using System;
using System.Collections.Generic;
using System.Text;
using TradeCategories.Core.Exceptions;
using TradeCategories.Domain.Enums;
using TradeCategories.Domain.Validators;

namespace TradeCategories.Domain
{
    public class Category : Base
    {
        protected Category() { }

        public Category(string name, decimal valueInitial, decimal valueFinal, ESectorClient sectorClient)
        {
            Name = name;
            ValueInitial = valueInitial;
            ValueFinal = valueFinal;
            SectorClient = sectorClient;
            _errors = new List<string>();

            Validate();
        }

        public string Name { get; private set; }

        public decimal ValueInitial { get; private set; }

        public decimal ValueFinal { get; private set; }

        public ESectorClient SectorClient { get; private set; }

        public override bool Validate()
        {
            var validator = new CategoryValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                };

                throw new DomainException("Alguns campos estão inválidos, por favor revise as informações.", _errors);
            }

            return true;
        }

        public void ChangeName(string name)
        {
            Name = name;
            Validate();
        }

        public void ChangeValueInitial(decimal valueInitial)
        {
            ValueInitial = valueInitial;
            Validate();
        }

        public void ChangeValueFinal(decimal valueFinal)
        {
            ValueFinal = valueFinal;
            Validate();
        }

        public void ChangeSectorClient(ESectorClient sectorClient)
        {
            SectorClient = sectorClient;
            Validate();
        }

    }
}
