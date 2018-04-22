namespace DependecyResolver
{
    using Exporter.Implementations;
    using Exporter.Implementations.Logger;
    using Exporter.Interfaces;
    using Ninject;

    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<ILogger>().To<NLogLogger>().WithConstructorArgument("className", "Program");
            kernel.Bind<IDataProvider<string>>().To<TxtFileDataProvider>().WithConstructorArgument("filePath", @"data.txt");
            kernel.Bind<IXmlCreater<string>>().To<XmlCreaterFromUri>();
        }
    }
}
