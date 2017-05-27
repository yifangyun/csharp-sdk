# 亿方云 C# SDK

    亿方云 C# 版本 SDK，集成亿方云 V2 系列API。该 SDK 可以在包括 .Net 3.5 以上的各个版本上运行。

## 安装

当前最新版本及最新改动同步在master分支。

**1. 直接添加DLL引用**

您可以在[这里](https://github.com/yifangyun/fangcloud-csharp-sdk/releases)找到所有的Release，选择您需要的版本下载，解压后将*.dll文件添加至项目引用。需要注意的是，此SDK依赖[Json.NET](http://www.newtonsoft.com/json)，可以添加对应版本Newtonsoft.Json.dll引用或者使用NuGet来安装它：

```
Install-Package Newtonsoft.Json
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

    `using namespace Yfy.Api;`

2. 初始化YfySystem。ClientId和ClientSecret 是你在亿方云网站上申请的id和secret

    `YfySystem.Init(YourClientId, YourClientSecret);`

3. 使用accesstoken初始化Yfyclient

    `var fc = new YfyClient(YourAccessToken);`

4. 使用YfyClient中的Users/Files/Folders/Common去操作特定的api

    `var user = fc.Users.Info();`

## 运行示例

### WebDemo

WebDemo 是一个小型的 web app，包括了完整的 Oauth2 的授权和发送 api 过程。运行此 demo 时需进入企业控制台—企业设置—开放平台，修改你的应用回调地址。 成功后修改 WebDemo下的 Web.config 文件，填入你的 client_id, client_secret和redirect_url, 即可在vs内建的IIS中运行该demo

### RequestDemo

RequestDemo 是一个简单的通过 access token 获取用户信息的 demo。


## 贡献代码

1. Fork

2. 创建新分支 git checkout -b my-new-feature

3. 提交改动 git commit -am 'Added some feature'

4. 将您的修改记录提交到远程 git 仓库 git push origin my-new-feature

5. 然后到 github 网站的该 git 远程仓库的 my-new-feature 分支下发起 Pull Request


## 许可证

Copyright (c) 2017 杭州亿方云网络科技有限公司

基于 MIT 协议发布