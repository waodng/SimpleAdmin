﻿using Furion.DataEncryption;
using Microsoft.AspNetCore.Http;
using SimpleTool;

namespace SimpleAdmin.Plugin.Mqtt;

/// <summary>
/// <inheritdoc cref="IMqttService"/>
/// </summary>
public class MqttService : IMqttService
{
    private readonly ISimpleCacheService _simpleCacheService;

    public MqttService(ISimpleCacheService simpleCacheService)
    {
        this._simpleCacheService = simpleCacheService;
    }

    /// <inheritdoc/>
    public async Task<MqttParameterOutput> GetWebLoginParameter(SysUser user)
    {
        var token = JWTEncryption.GetJwtBearerToken((DefaultHttpContext)App.HttpContext); // 获取当前token
        //获取mqtt配置
        var mqttconfig = await GetMqttConfig();
        //地址
        var url = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_URL).Select(it => it.ConfigValue).FirstOrDefault();
        //用户名
        var userName = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_USERNAME).Select(it => it.ConfigValue).FirstOrDefault();
        //密码
        var password = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_PASSWORD).Select(it => it.ConfigValue).FirstOrDefault();

        #region 用户名特殊处理

        if (userName.ToLower() == "$username")
            userName = user.Account;
        else if (userName.ToLower() == "$userid")
            userName = user.Id.ToString();

        #endregion 用户名特殊处理

        #region 密码特殊处理

        if (password.ToLower() == "$username")
            password = token; // 当前token作为mqtt密码

        #endregion 密码特殊处理

        var clientId = $"{user.Id}_{RandomHelper.CreateLetterAndNumber(5)}";//客户端ID
        _simpleCacheService.Set(CacheConst.Cache_MqttClientUser + clientId, token, TimeSpan.FromMinutes(1));//将该客户端ID对应的token插入redis后面可以根据这个判断是哪个token登录的
        return new MqttParameterOutput
        {
            ClientId = clientId,
            Password = password,
            Url = url,
            UserName = userName,
            Topics = new List<string> { MqttConst.Mqtt_TopicPrefix + user.Id }//默认监听自己
        };
    }

    /// <inheritdoc/>
    public async Task<MqttAuthOutput> Auth(MqttAuthInput input, string userId)
    {
        MqttAuthOutput mqttAuthOutput = new MqttAuthOutput { Is_superuser = false, Result = "deny" };

        //获取用户token
        var tokens = _simpleCacheService.HashGetOne<List<TokenInfo>>(CacheConst.Cache_UserToken, userId);
        if (tokens != null)
        {
            if (tokens.Any(it => it.Token == input.Password))//判断是否有token
                mqttAuthOutput.Result = "allow";//允许登录
        }
        return mqttAuthOutput;
    }

    #region 方法

    private async Task<List<DevConfig>> GetMqttConfig()
    {
        var key = CacheConst.Cache_DevConfig + CateGoryConst.Config_MQTT_BASE;//mqtt配置key
        //先从redis拿配置
        var configList = _simpleCacheService.Get<List<DevConfig>>(key);
        if (configList == null)
        {
            //redis没有再去数据可拿
            configList = await DbContext.Db.Queryable<DevConfig>().Where(it => it.Category == CateGoryConst.Config_MQTT_BASE).ToListAsync();//获取mqtt配置配置列表
            if (configList.Count > 0)
            {
                _simpleCacheService.Set(key, configList);//如果不为空,插入redis
            }
        }
        return configList;
    }

    #endregion 方法
}