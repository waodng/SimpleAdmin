﻿namespace SimpleAdmin.Plugin.CodeFirst;

/// <summary>
/// 资源表种子数据
/// </summary>
public class SysResourceSeedData : ISqlSugarEntitySeedData<SysResource>
{
    public IEnumerable<SysResource> SeedData()
    {
        return SeedDataUtil.GetSeedData<SysResource>(SqlsugarConst.DB_Default, "sys_resource.json");
    }
}