﻿
using SimpleMQTT;

namespace SimpleAdmin.Plugin.Mqtt;

/// <summary>
/// AppStartup启动类
/// </summary>
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("注册Mqtt插件");
        services.AddMqttClientManager();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        App.GetService<IMqttClientManager>();//获取mqtt服务判断配置是否有问题
    }

}
