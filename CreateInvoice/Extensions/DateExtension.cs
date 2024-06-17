namespace CreateInvoice.Extensions
{
    public static class DateExtension
    {
        public static string ToVietnameseDate(this DateTime date)
        {
            return $"Ngày {date.Day} tháng {date.Month} năm {date.Year}";
        }
    }
}
