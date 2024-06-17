using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CreateInvoice.Features
{
    public class PdfService
    {
        public byte[] GeneratePdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.DefaultTextStyle(x => x.FontFamily("Times New Roman"));

                    // Header
                    page.Header().PaddingBottom(15).Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.Spacing(5);
                            row.ConstantItem(50).Height(50).Placeholder(); // Logo or Placeholder
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("RÈM QUANG HUY").FontSize(14).Bold();
                                col.Item().Text("SDT: 0334191022");
                                col.Item().Text("số 8 Nghi Tàm, Hà Nội, 14 Hùng Vương, P1, Q10");
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Spacing(5);
                                col.Item().Text("ĐƠN ĐẶT HÀNG").FontSize(20).AlignRight().Bold();
                                col.Item().Text("Ngày 12 tháng 06 năm 2024").AlignRight();
                                col.Item().Text("SỐ HÓA ĐƠN: 000776").AlignRight().FontSize(14).Bold();
                            });
                        });

                    });

                    // Content
                    page.Content().Column(column =>
                    {
                        // Customer Info
                        column.Item().Border(1).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.Cell().Element(CellStyle).Text("KHÁCH HÀNG").Bold();
                            table.Cell().Element(CellStyle).Text("chị Nguyễn Hòa");

                            table.Cell().Element(CellStyle).Text("ĐỊA CHỈ").Bold();
                            table.Cell().Element(CellStyle).Text("106/35 ĐƯỜNG 109 PHƯỚC LONG B, THỦ ĐỨC, TPHCM");

                            table.Cell().Element(CellStyle).Text("SDT").Bold();
                            table.Cell().Element(CellStyle).Text("0372 015192");

                            table.Cell().Element(CellStyle).Text("GHI CHÚ").Bold();
                            table.Cell().Element(CellStyle).Text("2 BỘ RÈM CUỐN MÀU NÂU");

                            static IContainer CellStyle(IContainer container) => container.Border(1).Padding(5).AlignLeft();
                        });

                        // Item Details
                        column.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);
                                columns.RelativeColumn();
                                columns.ConstantColumn(30);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(50);
                                columns.ConstantColumn(50);
                                columns.ConstantColumn(70);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("STT").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("TÊN HÀNG HÓA").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("DVT").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("RỘNG").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("CAO").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("SỐ BỘ").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("TỔNG M2").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("ĐƠN GIÁ").Bold().FontSize(10);
                                header.Cell().Element(CellStyle).Text("THÀNH TIỀN").Bold().FontSize(10);

                                static IContainer CellStyle(IContainer container) => container.Border(1).PaddingVertical(10).PaddingHorizontal(1).AlignCenter();
                            });

                            // Item Rows
                            table.Cell().Element(CellStyle).Text("1");
                            table.Cell().Element(CellStyle).Text("TX05 asjdhkajfdhkafksdfads");
                            table.Cell().Element(CellStyle).Text("M2");
                            table.Cell().Element(CellStyle).Text("105");
                            table.Cell().Element(CellStyle).Text("180");
                            table.Cell().Element(CellStyle).Text("2");
                            table.Cell().Element(CellStyle).Text("3.78M2");
                            table.Cell().Element(CellStyle).Text("195.000");
                            table.Cell().Element(CellStyle).Text("737.000");

                            table.Cell().Element(CellStyle).Text("2");
                            table.Cell().Element(CellStyle).Text("PHÍ SHIP");
                            table.Cell().Element(CellStyle).Text("");
                            table.Cell().Element(CellStyle).Text("");
                            table.Cell().Element(CellStyle).Text("");
                            table.Cell().Element(CellStyle).Text("");
                            table.Cell().Element(CellStyle).Text("");
                            table.Cell().Element(CellStyle).Text("30.000");
                            table.Cell().Element(CellStyle).Text("30.000");

                            static IContainer CellStyle(IContainer container) => container.Border(1).PaddingHorizontal(1).PaddingVertical(5).AlignCenter();
                        });
                        column.Item().PaddingBottom(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(70);
                            });

                            table.Cell().Element(CellStyle).Text("CỘNG TIỀN HÀNG:").Bold();
                            table.Cell().Element(CellStyle).Text("767.000");

                            static IContainer CellStyle(IContainer container) => container.Border(1).Padding(5).AlignCenter();
                        });

                        column.Item().Text("(Số tiền viết bằng chữ: BẢY TRĂM SÁU MƯƠI BẢY NGHÌN ĐỒNG)").Bold().Italic().AlignRight();

                        // Payment Info
                        column.Item().PaddingVertical(15).Column(innerColumn =>
                        {
                            innerColumn.Spacing(2);
                            innerColumn.Item().Text("HÌNH THỨC THANH TOÁN:").Bold().FontColor(Color.FromHex("#FF0000"));
                            innerColumn.Item().Element(container => container.PaddingVertical(5)).Text("Quý khách vui lòng chuyển khoản số tiền là: 767.000đ").Bold().Italic();
                            innerColumn.Item().Row(row =>
                            {
                                row.Spacing(2);
                                row.AutoItem().Text("Đơn giá này");
                                row.AutoItem().Text("ĐÃ").Bold();
                                row.AutoItem().Text("bao gồm đầy đủ phụ kiện, chi phí vận chuyển, chưa bao gồm thuế VAT.");
                            });
                            innerColumn.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem().Text("Hình thức thanh toán:");
                                row.AutoItem().Text("chuyển khoản (NỘI DUNG: TÊN KH + SDT)").Bold();
                            });
                            innerColumn.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem().Text("Thông tin chuyển khoản:");
                                row.AutoItem().Column(col =>
                                {
                                    col.Item().Text("NH TECHCOMBANK").Bold();
                                    col.Item().Text("STK: 9145 686868").Bold();
                                    col.Item().Text("CTK: NGUYEN THI NGOC ANH").Bold();
                                });
                            });
                        });

                        // Signatures
                        column.Item().PaddingVertical(10).Row(row =>
                        {
                            row.ConstantItem(50).Height(50).Placeholder();
                            row.RelativeItem().AlignCenter().Column(col =>
                            {
                                col.Item().Text("NV Bán Hàng");
                                col.Item().Text("(Ký, họ tên)").Italic().AlignCenter();
                            });
                            row.RelativeItem().AlignCenter().Column(col =>
                            {
                                col.Item().Text("Khách Hàng");
                                col.Item().Text("(Ký, họ tên)").Italic().AlignCenter();
                            });
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Row(row =>
                    {
                        row.Spacing(5);
                        row.ConstantItem(180).AlignMiddle().Text("Sản phẩm được bảo hành 12 tháng nếu lỗi từ nhà sản xuất.").Bold().Italic().FontSize(14);
                        row.RelativeItem().Border(1).Padding(5).AlignMiddle().Column(col =>
                        {
                            col.Item().AlignCenter().Text("HOTLINE/ZALO: 0101010101").Bold();
                            col.Item().AlignMiddle().PaddingVertical(5).Row(innerRow =>
                            {
                                innerRow.Spacing(5);
                                innerRow.ConstantItem(60).AlignMiddle().Text("Quý khách vui lòng");
                                innerRow.RelativeItem().Column(innerColumn =>
                                {
                                    innerColumn.Item().Text("QUAY VIDEO MỞ HÀNG").Bold().FontSize(16).FontColor(Color.FromHex("#FF0000")).AlignCenter();
                                    innerColumn.Item().Text("Để nhận được hỗ trợ tốt nhất khi gặp sự cố.").AlignCenter();
                                    innerColumn.Item().Text("Xin cảm ơn!").AlignCenter();
                                });
                            });
                        });
                    });
                });
            }).GeneratePdf(stream);

            return stream.ToArray();
        }
    }
}
