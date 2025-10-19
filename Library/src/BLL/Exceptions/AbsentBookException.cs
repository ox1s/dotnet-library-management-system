namespace Library.BLL.Exceptions;

public class AbsentBookException : Exception
{
    public AbsentBookException(string message) : base(message) { }

}
