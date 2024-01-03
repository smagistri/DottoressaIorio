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
                    h2 {{ color: #333; }}
                    .header .h1 {{ font-family: Monotype Corsiva, Times, Serif; font-size: 28px; margin: 0px }}
                    .header p {{margin: 0px}}
                    .content h4 {{ text-align: right; }}
                </style>
            </head>
            <body>
               <div class=""header"">
                 <p class=""h1"">Dott.ssa Alessandra Iorio</h1>
                 <p>Medico Chirurgo</p>
                 <p>Specialista in Dermatologia e Venereologia</p>
                 <p>Dirigente Medico Istituto San Gallicano – Roma I.R.C.S.S.</p>
                 <p>Cell: 320.1871136</p>
                 <p>alessandraiorio8@gmail.com</p>                
                </div>  
                <div class=""content"">
                    <h4>{therapy.CreatedDate:dd/MM/yyyy}</h4>
                    <p>{therapy.Description}</p>
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