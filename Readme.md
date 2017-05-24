# 亿方云 C# SDK

    亿方云 C# 版本 SDK，集成亿方云 V2 系列API，具有强大的文件管理能力。该 SDK 可以在包括 .Net 3.5 以上的各个版本上运行。

## 安装

1. 如果使用Nuget，直接在Nuget包管理器中搜索fangcloud.api安装即可。

2. 如果需要引入工程源码，在github上下载工程引入项目即可。工程文件兼容vs2015和vs2017。

3. 如果直接引入dll文件，下载fangcloud.dll引入，值得注意的是，sdk依赖于Newtonsoft.Json(9.0.1)

## 创建应用

    创建完应用后就得到了 client_id 和 client_secret，这两个参数会在 SDK 中使用，请妥善保管，不要泄露给别人。

## 使用亿方云 API

    所有用户必须先通过 OAuth2 授权你的应用，然后才能通过你的应用获取用户的相关信息。完成授权后会返回关联了用户亿方云账号的 access token 和 refresh token 给你，在请求中使用这些 token 才能拿到用户文件。

    * 授权登录：授权登录的简单web demo

    一旦获取了用户的 access token，就可以创建一个 YfyClient 并使用它进行 api 请求。

    每一个用户初次登录都需要进行一遍授权流程，默认的 access token 过期时间为6小时，refresh token 过期时间为90天。access token 过期就需要 app 使用用户的 refresh token 获取新的 access token，如果 refresh token 也过期，则用户必须重走授权流程。SDK 中有 access token 自动刷新机制，推荐使用。用户的 access token 和 refresh token 需要持久化，方便重复使用。

## 运行示例

### WebDemo

WebDemo 是一个小型的 web app，包括了完整的 Oauth2 的授权和发送 api 过程。运行此 demo 时需进入企业控制台——企业设置——开放平台，将应用官网URL改成 http://localhost:51669/Oauth.aspx。成功后修改 WebDemo下的 Web.config 文件，填入你的 client_id 和 client_secret, 即可在vs内建的IIS中运行该demo

### RequestDemo

RequestDemo 是一个简单的通过 access token 获取用户信息的 demo。

### Api使用

使用流程:
1. 引入命名空间。

    `using namespace yfy.api;`

2. 初始化YfySystem。ClientId和ClientSecret 是你在亿方云网站上申请的id和secret

    `YfySystem.Init(YourClientId, YourClientSecret);`

3. 使用accesstoken初始化Yfyclient

    `var fc = new YfyClient(YourAccessToekn);`

4. 使用YfyClient中的Users/Files/Folders/Common去操作特定的api

    `var user = YfyClient.Users.GetAccountInfo();`

