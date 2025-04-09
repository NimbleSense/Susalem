using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Susalem.Susalem.Result
{
    /// <summary>
    /// 自定义返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WrapResult<T>
    {

        /// 是否成功
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// 默认初始化
        /// </summary>
        public WrapResult()
        {
            IsSuccess = true;
            Message = "";
            Data = default;
            Code = 200;
        }
        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public void SetSuccess(T data, string message = "成功", int code = 200)
        {
            IsSuccess = true;
            Data = data;
            Code = code;
        }
        /// <summary>
        /// 失败返回值
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public void SetFail(string message = "失败", int code = 500)
        {
            IsSuccess = false;
            Message = message;
            Code = code;
        }
    }

}
