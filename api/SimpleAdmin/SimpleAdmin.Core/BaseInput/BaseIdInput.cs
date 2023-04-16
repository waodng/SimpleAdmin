﻿namespace SimpleAdmin.Core;

/// <summary>
/// 主键Id输入参数
/// </summary>
public class BaseIdInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [IdNotNull(ErrorMessage = "Id不能为空")]
    [DataValidation(ValidationTypes.Numeric)]
    public virtual string Id { get; set; }
}