namespace Library.BLL.Exceptions;

public class DuplicateBookException : Exception
{
    public DuplicateBookException(string message) : base(message) { }
}