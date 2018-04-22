namespace Exporter.Interfaces
{
    using System.Xml.Linq;

    /// <summary>
    /// An interface describing an entity capable of creating XML.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IXmlCreater<T>
    {
        /// <summary>
        /// Creates an element of XML.
        /// </summary>
        /// <param name="data">
        /// Data for the node.
        /// </param>
        /// <returns>
        /// XML node.
        /// </returns>
        XElement GetElement(T data);
    }
}
