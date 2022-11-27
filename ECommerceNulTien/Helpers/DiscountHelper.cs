namespace ECommerceNulTien.Helpers
{
    public static class DiscountHelper
    {
        public static double Calculate(double sum, string phoneNumber)
        {
            if (!IsDiscountActive())
                return 0;
            var lastDigit = Convert.ToInt32(phoneNumber.Last());

            if (lastDigit == 0)
                return GetDiscount(sum, 30);

            return lastDigit % 2 == 0 ? GetDiscount(sum, 20) : GetDiscount(sum, 10);
        }

        private static bool IsDiscountActive()
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var from = new TimeSpan(16, 0, 0);
            var to = new TimeSpan(17, 0, 0);
            return from <= currentTime && to >= currentTime;
        }

        private static double GetDiscount(double sum, int discount)
        {
            return sum * discount / 100;
        }
    }
}
