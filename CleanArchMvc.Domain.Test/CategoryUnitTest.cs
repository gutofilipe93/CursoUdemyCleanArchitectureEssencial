using System;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validations;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Test
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Create category with valid state")]        
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name test unit");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Valid negative id value")]
        public void CreateCategory_NegativeIdValeu_DomainExceptionValidation()
        {
            Action action = () => new Category(-1, "Category Name test unit");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Valid short name valeu")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. too short, minimum 3 caracters");
        }

        [Fact(DisplayName = "Valid name is required")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required!");
        }

        [Fact(DisplayName = "Valid name cannot be null")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}
