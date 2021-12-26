using System;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validations;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Test
{
    public class ProductUnitTest
    {
        [Fact(DisplayName = "Create product with parameters valid")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product", "Description product", 9.99m, 99, "product image");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Valid negative id")]
        public void CreateProduct_NegativeIdValeu_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product", "Description product", 9.99m, 99, "product image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Throw exception with field name is short")]
        public void CreateProduct_ShortNameValeu_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Description product", 9.99m, 99, "product image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. too short, minimum 3 caracters");
        }

        [Fact(DisplayName = "Allow create product with image null")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product", "Description product", 9.99m, 99, null);
            action.Should()
                .NotThrow<DomainExceptionValidation>();
                
        }

        [Fact(DisplayName = "Allow create product with image empty")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product", "Description product", 9.99m, 99, string.Empty);
            action.Should()
                .NotThrow<DomainExceptionValidation>();

        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_WithInvalidPriceValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product", "Description product", value, 99, string.Empty);
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_WithInvalidStockValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product", "Description product", 9.99m, value, string.Empty);
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }

    }
}
