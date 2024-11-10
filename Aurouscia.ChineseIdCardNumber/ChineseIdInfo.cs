namespace Aurouscia.ChineseIdCardNumber
{
    public class ChineseIdInfo(
        string? provinceName, string? cityName, string? areaName,
        DateTime birthday, bool isMale)
    {
        public string? ProvinceName { get; set; } = provinceName;
        public string? CityName { get; set; } = cityName;
        public string? AreaName { get; set; } = areaName;
        public DateTime Birthday { get; set; } = birthday;
        public bool IsMale { get; set; } = isMale;
        public int GetAge(DateTime? at = null)
        {
            DateTime now = at ?? DateTime.Now;
            var age = now.Year - Birthday.Year;
            if (Birthday.Month > now.Month)
                age -= 1;
            else if (Birthday.Month == now.Month)
            {
                if (Birthday.Day > now.Day)
                    age -= 1;
            }
            return age;
        }
    }
}