using System;
using System.Collections;
using System.Collections.Generic;
using CleanArchMvc.Domain.Validations;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {        
        public string Name { get; private set; }
        public ICollection<Product> Products { get; set; }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidateDomain(name);
        }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required!");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name. too short, minimum 3 caracters");

            Name = name;
        }
    }
}
