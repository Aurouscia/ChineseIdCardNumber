﻿using System.Text.RegularExpressions;
using Aurouscia.ChineseIdCardNumber.Etc;
using ChinaProvinceCityArea;

namespace Aurouscia.ChineseIdCardNumber
{
    public static class ChineseIdHelper
    {
        public static ChineseIdInfo? Parse(string idNumber, out string? errmsg, bool looseVerify = false)
        {
            idNumber = idNumber.Trim().ToUpper();
            ReadOnlySpan<char> span = idNumber.AsSpan();
            if (span.Length != 18)
            {
                errmsg = ChineseIdErrMsg.InvalidLength;
                return null;
            }
            if (!Regex.IsMatch(idNumber, "^[0-9]{17}[0-9|X]$"))
            {
                errmsg = ChineseIdErrMsg.InvalidCharContained;
                return null;
            }
            
            var areaSpan = span.Slice(0, 6);
            int areaCode = int.Parse(areaSpan);
            var areaRes = ChinaAreaHelper.Get(areaCode);
            //无法获知是不是已废弃区划号码，不能因为获取不到而认为身份证号非法
            //但如果areaRes为null，那说明位数有误（0开头），肯定有问题
            if (areaRes is null)
            {
                errmsg = ChineseIdErrMsg.InvalidAreaCode;
                return null;
            }
            
            
            var yearSpan = span.Slice(6, 4);
            int year = int.Parse(yearSpan);
            var monthSpan = span.Slice(10, 2);
            int month = int.Parse(monthSpan);
            var daySpan = span.Slice(12, 2);
            int day = int.Parse(daySpan);
            DateTime birth;
            try
            {
                birth = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                errmsg = ChineseIdErrMsg.InvalidBirthday;
                return null;
            }

            var serialLastDigit = span[16] - '0';
            bool isMale = serialLastDigit % 2 == 1;

            //GB11643-1999标准校验码
            bool verifyPassed = true;
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                var coeff = verifyMutCoeff[i];
                var digit = span[i] - '0';
                sum += coeff * digit;
            }
            int computedCodeIdx = sum % 11;
            if (verifyCodeResArr[computedCodeIdx] != span[17])
            {
                if (!looseVerify)
                {
                    errmsg = ChineseIdErrMsg.VerificationErr;
                    return null; //若不是宽松模式，返回null
                }
                else
                    verifyPassed = false; //若是宽松模式，将verifyPassed设为false
            }

            errmsg = null;
            return new(areaRes.ProvinceName, areaRes.CityName, areaRes.AreaName, birth, isMale, verifyPassed);
        }
        private readonly static char[] verifyCodeResArr = ['1','0','X','9','8','7','6','5','4','3','2'];
        private readonly static byte[] verifyMutCoeff = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
    }
}