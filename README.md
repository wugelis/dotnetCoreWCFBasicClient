# 在 .NET Core 自定義 ClientBase<> 呼叫 WCF 範例


## WCF 的 Open Source Project

先前提到 WCF 的 Open Source Project [https://github.com/dotnet/wcf] 因為這個專案讓這件事情變的容易。

## 套件引用

因為這個 MIT 的專案，因此實現這個 WCF 的 ClientBase<> 可安裝官方維護與上傳的 System.ServiceModel 套件，輕易完成 WCF Client

(1). System.ServiceModel.Duplex
(2). System.ServiceModel.Http
(3). System.ServiceModel.NetTcp
(4). System.ServiceModel.Security