namespace Library.BLL;

public class ImpossibleDateException : Exception
{
    public ImpossibleDateException(string message) : base(message) { }

}
