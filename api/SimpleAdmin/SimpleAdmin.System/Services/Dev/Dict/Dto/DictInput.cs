﻿namespace SimpleAdmin.System;

/// <summary>
/// 字典查询参数
/// </summary>
public class DictInput
{
}

/// <summary>
/// 字典树参数
/// </summary>
public class DictTreeInput
{
    /// <summary>
    /// 字典分类
    /// </summary>
    public string Category { get; set; }
}

/// <summary>
/// 字典查询参数
/// </summary>
public class DictPageInput : BasePageInput
{
    /// <summary>
    /// 父id
    ///</summary>
    public string ParentId { get; set; }

    /// <summary>
    /// 分类
    ///</summary>
    public string Category { get; set; }
}

/// <summary>
/// 添加字典参数
/// </summary>
public class DictAddInput : DevDict
{
    /// <summary>
    /// 父ID
    /// </summary>
    public override string ParentId { get; set; } = SimpleAdminConst.Zero;

    /// <summary>
    /// 字典名称
    /// </summary>
    [Required(ErrorMessage = "DictLabel不能为空")]
    public override string DictLabel { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>

    [Required(ErrorMessage = "DictValue不能为空")]
    public override string DictValue { get; set; }
}

/// <summary>
/// 编辑字典参数
/// </summary>
public class DictEditInput : DictAddInput
{
    /// <summary>
    /// ID
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    public override string Id { get; set; }
}

/// <summary>
/// 删除字典参数
/// </summary>
public class DictDeleteInput : BaseIdInput
{
}