using System;
namespace CleanArchMvc.Domain.Validations
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error)
        {
        }

        public static void When(bool hasError, string erro)
        {
            if (hasError)
                throw new DomainExceptionValidation(erro);
        }
    }
}
