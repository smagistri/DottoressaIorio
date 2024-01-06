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
                </style>
            </head>
            <body>
                <h4>{therapy.Patient.Title} {therapy.Patient.FirstName} {therapy.Patient.LastName},</h4>
                <p>{therapy.Description.Replace("\n", "<br />")}</p>
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
            HeaderSettings = { FontName = "Calibri", FontSize = 12, Spacing = 15,
                Right = $"Data terapia: {therapy.CreatedDate:dd/MM/yyyy}",
                HtmUrl = "Services/PdfHeader.html"
            },
            FooterSettings = { FontName = "Calibri", FontSize = 8, Spacing = 10,
                Left = $"Terapia Paziente: {therapy.CreatedDate:dd/MM/yyyy} - {therapy.Patient.Title} {therapy.Patient.FirstName} {therapy.Patient.LastName}",
                Right = "Pagina [page] di [toPage]", 
                HtmUrl = "Services/PdfFooter.html",  }
        };

        var pdf = _converter.Convert(new HtmlToPdfDocument
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        });

        return pdf;
    }
}