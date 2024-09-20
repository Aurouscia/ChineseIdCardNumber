namespace Aurouscia.ChineseIdCardNumber
{
    public class ChnIdInfo(
        string provinceName, string cityName, string areaName,
        DateTime birthday, bool isMale)
    {
        public string ProvinceName { get; set; } = provinceName;
        public string CityName { get; set; } = cityName;
        public string AreaName { get; set; } = areaName;
        public DateTime Birthday { get; set; } = birthday;
        public bool IsMale { get; set; } = isMale;
    }
}