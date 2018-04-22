namespace ConsolePL
{
    using System;
    using System.IO;
    using System.Text;
    using DependecyResolver;
    using Exporter;
    using Exporter.Interfaces;
    using Ninject;

    public class Program
    {
        private static readonly IKernel Resolver;
        private static readonly string DataPath = @"data.txt";

        static Program()
        {
            Resolver = new StandardKernel();
            Resolver.ConfigurateResolver();
        }

        public static void Main(string[] args)
        {
            var exporter = new ExporterInXml<string>(Resolver.Get<IXmlCreater<string>>(), Resolver.Get<ILogger>());
            var dataProvider = Resolver.Get<IDataProvider<string>>();
            
            CreateSourse();

            var xml = exporter.ExportToXml(dataProvider);
            xml.Save("xml.xml");
            Console.WriteLine(xml.ToString());

            Console.ReadLine();
        }

        private static void CreateSourse()
        {
            var dataSet = new string[]
            {
                @"https://github.com/AnzhelikaKravchuk?tab=repositories",
                @"https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU",
                @"https://github.com/",
                @"f",
                @"https://habrahabr.ru/company/it-grad/blog/341486/"
            };

            using (var stream = new StreamWriter(DataPath, false, Encoding.UTF8))
            {
                foreach (var s in dataSet)
                {
                    stream.WriteLine(s);
                }
            }
        }
    }
}
