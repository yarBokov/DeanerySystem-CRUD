namespace DeanerySystem.Extensions
{
    public static class StringExtensions
    {
        public static string ToMarkName(this string markNumber)
        {
            if (markNumber != null)
            {
                int mark = Int32.Parse(markNumber);
                switch(mark)
                {
                    case 2:return "Двойки";
                    case 3:return "Тройки";
                    case 4:return "Четверки";
                    case 5:return "Пятерки";
                    default:return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
