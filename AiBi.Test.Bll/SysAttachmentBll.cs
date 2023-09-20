using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using Newtonsoft.Json;
using System.Web;
using System.Linq;
using System.IO;
using System.Linq.Expressions;

namespace AiBi.Test.Bll
{
    public partial class SysAttachmentBll : BaseBll<SysAttachment, AttachmentReq.Page>
    {
        public const string _upload_path_= "/Upload/{0}/{1}/";
        public const string _upload_tmppath_= "/UploadTmp/{0}/{1}/";
        public override bool AddValidate(out string errorMsg, SysAttachment model)
        {
            errorMsg = "";
            var modules = EnumConvert.ToList<EnumAttachmentModule>();
            if (!modules.Any(a => (int)a.Value == model.Module))
            {
                errorMsg = "模块不匹配";
                return false;
            }
            if (HttpContext.Current.Request.Files.Count <= 0)
            {
                errorMsg = "没有找到上传的文件";
                return false;
            }
            var file = HttpContext.Current.Request.Files[0];
            model.HttpFile = file;
            model.FileName = DateTime.Now.Ticks.ToString();
            model.Name = file.FileName; 
            model.Ext = new DirectoryInfo(file.FileName).Extension;
            model.Path = string.Format(_upload_tmppath_,(EnumAttachmentModule)model.Module,DateTime.Now.ToString("yyyyMMdd"));
            return true;
            
        }

        public override void AddAfter(Response<SysAttachment, object, object, object> res, SysAttachment inModel)
        {
            var path = HttpContext.Current.Server.MapPath(res.data.Path);
            var file = HttpContext.Current.Server.MapPath(res.data.FullName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            res.data.HttpFile.SaveAs(file);
        }
        public new Response<SysAttachment, object, object, object> Modify(SysAttachment model)
        {
             var res = new Response<SysAttachment, object, object, object>();
            res.code = EnumResStatus.Fail;
            res.msg = "附件无法修改";

            return res;
        }
        public void Cancel(int id)
        {
            Cancel(Find(false, id));
        }
        public void Cancel(SysAttachment model)
        {
            if (model == null)
            {
                return;
            }
            model.Status = (int)EnumAttachmentStatus.Create;
            model.DelTime = DateTime.Now;
            model.DelUserId = CurrentUserId;
            model.IsDel = true;
        }
        public void Apply(int id)
        {
            Apply(Find(false,id));
        }
        public void Apply(SysAttachment model)
        {
            if (model==null)
            {
                return;
            }
            if (!model.Path.Contains("/UploadTmp/") || model.Status== (int)EnumAttachmentStatus.Apply)
            {
                return;
            }
            model.Status = (int)EnumAttachmentStatus.Apply;
            model.Path = model.Path.Replace("/UploadTmp/","/Upload/");
            model.ModifyTime = DateTime.Now;
            model.ModifyUserId = CurrentUserId;
        }
        public void ApplyFile(int id)
        {
            ApplyFile(Find(false,id));
        }
        public void ApplyFile(SysAttachment model)
        {
            if (model==null)
            {
                return;
            }
            if (!model.Path.Contains("/Upload/"))
            {
                return;
            }
            var newfile = HttpContext.Current.Server.MapPath(model.FullName);
            var oldfile = HttpContext.Current.Server.MapPath(model.FullName.Replace("/Upload/","/UploadTmp/"));
            if (!File.Exists(oldfile)|| File.Exists(newfile))
            {
                return;
            }
            var newpath = newfile.Substring(0, newfile.LastIndexOf("\\"));
            if (!Directory.Exists(newpath))
            {
                Directory.CreateDirectory(newpath);
            }
            File.Copy(oldfile,newfile,true);
        }

        public Response Clear() {
            var res = new Response();
            DelFilterMode = EnumDeleteFilterMode.All;
            GetListFilter(a => a.Where(b => b.IsDel || b.Status == (int)EnumAttachmentStatus.Create), a => a.OrderBy(b => b.Id), false).ToList().ForEach(
                a => {
                    try
                    {
                        if (a.FullName.Contains("/UploadTmp/"))
                        {
                            Context.SysAttachments.Remove(a);
                            return;
                        }
                        if (!File.Exists(HttpContext.Current.Server.MapPath(a.FullName)))
                        {
                            Context.SysAttachments.Remove(a);
                            return;
                        }
                        File.Delete(HttpContext.Current.Server.MapPath(a.FullName));
                        Context.SysAttachments.Remove(a);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            );
            if (Directory.Exists(HttpContext.Current.Server.MapPath("/UploadTmp")))
            {
                try
                {
                    Directory.Delete(HttpContext.Current.Server.MapPath("/UploadTmp"),true);
                }catch (Exception ex)
                {

                }
            }
            res.data = Context.SaveChanges();

            return res;
        }
    }
}
