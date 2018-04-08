using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGD.Model;

namespace ZGD.MySqlToMsSql
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("回车开始！");

            var my_Data_news = DataHelper.Query<article>("select * from article", true);
            var my_Data_news_col = DataHelper.Query<articleCol>("select * from columnofarticle", true);
            var my_Data_news_img = DataHelper.Query<articleImage>("select * from image", true);

            foreach (var item in my_Data_news)
            {
                Task.Factory.StartNew(() =>
                {
                    string sql = "insert into NewsInfo (Id,Title,Keyword,Tags,Description,Author,ClassId,Content,ImgUrl,IsImage,Click,IsTop,IsLock,PubTime,UserId,PubUnit,SubTitle) values (@Id,@Title,@Keyword,@Tags,@Description,@Author,@ClassId,@Content,@ImgUrl,@IsImage,@Click,@IsTop,@IsLock,@PubTime,@UserId,@PubUnit,@SubTitle)";
                    string colStr = string.Empty, imgStr = string.Empty, sql_col = string.Empty, sql_imgs = string.Empty;

                    var cols = my_Data_news_col.Where(a => a.articleId == item.articleId);
                    if (cols.Count() > 0)
                    {
                        colStr = string.Join(",", cols.Select(a => a.columnId).ToArray());
                        foreach (var item_col in cols)
                        {
                            sql_imgs += "insert into NewsColumns (Id,NewsId,ClassId,PubTime) values (" + item_col.articleColumnRelId + "," + item_col.articleId + "," + item_col.columnId + ",'" + item.submitTime + "'); ";
                        }
                    }

                    var imgs = my_Data_news_img.Where(a => a.articleId == item.articleId);
                    if (imgs.Count() > 0)
                    {
                        imgStr = string.Join(",", imgs.Select(a => a.imgSrc).ToArray());
                        foreach (var item_img in imgs)
                        {
                            sql_imgs += "insert into NewsImages (Id,NewsId,ImgSrc,ImgNote,PubTime) values (" + item_img.imgId + "," + item_img.articleId + ",'" + item_img.imgSrc + "','" + item_img.imgNote + "','" + item_img.createTime + "'); ";
                        }
                    }

                    if (DataHelper.Execute(sql, new NewsInfo
                    {
                        Id = item.articleId,
                        Title = item.articleTitle,
                        SubTitle = item.articleSubhead,
                        Keyword = item.keyWord,
                        Tags = item.articleTitle,
                        Description = item.articleTitle,
                        Author = item.author,
                        ClassId = colStr,
                        Content = item.articleContent,
                        ImgUrl = imgStr,
                        IsImage = string.IsNullOrWhiteSpace(imgStr) ? 0 : 1,
                        Click = item.viewTimes.Value,
                        IsTop = item.isTop.Value,
                        IsLock = 0,
                        PubTime = item.submitTime,
                        UserId = item.userId.Value,
                        PubUnit = item.submitUnit,

                    }) > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(sql_col))
                            DataHelper.Execute(sql_col);

                        if (!string.IsNullOrWhiteSpace(sql_imgs))
                            DataHelper.Execute(sql_imgs);

                        Console.WriteLine("成功：" + item.articleId + "\n ");
                    }
                });
                Task.WaitAll();
            }
        }
    }
}
