namespace BLL;

public class DuplicateBookException : Exception
{
    public DuplicateBookException(string message) : base(message) { }
}