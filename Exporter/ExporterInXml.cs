namespace Exporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Exporter.Exceptions;
    using Exporter.Interfaces;

    /// <summary>
    /// Exporting Data to XML
    /// </summary>
    /// <typeparam name="T">
    /// Data type
    /// </typeparam>
    public class ExporterInXml<T>
    {
        #region private fields
        private IXmlCreater<T> _xmlCreater;
        private ILogger _logger;
        private string _rootName = "urlAddresses";
        #endregion private fields

        /// <summary>
        /// Initialization of the exporter.
        /// </summary>
        /// <param name="xmlCreater">
        /// Data converter in xml.
        /// </param>
        /// <param name="logger">
        /// Logger.
        /// </param>
        public ExporterInXml(IXmlCreater<T> xmlCreater, ILogger logger)
        {
            _xmlCreater = xmlCreater ?? throw new ArgumentNullException(nameof(xmlCreater));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// The name of the root node.
        /// </summary>
        public string RootName
        {
            get
            {
                return _rootName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(RootName)} is incorrect. The name must not be empty or null.", nameof(RootName));
                }
            }
        }

        /// <summary>
        /// Convert to a XML document.
        /// </summary>
        /// <param name="dataProvider">
        /// Data source.
        /// </param>
        /// <returns>
        /// XML Document
        /// </returns>
        public XDocument ExportToXml(IDataProvider<T> dataProvider)
        {
            if (ReferenceEquals(dataProvider, null))
            {
                throw new ArgumentNullException(nameof(dataProvider));
            }

            var dataSet = dataProvider.GetData();
            var elements = new List<XElement>();

            foreach (var item in dataSet.Select((Value, Index) => new { Value, Index }))
            {
                try
                {
                    var element = _xmlCreater.GetElement(item.Value);
                    elements.Add(element);
                }
                catch (UnsupportedPatternUriException e)
                {
                    _logger.Warn($"Unsupport Pattern, String number {item.Index}: {item.Value}", e);
                }
            }

            var document = new XElement(_rootName, elements);

            XDocument xdoc = new XDocument();
            xdoc.Add(document);
            return xdoc;
        }
    }
}