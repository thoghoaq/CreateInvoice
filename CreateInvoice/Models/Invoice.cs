using CreateInvoice.Enums;
using CreateInvoice.Extensions;

namespace CreateInvoice.Models
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? Note { get; set; }

        public List<InvoiceItem> Items { get; set; } = [];
        public decimal ShippingFee { get; set; }
        public decimal Total => Items.Sum(x => x.Total) + ShippingFee;
    }

    public class InvoiceItem
    {
        public string Name { get; set; } = null!;
        public string Dvt { get; set; } = null!;
        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
        public string TotalM2 => $"{Math.Round(UnitConverter.Convert(Width * Height, Unit.CM2, Dvt.GetUnitEnum()) * Quantity, 2)}{Dvt}";
        public decimal Price { get; set; }
        public decimal Total => Math.Round((decimal)(UnitConverter.Convert(Width * Height, Unit.CM2, Dvt.GetUnitEnum())) * Quantity * Price);
    }
}
