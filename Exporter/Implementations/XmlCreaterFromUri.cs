namespace Exporter.Implementations
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;
    using Exporter.Exceptions;
    using Exporter.Interfaces;

    /// <summary>
    /// A class capable of creating XML.
    /// </summary>
    public class XmlCreaterFromUri : IXmlCreater<string>
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
        public XElement GetElement(string data)
        {
            var uri = GetUri(data);
            var segments = uri.Segments;
            var queryParams = HttpUtility.ParseQueryString(uri.Query);

            var rootElement = new XElement("urlAddress");
            var hostElement = new XElement("host");
            var uriElements = new XElement("uri");
            var parametrsElement = new XElement("parameters");
            hostElement.Add(new XAttribute("name", uri.Host));

            foreach (var segment in segments)
            {
                var segmentData = segment.Trim('/', ' ');
                if (!string.IsNullOrWhiteSpace(segmentData))
                {
                    uriElements.Add(new XElement("segment", segmentData));
                }
            }

            foreach (var key in queryParams.AllKeys)
            {
                var parametr = new XElement("parametr");
                parametr.Add(new XAttribute("value", queryParams[key]));
                parametr.Add(new XAttribute("key", key));
                parametrsElement.Add(parametr);
            }

            rootElement.Add(hostElement);

            if (uriElements.Elements().Count() > 0)
            {
                rootElement.Add(uriElements);
            }

            if (parametrsElement.Elements().Count() > 0)
            {
                rootElement.Add(parametrsElement);
            }

            return rootElement;
        }

        private Uri GetUri(string data)
        {
            try
            {
                var uri = new Uri(data);
                return uri;
            }
            catch (UriFormatException e)
            {
                throw new UnsupportedPatternUriException(data, e);
            }
        }
    }
}
