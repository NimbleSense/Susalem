using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Fam.FamEmployeeJobStatusEnum;
using Susalem.Fam.FamGenderEnum;

namespace Susalem.Fam.FamEmployees
{
    /// <summary>
    /// 【领域实体】员工管理
    /// </summary>
    public class Fam_Employee : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 员工号 (域账号)
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Code { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 在职状态
        /// </summary>
        [Required]
        public EmployeeJobStatusEnum JobStatus { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength18)]
        public string IdCard { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Email { get; set; }

        /// <summary>
        /// 头像URL
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string HeadImage { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        public Fam_Employee() { }

        /// <summary>
        /// 【领域实体】员工管理
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="code">员工号</param>
        /// <param name="name">员工姓名</param>
        /// <param name="jobStatus">在职状态</param>
        /// <param name="dataSourceType">数据来源</param>
        /// <param name="idCard">身份证号</param>
        /// <param name="birthDay">生日</param>
        /// <param name="gender">性别</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="headImage">头像Url</param>
        /// <param name="userId">用户Id</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="remark">备注</param>
        /// <param name="isEnable">是否启用</param>
        public Fam_Employee(
                Guid id,
                string code,
                string name,
                EmployeeJobStatusEnum jobStatus,
                string dataSourceType,
                string idCard,
                DateTime? birthDay,
                GenderEnum gender,
                string email,
                string headImage,
                Guid userId,
                ExtraPropertyDictionary extraProperties,
                string remark = null,
                bool isEnable = true)
        {
            Id = id;
            Code = code;
            Name = name;
            JobStatus = jobStatus;
            DataSourceType = dataSourceType;
            IdCard = idCard;
            BirthDay = birthDay;
            Gender = gender;
            Email = email;
            HeadImage = headImage;
            UserId = userId;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }

    }
}
