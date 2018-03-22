using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:NewsInfo
    /// </summary>
    public partial class NewsInfo
    {
        public NewsInfo()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId(string where = "")
        {
            string sql = "select max(id) from NewsInfo";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += " where " + where;
            }
            object obj = DbHelperSQL.GetSingle(sql);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 得到最小ID
        /// </summary>
        public int GetMinId(string where = "")
        {
            string sql = "select min(id) from NewsInfo";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += " where " + where;
            }
            object obj = DbHelperSQL.GetSingle(sql);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from NewsInfo");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from NewsInfo n");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 返回总浏览量
        /// </summary>
        public int GetSumClick(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Click) as H ");
            strSql.Append(" from NewsInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into NewsInfo(");
            strSql.Append("Title,Author,ClassId,Content,ImgUrl,IsImage,Click,IsTop,IsLock,PubTime,Keyword,Description,AssId,ZxType,Tags,Address,UserName,DesignerId,fgID,hxID,ZxKind,gdStatus,Area,AreaId,Price)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Author,@ClassId,@Content,@ImgUrl,@IsImage,@Click,@IsTop,@IsLock,@PubTime,@Keyword,@Description,@AssId,@ZxType,@Tags,@Address,@UserName,@DesignerId,@fgID,@hxID,@ZxKind,@gdStatus,@Area,@AreaId,@Price)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,100),
                    new SqlParameter("@Author", SqlDbType.NVarChar,50),
                    new SqlParameter("@ClassId", SqlDbType.Int,4),
                    new SqlParameter("@Content", SqlDbType.NText),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@IsImage", SqlDbType.Int,4),
                    new SqlParameter("@Click", SqlDbType.Int,4),
                    new SqlParameter("@IsTop", SqlDbType.Int,4),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@Keyword", SqlDbType.NVarChar,200),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@AssId", SqlDbType.Int,4),
                    new SqlParameter("@ZxType", SqlDbType.Int,4),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,200),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@DesignerId", SqlDbType.Int,4),
                    new SqlParameter("@fgID", SqlDbType.Int,4),
                    new SqlParameter("@hxID", SqlDbType.Int,4),
                    new SqlParameter("@ZxKind", SqlDbType.Int,4),
                    new SqlParameter("@gdStatus", SqlDbType.Int,4),
                    new SqlParameter("@Area", SqlDbType.Int,4),
                    new SqlParameter("@AreaId", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Decimal)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Author;
            parameters[2].Value = model.ClassId;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.ImgUrl;
            parameters[5].Value = model.IsImage;
            parameters[6].Value = model.Click;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IsLock;
            parameters[9].Value = model.PubTime;
            parameters[10].Value = model.Keyword;
            parameters[11].Value = model.Description;
            parameters[12].Value = model.AssId;
            parameters[13].Value = model.ZxType;
            parameters[14].Value = model.Tags;
            parameters[15].Value = model.Address;
            parameters[16].Value = model.UserName;
            parameters[17].Value = model.DesignerId;
            parameters[18].Value = model.fgID;
            parameters[19].Value = model.hxID;
            parameters[20].Value = model.ZxKind;
            parameters[21].Value = model.gdStatus;
            parameters[22].Value = model.Area;
            parameters[23].Value = model.AreaId;
            parameters[24].Value = model.Price;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NewsInfo set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NewsInfo set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Author=@Author,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("Content=@Content,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("IsImage=@IsImage,");
            strSql.Append("Click=@Click,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Description=@Description,");
            strSql.Append("AssId=@AssId,");
            strSql.Append("ZxType=@ZxType,");
            strSql.Append("Tags=@Tags,");
            strSql.Append("Address=@Address,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("DesignerId=@DesignerId,");
            strSql.Append("fgID=@fgID,");
            strSql.Append("hxID=@hxID,");
            strSql.Append("ZxKind=@ZxKind,");
            strSql.Append("gdStatus=@gdStatus,");
            strSql.Append("Area=@Area,");
            strSql.Append("AreaId=@AreaId,");
            strSql.Append("Price=@Price");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,100),
                    new SqlParameter("@Author", SqlDbType.NVarChar,50),
                    new SqlParameter("@ClassId", SqlDbType.Int,4),
                    new SqlParameter("@Content", SqlDbType.NText),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@IsImage", SqlDbType.Int,4),
                    new SqlParameter("@Click", SqlDbType.Int,4),
                    new SqlParameter("@IsTop", SqlDbType.Int,4),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Keyword", SqlDbType.NVarChar,200),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@AssId", SqlDbType.Int,4),
                    new SqlParameter("@ZxType", SqlDbType.Int,4),
                    new SqlParameter("@Tags", SqlDbType.NVarChar,200),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@DesignerId", SqlDbType.Int,4),
                    new SqlParameter("@fgID", SqlDbType.Int,4),
                    new SqlParameter("@hxID", SqlDbType.Int,4),
                    new SqlParameter("@ZxKind", SqlDbType.Int,4),
                    new SqlParameter("@gdStatus", SqlDbType.Int,4),
                    new SqlParameter("@Area", SqlDbType.Int,4),
                    new SqlParameter("@AreaId", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Decimal)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Author;
            parameters[2].Value = model.ClassId;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.ImgUrl;
            parameters[5].Value = model.IsImage;
            parameters[6].Value = model.Click;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IsLock;
            parameters[9].Value = model.PubTime;
            parameters[10].Value = model.Id;
            parameters[11].Value = model.Keyword;
            parameters[12].Value = model.Description;
            parameters[13].Value = model.AssId;
            parameters[14].Value = model.ZxType;
            parameters[15].Value = model.Tags;
            parameters[16].Value = model.Address;
            parameters[17].Value = model.UserName;
            parameters[18].Value = model.DesignerId;
            parameters[19].Value = model.fgID;
            parameters[20].Value = model.hxID;
            parameters[21].Value = model.ZxKind;
            parameters[22].Value = model.gdStatus;
            parameters[23].Value = model.Area;
            parameters[24].Value = model.AreaId;
            parameters[25].Value = model.Price;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NewsInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NewsInfo ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.NewsInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Title,Author,ClassId,Content,ImgUrl,IsImage,Click,IsTop,IsLock,PubTime,Keyword,Description,AssId,ZxType,Tags,Address,UserName,DesignerId,fgID,hxID,ZxKind,gdStatus,Area,AreaId,Price from NewsInfo ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = Id;

            ZGD.Model.NewsInfo model = new ZGD.Model.NewsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Keyword"] != null && ds.Tables[0].Rows[0]["Keyword"].ToString() != "")
                {
                    model.Keyword = ds.Tables[0].Rows[0]["Keyword"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Description"] != null && ds.Tables[0].Rows[0]["Description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Author"] != null && ds.Tables[0].Rows[0]["Author"].ToString() != "")
                {
                    model.Author = ds.Tables[0].Rows[0]["Author"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClassId"] != null && ds.Tables[0].Rows[0]["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Content"] != null && ds.Tables[0].Rows[0]["Content"].ToString() != "")
                {
                    model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImgUrl"] != null && ds.Tables[0].Rows[0]["ImgUrl"].ToString() != "")
                {
                    model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsImage"] != null && ds.Tables[0].Rows[0]["IsImage"].ToString() != "")
                {
                    model.IsImage = int.Parse(ds.Tables[0].Rows[0]["IsImage"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Click"] != null && ds.Tables[0].Rows[0]["Click"].ToString() != "")
                {
                    model.Click = int.Parse(ds.Tables[0].Rows[0]["Click"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsTop"] != null && ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsLock"] != null && ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PubTime"] != null && ds.Tables[0].Rows[0]["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(ds.Tables[0].Rows[0]["PubTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AssId"] != null && ds.Tables[0].Rows[0]["AssId"].ToString() != "")
                {
                    model.AssId = int.Parse(ds.Tables[0].Rows[0]["AssId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZxType"] != null && ds.Tables[0].Rows[0]["ZxType"].ToString() != "")
                {
                    model.ZxType = int.Parse(ds.Tables[0].Rows[0]["ZxType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Tags"] != null && ds.Tables[0].Rows[0]["Tags"].ToString() != "")
                {
                    model.Tags = ds.Tables[0].Rows[0]["Tags"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DesignerId"] != null && ds.Tables[0].Rows[0]["DesignerId"].ToString() != "")
                {
                    model.DesignerId = int.Parse(ds.Tables[0].Rows[0]["DesignerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["fgID"] != null && ds.Tables[0].Rows[0]["fgID"].ToString() != "")
                {
                    model.fgID = int.Parse(ds.Tables[0].Rows[0]["fgID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hxID"] != null && ds.Tables[0].Rows[0]["hxID"].ToString() != "")
                {
                    model.hxID = int.Parse(ds.Tables[0].Rows[0]["hxID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZxKind"] != null && ds.Tables[0].Rows[0]["ZxKind"].ToString() != "")
                {
                    model.ZxKind = int.Parse(ds.Tables[0].Rows[0]["ZxKind"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gdStatus"] != null && ds.Tables[0].Rows[0]["gdStatus"].ToString() != "")
                {
                    model.gdStatus = int.Parse(ds.Tables[0].Rows[0]["gdStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Area"] != null && ds.Tables[0].Rows[0]["Area"].ToString() != "")
                {
                    model.Area = int.Parse(ds.Tables[0].Rows[0]["Area"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AreaId"] != null && ds.Tables[0].Rows[0]["AreaId"].ToString() != "")
                {
                    model.AreaId = int.Parse(ds.Tables[0].Rows[0]["AreaId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得标签数据列表
        /// </summary>
        public DataSet GetTagList(int top = 20)
        {
            string sql = "select top " + top + " Tags from newsinfo order by click desc";
            return DbHelperSQL.Query(sql);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,Author,ClassId,Content,ImgUrl,IsImage,Click,IsTop,IsLock,PubTime,Keyword,Description,AssId,ZxType,Tags,Address,UserName,DesignerId,fgID,hxID,ZxKind,gdStatus,Area,AreaId,Price ");
            strSql.Append(" FROM NewsInfo");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetGongDiList(int top = 0, string strWhere = "", string order = "")
        {
            StringBuilder strSql = new StringBuilder();
            if (top <= 0)
            {
                strSql.Append("select n.Id,n.Title,n.Author,n.ClassId,n.Content,n.ImgUrl,n.IsImage,n.Click,n.IsTop,n.IsLock,n.PubTime,n.Keyword,n.Description,n.AssId,n.ZxType,n.Tags,n.Address,h.Title as HouseName,h.Address as hAddress,n.UserName,n.DesignerId,n.gdStatus,n.Area,d.Name as DesignerName,n.fgID,n.hxID,n.ZxKind,c.Title as fg,hou.Title as hx,qy.Title as qy ");
            }
            else
            {
                strSql.Append("select top " + top + " n.Id,n.Title,n.Author,n.ClassId,n.Content,n.ImgUrl,n.IsImage,n.Click,n.IsTop,n.IsLock,n.PubTime,n.Keyword,n.Description,n.AssId,n.ZxType,n.Tags,n.Address,h.Title as HouseName,h.Address as hAddress,n.UserName,n.DesignerId,n.gdStatus,n.Area,d.Name as DesignerName,n.fgID,n.hxID,n.ZxKind,c.Title as fg,hou.Title as hx,qy.Title as qy,jd.Title as jd ");
            }
            strSql.Append(" FROM NewsInfo n left join House h on n.AssId=h.ID left join Designer d on n.DesignerId=d.ID left join Channel c on n.fgID=c.Id left join Channel hou on n.hxID=hou.Id left join Channel qy on n.AreaId=qy.Id left join Channel jd on n.gdStatus=jd.Id");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" where " + strWhere + " and n.ZxType=2");
            }
            else
            {
                strSql.Append(" where n.ZxType=2");
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                strSql.Append(" order by " + order);
            }
            else
            {
                strSql.Append(" order by n.Id desc");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,Title,Author,ClassId,Content,ImgUrl,IsImage,Click,IsTop,IsLock,PubTime,Keyword,Description,AssId,Tags,Address,UserName,DesignerId,fgID,hxID,ZxKind,gdStatus,Area,AreaId,Price ");
            strSql.Append(" FROM NewsInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " NewsInfo ";
            pager.fieldsName = " * ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " IsTop desc,PubTime desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and IsLock=0";
            else
                pager.sqlWhere = " IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList_GD(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " NewsInfo p left join House hs on p.AssId=hs.ID left join Channel c on p.fgID=c.Id left join Channel h on p.hxID=h.Id left join Channel qy on p.AreaId=qy.Id left join Channel jd on p.A=jd.Id ";
            pager.fieldsName = " p.*,hs.Title as HouseTitle,c.Title as fg,h.Title as hx,qy.Title as qy,jd.Title as jd  ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " p.IsTop desc, p.PubTime desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and p.IsLock=0";
            else
                pager.sqlWhere = " p.IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList_M(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " NewsInfo p left join Channel c on p.fgID=c.Id left join Channel h on p.hxID=h.Id left join Channel qy on p.AreaId=qy.Id left join Channel jd on p.gdStatus=jd.Id";
            pager.fieldsName = " p.*,c.Title as fg,h.Title as hx ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " p.IsTop desc, p.PubTime desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and p.IsLock=0";
            else
                pager.sqlWhere = " p.IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "NewsInfo";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}