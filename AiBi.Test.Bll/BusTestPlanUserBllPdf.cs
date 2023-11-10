using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;
using AiBi.Test.Common;
using AiBi.Test.Dal.Enum;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using iText.Html2pdf;
using iText.Kernel.Font;
using iText.Html2pdf.Resolver.Font;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Kernel.Geom;
using System.Numerics;
using System.Text;

namespace AiBi.Test.Bll
{
    public partial class BusTestPlanUserBll : BaseBll<BusTestPlanUser,PlanUserReq.Page>
    {
        #region 模板
        const string _html = @"<!doctype html>
<html lang=""zh-cn"">
<head>
<meta charset=""utf-8"" />
<meta name=""viewport"" content=""width=device-width, initial-scale=1, viewport-fit=cover"" />
<meta http-equiv=""X-UA-Compatible"" content=""ie=edge"" />
<link href=""layui/css/layui.css"" rel=""stylesheet"" />
<link href=""Styles/aibibase.css"" rel=""stylesheet"" />
<style>
    .propname{{
        font-weight:400;
        text-align:right;
    }}
    #page {{
        background: white;
        width: 980px;
        border: 1px solid #cfcfcf;
        margin-top: 20px;
        box-sizing:content-box;
    }}
    .layui-table td, .layui-table th, .layui-table-col-set, .layui-table-fixed-r, .layui-table-grid-down, .layui-table-header, .layui-table-mend, .layui-table-page, .layui-table-tips-main, .layui-table-tool, .layui-table-total, .layui-table-view, .layui-table[lay-skin=line], .layui-table[lay-skin=row]{{
        border-color:black
    }}
    strong{{font-weight: bold;}}
    .strongt{{
        margin-top:15px;
        display:block;
        font-weight: bold;
    }}
    .red{{
        color:red;
    }}
    .redu{{
        color:red;
        text-decoration:underline;
    }}
    .layui-table tbody tr:hover, .layui-table thead tr, .layui-table-click, .layui-table-header, .layui-table-hover, .layui-table-mend, .layui-table-patch, .layui-table-tool, .layui-table-total, .layui-table-total tr, .layui-table[lay-even] tr:nth-child(even) {{
        background-color: transparent;
    }}
</style>
</head>
<body>
<div style=""padding:20px;line-height:26px"">
    <h1 style=""text-align:center;padding:35px 0"">爱拜尔PSY心理测评报告</h1>
    <table class=""layui-table"" style=""color:black"">
        <tbody>
            {0}{1}{2}
        </tbody>
    </table>
</div>
</body>
</html>
";
        const string _header = @"<tr>
    <td>测试名称</td>
    <td colspan=""5"">{0}</td>
</tr>
<tr>
    <td>题目数</td>
    <td>{1}</td>
    <td>完成题数</td>
    <td>{2}</td>
    <td>版  本</td>
    <td>V1.0</td>
</tr>
<tr>
    <td>规定时长</td>
    <td>{3}分钟</td>
    <td>测评对象</td>
    <td>{4}</td>
    <td>测评人员</td>
    <td>{5}</td>
</tr>
<tr>
    <td>实际用时</td>
    <td>{6}分钟</td>
    <td>页    次</td>
    <td id=""pagenum"">{7}</td>
    <td>报告日期</td>
    <td>{8}</td>
</tr>
<tr>
    <td>测试时间</td>
    <td>{9}</td>
    <td>结束时间</td>
    <td>{10}</td>
    <td>完成时间</td>
    <td>{11}</td>
</tr>
";
        const string _detail = @"<tr>
<td colspan=""6"" style=""line-height:26px"">
    <div></div>
    <strong class=""strongt"">说明</strong>
    <pre style=""font-family: 微软雅黑;font-size:14px;line-height:26px; "">{2}</pre>
    <strong class=""strongt"">评价方法</strong>
    <pre style=""font-family: 微软雅黑;font-size:14px;line-height:26px; "">{3}</pre>
    <strong class=""strongt"">测试题</strong>
    {0}
    <strong class=""strongt"">评价结果</strong>
    <div>
        {1}
    </div>
