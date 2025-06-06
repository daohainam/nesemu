namespace mini_6502;
internal class PanicException : Exception
{
    public PanicException()
    {
    }

    public PanicException(string? message) : base(message)
    {
    }

    public PanicException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}