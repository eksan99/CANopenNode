
namespace libEDSsharp
{
    public static class ExporterFactory
    {
        public enum Exporter
        {
            SOURCE_CANOPENNODE_V4 = 0,
            SOURCE_CANOPENNODE_LEGACY = 1,
            DOCUMENT_HTML,
            DOCUMENT_MD,
        }

        public static IExporter getExporter(Exporter ex , string fileName, EDSsharp eds, string gitVersion)
        {
            IExporter exporter;

            switch (ex)
            {
                default:
                case Exporter.SOURCE_CANOPENNODE_V4:
                    exporter = new CanOpenNodeExporter_V4(fileName, eds, gitVersion);
                    break;
                case Exporter.SOURCE_CANOPENNODE_LEGACY:
                    exporter = new CanOpenNodeExporter(fileName, eds, gitVersion);
                    break;
                case Exporter.DOCUMENT_HTML:
                    exporter = new DocumentationHTMLExporter(fileName, eds, gitVersion);
                    break;
                case Exporter.DOCUMENT_MD:
                    exporter = new DocumentationMDExporter(fileName, eds, gitVersion);
                    break;
            }


            return exporter;
        }
    }
}