</td>
</tr>";
        const string _tail = @"<tr>
    <td>相关说明</td>
    <td colspan=""5""></td>
</tr>
<tr>
    <td>编制人员</td>
    <td></td>
    <td>审核人员</td>
    <td></td>
    <td>批准人员</td>
    <td></td>
</tr>
<tr>
    <td>编制日期</td>
    <td></td>
    <td>审核日期</td>
    <td></td>
    <td>批准日期</td>
    <td></td>
</tr>";
        const string _questionTitle = @"<div>
    <div style=""float:left; line-height:26px;"">{0}.</div>
    <pre style=""display:block;font-family: 微软雅黑;font-size:14px;line-height:26px;"">{1}</pre>
</div><div style=""clear:both""></div>";
        const string _questionImg = @"<div style=""text-align:center""><img style=""height:100px;margin:10px auto;display:block;object-fit:cover"" src=""{0}"" /></div>";
        const string _questionAnswer = @"<div class=""layui-inline"" style=""margin-bottom: 0;"">
    <strong style=""color:red;{1}"">（{0}）</strong>
</div>";
        
        const string _resultAnswer = @"答题：<span class=""red"">{0}</span>";
        const string _resultNothing = @"<div style=""padding:50px;color:#888;text-align:center"">暂无结果</div>";
        const string _resultScore = @"&nbsp;分数{0}：<span class=""redu"">{1}</span>";
        const string _resultDesc = @"<pre style=""font-family: 微软雅黑;font-size:14px;line-height:26px;"">
{0}：{1}
{2}
</pre>";
        #endregion
        private Stream planToPdf(BusTestPlan plan)
        {
            var questions = plan.Template.BusTestTemplateExamples.SelectMany(a=>a.Example.BusExampleQuestions).ToList();
            var results = plan.Template.BusTestTemplateExamples.SelectMany(a=>a.Example.BusExampleResults).ToList();
            var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            using (var pdf = new PdfDocument(writer.SetSmartMode(true)))
            using (var doc = new Document(pdf,PageSize.A3))
            {
                var fontpd = new DefaultFontProvider(true,true,true,"SimHei");
                var seletor = fontpd.GetFontSelector(new[] { "微软雅黑"},new iText.Layout.Font.FontCharacteristics());
                var font = seletor.GetFonts().Where(a => a + "" == "SimHei").FirstOrDefault();
                doc.SetFontProvider(fontpd);
                var pfont = PdfFontFactory.CreateFont(font.GetFontName());
                doc.SetFont(pfont);
                var properties = new ConverterProperties();
                properties.SetFontProvider(fontpd);
                properties.SetBaseUri(AppDomain.CurrentDomain.BaseDirectory);
                //var css = new DefaultCssApplierFactory();
                plan.BusTestPlanUsers.ForEach((planUser, index) =>
                {
                    if (index > 0)
                    {
                        doc.Add(new AreaBreak(iText.Layout.Properties.AreaBreakType.NEXT_PAGE));
                    }


                    #region 标题表格表头
                    var header = string.Format(_header, plan.Name, plan.QuestionNum
                        , planUser.FinishQuestion
                        , plan.Template.Duration
                        , (planUser.User?.BusUserInfoUsers?.FirstOrDefault()?.RealName) ?? (planUser.User?.Name)
                        , (planUser.CreateUser?.BusUserInfoUsers?.FirstOrDefault()?.RealName) ?? (planUser.CreateUser?.Name)
                        , Math.Round(planUser.Duration / 60.0)
                        , ""
                        , planUser.EndTime?.ToString("yyyy.MM.dd")
                        ,planUser.BeginTime?.ToString("yyyy.MM.dd HH:mm:ss")
                        ,planUser.EndTime?.ToString("yyyy.MM.dd HH:mm:ss")
                        ,planUser.EndTime?.ToString("yyyy.MM.dd HH:mm:ss")
                        );
                    #endregion

                    #region 测试题
                    var questionsb = new StringBuilder();
                    questions.ForEach((a,i)=> {
                        if (a.SortNo2 == null)
                        {
                            questionsb.AppendFormat(_questionTitle,i+1, a.Question.Title);
                            if (a.Question.Image != null)
                            {
                                questionsb.AppendFormat(_questionImg,a.Question.Image.FullName.Substring(1));
                            }
                            var myop = plan.BusTestPlanUserOptions.Where(b=>{return b.QuestionId==a.QuestionId;});
                            var mycode = string.Join(",",myop.Select(b => { return b.Option.Code; }));
                            questionsb.AppendFormat(_questionAnswer, mycode, string.IsNullOrWhiteSpace(mycode) ? "display:none" : "");
                        }
                        else if (a.SortNo2 == 1)
                        {
                            questionsb.AppendFormat(_questionTitle, i + 1, a.Question.NContent);
                            if (a.Question.Image != null)
                            {
                                questionsb.AppendFormat(_questionImg, a.Question.Image.FullName.Substring(1));
                            }
                            questionsb.AppendFormat(_questionTitle, a.SortNo2, a.Question.Title);
                            var myop = plan.BusTestPlanUserOptions.Where(b => { return b.QuestionId == a.QuestionId; });
                            var mycode = string.Join(",", myop.Select(b => { return b.Option.Code; }));
                            questionsb.AppendFormat(_questionAnswer, mycode, string.IsNullOrWhiteSpace(mycode) ? "display:none" : "");
                        }
                        else
                        {
                            questionsb.AppendFormat(_questionTitle, a.SortNo2, a.Question.Title);
                            var myop = plan.BusTestPlanUserOptions.Where(b => { return b.QuestionId == a.QuestionId; });
                            var mycode = string.Join(",", myop.Select(b => { return b.Option.Code; }));
                            questionsb.AppendFormat(_questionAnswer, mycode, string.IsNullOrWhiteSpace(mycode) ? "display:none" : "");
                        }
                    });
                    var question = questionsb.ToString();
                    #endregion

                    #region 结果
                    var resultsb=new StringBuilder();

                    resultsb.AppendFormat(_resultAnswer, planUser.FinishQuestion);
                    if(plan.BusTestPlanUserExamples.Count((a)=>{ return a.ResultCode != null; }) <= 0)
                    {
                        resultsb.AppendFormat(_resultAnswer, _resultNothing);
                    }
                    else
                    {
                        var resultArr = plan.BusTestPlanUserExamples.SelectMany((a, i) => {
                            if (a.Result == null)
                            {
                                return new List<KeyValuePair<int, BusExampleResult>> { new KeyValuePair<int, BusExampleResult>(a.Score,a.Result) };
                            }
                            else
                            {
                                var srcs = a.Scores.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(b=>int.TryParse(b,out int intt)).Select(int.Parse).ToArray();
                                int j = 0;
                                var rlts = a.ResultIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(b=>int.TryParse(b,out int intt)).Select(int.Parse).Select(b => {
                                    return new KeyValuePair<int, BusExampleResult>(srcs[j++], results.FirstOrDefault(c => c.Id == b));
                                });
                                return rlts;
                            }
                        }).ToList();

                        resultArr.ForEach((a, i) => {
                            resultsb.AppendFormat(_resultScore,i+1,a.Key);
                        });
                        resultArr.ForEach((a, i) => {
                            var imghtml = "";
                            if (a.Value.Image != null)
                            {
                                imghtml = string.Format(_questionImg,a.Value.Image.FullName.Substring(1));
                            }
                            resultsb.AppendFormat(_resultDesc, a.Value.Title,a.Value.NContent, imghtml);
                        });
                    }

                    var result = resultsb.ToString();
                    #endregion

                    var detail = string.Format(_detail,question,result, plan.Template.NContent,plan.Template.Note);

                    #region 结尾表格
                    var tail = string.Format(_tail);
                    #endregion

                    var html = string.Format(_html,header,detail,tail);
                    var eles = HtmlConverter.ConvertToElements(html, properties);
                    eles.ForEach((ele, i) => doc.Add((IBlockElement)ele));
                });
                writer.SetCloseStream(false);
                
                doc.Close();
                pdf.Close();
                writer.Close();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
