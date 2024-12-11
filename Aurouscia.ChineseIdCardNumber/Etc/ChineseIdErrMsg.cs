namespace Aurouscia.ChineseIdCardNumber.Etc
{
    public static class ChineseIdErrMsg
    {
        private const string invalidLength = "长度异常";
        private const string invalidCharContained = "含有异常字符";
        private const string invalidAreaCode = "地区代码异常";
        private const string invalidBirthday = "出生日期异常";
        private const string verificationErr = "校验码错误";
        public static string InvalidLength { get; set; } = invalidLength;
        public static string InvalidCharContained { get; set; } = invalidCharContained;
        public static string InvalidAreaCode { get; set; } = invalidAreaCode;
        public static string InvalidBirthday { get; set; } = invalidBirthday;
        public static string VerificationErr { get; set; } = verificationErr;

        /// <summary>
        /// 重置错误信息为默认值
        /// </summary>
        public static void ResetToDefault()
        {
            InvalidLength = invalidLength;
            InvalidCharContained = invalidCharContained;
            InvalidAreaCode = invalidAreaCode;
            InvalidBirthday = invalidBirthday;
            VerificationErr = verificationErr;
        }
    }
}