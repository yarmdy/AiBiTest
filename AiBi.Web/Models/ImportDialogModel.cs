using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Web
{
    public class ImportDialogModel
    {
        /// <summary>
        /// 对话框名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string TemplateUrl { get;set; }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl { get; set; }
        /// <summary>
        /// 上传参数
        /// </summary>
        public Dictionary<string,object> ParamList { get; set; }
        /// <summary>
        /// 指定允许上传时校验的文件类型。可选值有：
        /// images 图片类型
        /// file 所有文件类型
        /// video 视频类型
        /// audio 音频类型
        /// </summary>
        public string Accept { get; set; }
        /// <summary>
        /// 允许上传的文件后缀。一般结合 accept 属性来设定。
        /// 假设 accept: 'file' 类型时，那么设置 exts: 'zip|rar|7z' 即代表只允许上传压缩格式的文件。
        /// 默认为常见图片后缀，即 jpg|png|gif|bmp|jpeg|svg
        /// </summary>
        public string Exts { get; set; }
    }
    
}
