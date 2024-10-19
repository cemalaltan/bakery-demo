using Business.Abstract;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
namespace Business.Concrete;

using System.Globalization;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;
using PdfFont = iText.Kernel.Font.PdfFont;
using PdfWriter = iText.Kernel.Pdf.PdfWriter;

public class GeneralAccountPdfManager : IGeneralAccountPdfService
{
    private string fontPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"Business/Fonts/Roboto-Regular.ttf");

    public byte[] GetGeneralAccountPdfByDate(DateTime date)
    {
        using var stream = new MemoryStream();
        using var writer = new PdfWriter(stream);
        using var pdf = new PdfDocument(writer);
        using var document = new Document(pdf);

        // Set page margins
        document.SetMargins(36, 36, 36, 36);

        if (!File.Exists(fontPath))
        {
            throw new FileNotFoundException("Font file not found.", fontPath);
        }

        PdfFont font = PdfFontFactory.CreateFont(fontPath, "CP1254", PdfFontFactory.EmbeddingStrategy.FORCE_NOT_EMBEDDED);
        document.SetFont(font);
        
        string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
        var dayOfWeek = date.ToString("dddd", new CultureInfo("tr-TR"));

        // Header section with improved styling
        document.Add(new Paragraph("ASLANOĞLU Fırın")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(24)
            .SetBold()
            .SetMarginBottom(10));
            
        document.Add(new Paragraph("Genel Hesap")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(18)
            .SetMarginBottom(5));
            
        document.Add(new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetMarginBottom(20));

        // Main container table
        var mainTable = new Table(3)
            .UseAllAvailableWidth()
            .SetBorder(Border.NO_BORDER);

        // Create tables with consistent styling
        var leftTable = CreateStyledTable("Giderler");
        var rightTable = CreateStyledTable("Gelirler");

        // Left side data
        AddTableRow(leftTable, "Geri Hamur", "100");
        AddTableRow(leftTable, "Kalan Ekmek", "1200");
        AddTableRow(leftTable, "Servis", "1200");
        AddTableRow(leftTable, "Dış Bayı", "130");
        AddTableRow(leftTable, "Fırın yemek", "140", true);
        AddTableRow(leftTable, "Gider", "300", true);
        AddTableRow(leftTable, "Pastane Gider", "300", true);
        AddTableRow(leftTable, "Veresiye", "300");
        AddTableRow(leftTable, "Yenen", "80");
        AddTableRow(leftTable, "Bayat", "40");

        // Right side data
        AddTableRow(rightTable, "İmalat", "1500");
        AddTableRow(rightTable, "Dünden kalan", "1200", true);
        AddTableRow(rightTable, "Tezgah Ekmek", "1200", true);
        AddTableRow(rightTable, "Devir", "3000");
        AddTableRow(rightTable, "Servis", "3000");
        AddTableRow(rightTable, "Tezgah", "3000");
        AddTableRow(rightTable, "Pastane", "1100");
        AddTableRow(rightTable, "Dış Bayi", "1300", true);
        AddTableRow(rightTable, "Genel Gider", "1200", true);
        AddTableRow(rightTable, "Toplam kasa", "1200", true);
        AddTableRow(rightTable, "Ciro", "1200");
        AddTableRow(rightTable, "Fiş", "1200");
        AddTableRow(rightTable, "Gider", "1200");

        // Add tables to main container with spacing
        mainTable.AddCell(new Cell().Add(leftTable)
            .SetBorder(Border.NO_BORDER)
            .SetPaddingRight(20));
            
        mainTable.AddCell(new Cell()
            .SetBorder(Border.NO_BORDER)
            .SetWidth(20)); // Spacer column
            
        mainTable.AddCell(new Cell().Add(rightTable)
            .SetBorder(Border.NO_BORDER)
            .SetPaddingLeft(20));

        document.Add(mainTable);
        document.Close();
        return stream.ToArray();
    }

    private Table CreateStyledTable(string title)
    {
        var table = new Table(2)
            .UseAllAvailableWidth()
            .SetHorizontalAlignment(HorizontalAlignment.CENTER);

        return table;
    }

    private void AddTableRow(Table table, string label, string value, bool addSpacerAfter = false)
    {
        table.AddCell(new Cell()
            .Add(new Paragraph(label))
            .SetPadding(5)
            .SetBorder(new SolidBorder(ColorConstants.GRAY, 0.5f)));
            
        table.AddCell(new Cell()
            .Add(new Paragraph(value))
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetPadding(5)
            .SetBorder(new SolidBorder(ColorConstants.GRAY, 0.5f)));

        if (addSpacerAfter)
        {
            table.AddCell(new Cell(1, 2)
                .SetHeight(20)
                .SetBorder(Border.NO_BORDER));
        }
    }
}