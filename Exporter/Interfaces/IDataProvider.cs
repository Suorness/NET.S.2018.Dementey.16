namespace Exporter.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface describing the provision of data.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public interface IDataProvider<T>
    {
        /// <summary>
        /// Provision of data.
        /// </summary>
        /// <returns>
        /// Data set.
        /// </returns>
        IEnumerable<T> GetData();
    }
}
