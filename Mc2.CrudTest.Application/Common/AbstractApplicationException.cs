namespace Mc2.CrudTest.Application.Common;

public abstract class AbstractApplicationException : Exception
{
    public AbstractApplicationException(string message) : base(message)
    {
    }
}