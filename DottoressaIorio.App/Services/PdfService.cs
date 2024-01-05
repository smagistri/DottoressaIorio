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
                    body {{ font-family: Calibri, sans-serif; }}
                    .content h4 {{ text-align: right; }}
                </style>
            </head>
            <body>
                <div class=""content""> 
                    <p>{therapy.Description.Replace("\n", "<br />")}</p>
                </div>  
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
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontName = "Calibri", FontSize = 12, Right = $"Data Terapia: {therapy.CreatedDate:dd/MM/yyyy}", Spacing = 15, HtmUrl = "Services/PdfHeader.html" },
            FooterSettings = { FontName = "Calibri", FontSize = 8, Right = "Pagina [page] di [toPage]", HtmUrl = "Services/PdfFooter.html", Spacing = 10}
        };

        var pdf = _converter.Convert(new HtmlToPdfDocument
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        });

        return pdf;
    }
}