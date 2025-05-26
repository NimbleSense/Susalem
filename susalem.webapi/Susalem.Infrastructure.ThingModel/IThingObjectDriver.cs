using DynamicExpresso;
using Susalem.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel
{
    public interface IThingObjectDriver
    {
        /// <summary>
        /// 通信连接
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// 通信断开
        /// </summary>
        /// <returns></returns>
        void Disconnect();

        /// <summary>
        /// Check is connected
        /// </summary>
        bool IsConnected { get; }


        /// <summary>
        /// 写入单个寄存器指令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="command"></param>
        bool ExecuteReg(int address, WriteCommandDto command);

        /// <summary>
        /// 写入多个寄存器指令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commands"></param>
        bool ExecuteRegs(int address, IList<WriteCommandDto> commands);

        /// <summary>
        /// 写入处理后的多个寄存器指令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commands"></param>
        bool ExecuteComputedRegs(int address, IList<ThingCommandDto> commands);

        /// <summary>
        /// 写入多个线圈指令
        /// </summary>
        /// <param name="address"></param>
        /// <param name="commands"></param>
        bool ExecuteCoils(int address, IList<ThingCommandDto> commands);

        /// <summary>
        /// 读取多个寄存器
        /// </summary>
        /// <param name="address"></param>
        /// <param name="telemetries"></param>
        bool ReadRegs(int address, IList<ThingCommandDto> telemetries);

        /// <summary>
        /// 读取多个线圈
        /// </summary>
        /// <param name="address"></param>
        /// <param name="telemetries"></param>
        bool ReadCoils(int address, IList<ThingCommandDto> telemetries);


    }

    public class WriteCommandDto
    {
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public ushort Reg { get; }

        /// <summary>
        /// 单个寄存器写入值
        /// </summary>
        public ushort Value
        {
            get
            {
                if (Values != null && Values.Length > 0)
                {
                    return Values[0];
                }
                return 0;
            }
        }

        /// <summary>
        /// 多个寄存器数组
        /// </summary>
        public ushort[] Values { get; }

        public WriteCommandDto(ushort reg, ushort value)
        {
            Reg = reg;
            Values = new ushort[] { value };
        }

        public WriteCommandDto(ushort reg, ushort[] value)
        {
            Reg = reg;
            Values = value;
        }

        public void Update(ushort value)
        {
            Values[0] = value;
        }
    }

    public class ThingCommandDto
    {
        /// <summary>
        /// 数据名称
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 寄存器地址
        /// </summary>
        public byte Reg { get; set; }

        /// <summary>
        /// 读取或者写入的长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public int Factor { get; set; }

        /// <summary>
        /// 计算后的显示数据值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 读取到的原始值
        /// </summary>
        public uint OriginalValue { get; set; }

        public ThingCommandDto(string key, byte reg, int length, string expression)
        {
            Key = key;
            Reg = reg;
            Length = length;
            Expression = expression;
        }

        /// <summary>
        /// 将原始值通过计算，变成真实数据值
        /// </summary>
        /// <param name="initialValue"></param>
        public void Cal(uint initialValue)
        {
            OriginalValue = initialValue;

            //先乘以系数
            initialValue *= (uint)Factor;

            if (string.IsNullOrEmpty(Expression))
            {
                Value = initialValue;
                return;
            }

            var target = new Interpreter();
            var result =
                target.Eval<double>(Expression, new DynamicExpresso.Parameter("raw", typeof(double), initialValue));

            Value = Math.Round(result, 2);
        }

        /// <summary>
        /// 获取写入值
        /// </summary>
        /// <returns></returns>
        public ushort[] GetWriteData()
        {
            var datas = new ushort[Length];

            //先乘以系数
            var data = Value * Factor;

            if (!string.IsNullOrEmpty(Expression))
            {
                var target = new Interpreter();
                data = target.Eval<double>(Expression, new DynamicExpresso.Parameter("raw", typeof(double), data));
            }

            byte[] byteDatas;
            if (data < 0)
            {
                var intData = Convert.ToInt32(data);
                byteDatas = BitConverter.GetBytes(intData);
            }
            else
            {
                var intData = Convert.ToUInt32(data);
                byteDatas = BitConverter.GetBytes(intData);
            }

            for (var i = 0; i < Length; i++)
            {
                datas[i] = BitConverter.ToUInt16(byteDatas, i * 2);
            }
            return datas;
        }

    }
}
