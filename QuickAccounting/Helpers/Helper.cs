namespace QuickAccounting.Helpers
{
    public class Helper
    {
        public static string ConvertCageNumber(string cageNumber)
        {
            int c;
            if(int.TryParse(cageNumber, out c))
            {
                return c.ToString();
            }

            return cageNumber;
        }
    }
}
