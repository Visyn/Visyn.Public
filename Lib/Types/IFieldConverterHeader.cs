namespace Visyn.Types
{
    /// <summary>
    /// Interface IFieldConverterHeader
    /// </summary>
    public interface IFieldConverterHeader
    {
        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>The delimiter.</value>
        string Delimiter { get; set; }

        /// <summary>
        /// Gets the header text.
        /// </summary>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>System.String.</returns>
        string GetHeaderText(string delimiter);
    }
}