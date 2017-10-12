# 亿方云 C# SDK

    亿方云 C# 版本 SDK，集成亿方云 V2 系列API。该 SDK 可以在包括 .Net 4.0 以上的各个版本上运行。

    .Net 3.5 版本请使用 2.x 版本。 3.x版本支持Jwt模式，需要 .Net 4.0及以上运行环境

## 安装

当前最新版本及最新改动同步在master分支。

**1. 直接添加DLL引用**

您可以在[这里](https://github.com/yifangyun/fangcloud-csharp-sdk/releases)找到所有的Release，选择您需要的版本下载，解压后将*.dll文件添加至项目引用。需要注意的是，此SDK依赖[Json.NET](http://www.newtonsoft.com/json), [Jose.Jwt](https://github.com/dvsekhvalnov/jose-jwt)和[BouncyCastle](http://bouncycastle.org/csharp/index.html)。

```
Install-Package Newtonsoft.Json
Install-Package jose-jwt
Install-Package BouncyCastle
```

**2. 包管理器(NuGet)安装** 

或者从NuGet来安装，以Visual Studio 2015/2017为例，打开NuGet程序包管理器搜索`Yfy.Api`或者在控制台中键入以下命令：

```
Install-Package Yfy.Api
```

**3. 从源码编译**

当然，您也可以直接从源码编译

```
git clone https://github.com/yifangyun/fangcloud-csharp-sdk.git
```

## API参考手册

[SDK API参考手册](https://yifangyun.github.io/fangcloud-csharp-sdk/Help/index.html)

[亿方云开放平台文档](https://open.fangcloud.com/wiki/v2)

## Api使用

使用流程:

1. 引入命名空间。

    ```cs
    using Yfy.Api;
    ```

2. 初始化YfySystem。ClientId和ClientSecret 是你在亿方云网站上申请的id和secret

    ```cs
    YfySystem.Init(YourClientId, YourClientSecret);
    ```

3. 使用accesstoken初始化Yfyclient

    ```cs
    var fc = new YfyClient(YourAccessToken);
    ```

4. 使用YfyClient中的Users/Files/Folders/Common等去操作特定的api

    ```cs
    var user = fc.Users.Info();
    ```

## 运行示例

### WebDemo

WebDemo 是一个小型的 web app，包括了完整的 Oauth2 的授权和发送 api 过程。运行此 demo 时需进入企业控制台—企业设置—开放平台，修改你的应用回调地址。 成功后修改 WebDemo下的 Web.config 文件，填入你的 client_id, client_secret和redirect_url, 即可在vs内建的IIS中运行该demo

### RequestDemo

RequestDemo 是一个简单的通过 access token 获取用户信息的 demo。

### 获取AccessToken的方法

* 通过OAuth授权的方式，示例见WebDemo

* 通过密码模式

    注意，密码模式的用户名和密码不是clientId 和 clientSecret!

    ```cs
    var username = "username";
    var password = "password";
    var clientId = "Your clientId";
    var clientSecret = "Your clientSecret";
    YfySystem.Init(clientId, clientSecret);

    var token = OAuthHelper.GetOAuthTokenByPassword(username, password);
    ```

* 通过Jwt模式

    1. 首先构造Jwt的payload
    
    各项参数说明见[文档](https://open.fangcloud.com/wiki/v2/#ru-he-gou-zao-jwt)

    ```cs
    var payload = new YfyJwtPayload(YfySubType, kid, sub, JwtAlgorithms);
    ```

    2. 构造 X509Certificate2 对象

    ```cs
    var cert = new X509Certificate2(pathToCert);
    ```

    关于如何使用 X509Certificate2 请参阅MSDN文档。

    **需要注意的是，构造的 X509Certificate2 必须包含私钥。**

    3. 获取AccessToken

    ```cs
    var accessToken = OAuthHelper.GetOAuthTokenByJwt(payload, cert);
    ```

    * 如果你拥有RSA私钥，可以直接使用RSA私钥获取AccessToken

    ```cs
    var accessToken = OAuthHelper.GetOAuthTokenByJwt(payload, pathToRSAKey, RSAPasswd);
    ```

    **注意：RSA key 只接受 pkcs1 格式，不接受 pkcs8 格式！**
    
    * 补充

        1. RSA pkcs8转成pkcs1格式

        ```bash
        openssl rsa -in pkcs8_rsa_private.key -out pkcs1_private.key
        ```

        2. 使用已存在的RSA 私钥生成自签名证书

        ```bash
        opensshl req -new -x509 -days 365 -key private.key -out cert.crt
        ```

        ```bash
        openssl pkcs12 -export -in cert.crt -inkey private.key -out server.p12
        ```

        这样生成的 p12 文件可以用来直接构造 X509Certificate2 对象


## 贡献代码

1. Fork

2. 创建新分支 git checkout -b my-new-feature

3. 提交改动 git commit -am 'Added some feature'

4. 将您的修改记录提交到远程 git 仓库 git push origin my-new-feature

5. 然后到 github 网站的该 git 远程仓库的 my-new-feature 分支下发起 Pull Request


## 许可证

Copyright (c) 2017 杭州亿方云网络科技有限公司

基于 MIT 协议发布