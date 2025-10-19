namespace Library.BLL.Exceptions;

public class DuplicateAuthorException : Exception
{
    public DuplicateAuthorException(string message) : base(message) { }
}