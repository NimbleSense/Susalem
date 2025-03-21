using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Entities
{
    [Table("ChemicalParas")]
    public class ChemicalParaModel :ValidateModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "id")]
        public int Id { get; set; }

        /// <summary>
        /// 回温后这个标志位会置1
        /// </summary>
        [Column("IsUse")]
        public bool IsUse { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        [Column("CabinetId")]
        public string CabinetId { get; set; }

        /// <summary>
        /// 工匠品名称
        /// </summary>
        [Column(name: "Name")]
        [Required(ErrorMessage = "工匠品名称不允许为空")]
        public string Name { get; set; }

        /// <summary>
        /// 工匠品料号
        /// </summary>
        [Column(name: "PNCode")]
        [Required(ErrorMessage = "工匠品料号不允许为空")]
        public string PNCode {  get; set; }

        /// <summary>
        /// 工匠品编号
        /// </summary>
        [Column(name: "SerialNum")]
        [Required(ErrorMessage = "工匠品编号不允许为空")]
        public string SerialNum { get; set; }

        /// <summary>
        /// 机台码
        /// </summary>
        [Column(name: "MachineId")]
        [Required(ErrorMessage = "机台码不允许为空")]
        public string MachineId { get; set; }

        /// <summary>
        /// 回温时间(小时)
        /// </summary>
        [Column(name: "ReheatingTime")]
        [Required(ErrorMessage = "回温时间不允许为空")]
        [Range(1, int.MaxValue, ErrorMessage = "回温时间必须大于0")]
        public double ReheatingTime { get; set; }

        /// <summary>
        /// 保质期（天）
        /// </summary>
        [Column(name: "ExpirationDate")]
        [Required(ErrorMessage = "保质期不允许为空")]
        [Range(1, int.MaxValue, ErrorMessage = "保质期必须大于0")]
        public double ExpirationDate { get; set; }

    }
}
