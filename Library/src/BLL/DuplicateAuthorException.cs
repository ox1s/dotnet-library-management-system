namespace Library.BLL;

public class DuplicateAuthorException : Exception
{
    public DuplicateAuthorException(string message) : base(message) { }
}