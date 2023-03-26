namespace PoeData.Specifications;

/// <summary>
/// Exception thrown when wrong row offset is provided when trying to get value of <see cref="Unknown{T}"/>.
/// </summary>
public class WrongOffsetException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WrongOffsetException"/> class.
    /// </summary>
    public WrongOffsetException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WrongOffsetException"/> class.
    /// </summary>
    /// <param name="message">exception message.</param>
    public WrongOffsetException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WrongOffsetException"/> class.
    /// </summary>
    /// <param name="message">exception message.</param>
    /// <param name="innerException">inner exception.</param>
    public WrongOffsetException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}