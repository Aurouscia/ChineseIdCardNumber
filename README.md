# 中国大陆地区身份证号验证与信息提取
判断身份证号是否正确  
获取省市区名称、出生日期、性别

## 用法
Nuget 搜索安装 Aurouscia.ChineseIdCardNumber  
```csharp
using Aurouscia.ChineseIdCardNumber;
var res = ChineseIdHelper.Parse(身份证号, out string? 错误信息);

res : ChineseIdInfo?
res.ProvinceName : string?
res.CityName : string?
res.AreaName : string?
res.Birthday : DateTime
res.IsMale : bool
res.GetAge() : int
```
已被撤销的行政区划名称返回null  

### 自定义错误信息
```csharp
//设置ChineseIdErrMsg静态对象的属性值
ChineseIdErrMsg.InvalidLength = "长度有问题"
ChineseIdErrMsg.VerificationErr = "校验码有问题"
ChineseIdErrMsg.xxx
```

## License
MIT