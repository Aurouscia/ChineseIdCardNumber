# 中国大陆地区身份证号验证与信息提取
判断身份证号是否正确  
获取省市区名称、出生日期、性别

## 用法
Nuget 搜索安装 Aurouscia.ChineseIdCardNumber  
```
using Aurouscia.ChineseIdCardNumber;
var res = ChnIdHelper.Parse(身份证号, out string? 错误信息);

res : ChnIdInfo?
res.ProvinceName : string
res.CityName : string
res.AreaName : string
res.Birthday : DateTime
res.IsMale : bool
```
解决方案内含单元测试  

## License
MIT