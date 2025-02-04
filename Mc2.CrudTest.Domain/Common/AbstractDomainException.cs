namespace Mc2.CrudTest.Domain.Common;

public class AbstractDomainException : Exception
{
    public AbstractDomainException(string message) : base(message)
    {
    }
}