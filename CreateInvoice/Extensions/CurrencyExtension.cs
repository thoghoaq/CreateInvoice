using System.Globalization;

namespace CreateInvoice.Helpers
{
    public static class CurrencyExtension
    {
        public static string ToCurrency(this decimal value)
        {
            return value.ToString("N0", new CultureInfo("vi-VN"));
        }

        public static string ToVietnameseCurrency(this decimal amount)
        {
            if (amount == 0) return "KHÔNG ĐỒNG";
            string[] unitNumbers = new string[] { "KHÔNG", "MỘT", "HAI", "BA", "BỐN", "NĂM", "SÁU", "BẢY", "TÁM", "CHÍN" };
            string[] placeValues = new string[] { "", "NGHÌN", "TRIỆU", "TỶ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = amount.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "MỘT " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "LĂM " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "LẺ " + result;
                        if (tens == 1) result = "MƯỜI " + result;
                        if (tens > 1) result = unitNumbers[tens] + " MƯƠI " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " TRĂM " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "ÂM " + result;
            return result + " ĐỒNG";
        }
    }
}
