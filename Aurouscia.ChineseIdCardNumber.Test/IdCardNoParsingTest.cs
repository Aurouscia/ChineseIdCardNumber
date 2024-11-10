using Aurouscia.ChineseIdCardNumber.Etc;

namespace Aurouscia.ChineseIdCardNumber.Test
{
    /// <summary>
    /// 注：单元测试的身份证号除作者自己的外均为使用工具随机生成，如有雷同纯属巧合
    /// </summary>
    [TestClass]
    public class IdCardNoParsingTest
    {
        [TestMethod]
        [DataRow(
            "513233202003167719", "2020-03-16", true,
            "四川省-阿坝藏族羌族自治州-红原县")]
        [DataRow(
            "120101197606249179", "1976-06-24", true,
            "天津市-市辖区-和平区")]
        [DataRow(
            "110101195902252005", "1959-02-25", false,
            "北京市-市辖区-东城区")]
        [DataRow(
            "429004201008149460", "2010-08-14", false,
            "湖北省-直辖区县-仙桃市")]
        [DataRow(
            "15042119600409546X", "1960-04-09", false,
            "内蒙古自治区-赤峰市-阿鲁科尔沁旗")]
        [DataRow(
            "42010220030106081X", "2003-01-06", true,
            "湖北省-武汉市-江岸区")]
        [DataRow(
            "231121199708302613", "1997-08-30", true,
            "黑龙江省-黑河市-", DisplayName = "已被撤销行政区")]
        public void Common(
            string code, string birthday, bool isMale, string area)
        {
            var res = ChineseIdHelper.Parse(code, out string? errmsg);
            Assert.IsNotNull(res);
            Assert.IsNull(errmsg);
            Assert.AreEqual(birthday, res.Birthday.ToString("yyyy-MM-dd"));
            Assert.AreEqual(isMale, res.IsMale);
            Assert.AreEqual(area, $"{res.ProvinceName}-{res.CityName}-{res.AreaName}");
        }

        private readonly static DateTime testAgeAt 
            = new DateTime(2024, 6, 15);//假设现在是这个日期
        [TestMethod]
        [DataRow("330226197605164239", 48, DisplayName = "5.16，生日已过")]
        [DataRow("450503197606146701", 48, DisplayName = "6.14，生日已过")]
        [DataRow("130404197606152542", 48, DisplayName = "6.15，生日当天")]
        [DataRow("411622197606161643", 47, DisplayName = "6.16，生日未到")]
        [DataRow("422802197607140359", 47, DisplayName = "7.14，生日未到")]
        public void AgeCalculation(string code, int expectAge)
        {
            var info = ChineseIdHelper.Parse(code, out _);
            Assert.IsNotNull(info);
            var age = info.GetAge(testAgeAt);
            Assert.AreEqual(expectAge, age);
        }

        [TestMethod]
        [DataRow("42010200000000000", ErrMsg.InvalidLength)]
        [DataRow("4201020000000000000", ErrMsg.InvalidLength)]
        [DataRow("42010200000000000A", ErrMsg.InvalidCharContained)]
        [DataRow("420102200313010000", ErrMsg.InvalidBirthday)]
        [DataRow("420102200312320000", ErrMsg.InvalidBirthday)]
        [DataRow("420102200301060810", ErrMsg.VerificationErr)]
        [DataRow("42010220030106081X", null, DisplayName = "没问题")]
        [DataRow("231121199708302613", null, DisplayName = "已被撤销行政区")]
        public void ShouldThrow(string code, string? expectErrmsg)
        {
            var res = ChineseIdHelper.Parse(code, out string? actualErrmsg);
            Assert.AreEqual(expectErrmsg, actualErrmsg);
            if (expectErrmsg is { })
                Assert.IsNull(res);
            else
                Assert.IsNotNull(res);
        }
    }
}