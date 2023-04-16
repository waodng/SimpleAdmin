namespace SimpleAdmin.System;

/// <summary>
/// 组织分页查询参数
/// </summary>
public class SysOrgPageInput : BasePageInput
{
    /// <summary>
    /// 父ID
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    /// 机构列表
    /// </summary>
    public List<string> OrgIds { get; set; }
}

/// <summary>
/// 组织添加参数
/// </summary>
public class SysOrgAddInput : SysOrg
{
}

/// <summary>
/// 组织修改参数
/// </summary>
public class SysOrgEditInput : SysOrgAddInput
{
    /// <summary>
    /// Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override string Id { get; set; }
}

/// <summary>
/// 组织复制参数
/// </summary>
public class SysOrgCopyInput
{
    /// <summary>
    /// 目标ID
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    /// 组织Id列表
    /// </summary>
    [Required(ErrorMessage = "Ids列表不能为空")]
    public List<string>? Ids { get; set; }

    /// <summary>
    /// 是否包含下级
    /// </summary>
    public bool ContainsChild { get; set; } = false;
}