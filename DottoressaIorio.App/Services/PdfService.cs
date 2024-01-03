using DinkToPdf;
using DinkToPdf.Contracts;
using DottoressaIorio.App.Models;

namespace DottoressaIorio.App.Services;

public class PdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter)
    {
        _converter = converter;
    }

    public byte[] GenerateTherapyPdf(Therapy therapy)
    {
        var htmlContent = $@"
        <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    h2 {{ color: #333; }}
                </style>
            </head>
            <body>
                <h2>{therapy.CreatedDate:dd/MM/yyyy}</h2>
                <p>{therapy.Description}</p>
            </body>
        </html>";

        var globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4
        };

        var objectSettings = new ObjectSettings
        {
            PagesCount = true,
            HtmlContent = htmlContent,
            WebSettings = { DefaultEncoding = "utf-8" }
        };

        var pdf = _converter.Convert(new HtmlToPdfDocument
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        });

        return pdf;
    }

    public static class FileUtil
    {
        public static async Task SaveAs(byte[] content, string fileName)
        {
            var memoryStream = new MemoryStream(content);
            var stream = new FileStream(fileName, FileMode.Create);
            await memoryStream.CopyToAsync(stream);
            memoryStream.Close();
            stream.Close();
        }
    }
}