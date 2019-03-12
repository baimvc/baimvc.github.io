﻿//-----------------------------------------------------------------------
// <copyright file= "EntityBase.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/7 20:05:46 
// Modified by:
// Description: 泛型实体基类
//-----------------------------------------------------------------------
using System;

namespace BlogDemo.Core
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public abstract class EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    #region Implements

    /// <summary>
    /// Guid 类型主键实体基类
    /// </summary>
    public abstract class EntityBase : EntityBase<int>
    { }

    #endregion
}
