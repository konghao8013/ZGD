using System;
using System.Collections.Generic;
namespace ZGD.Model
{
    /// <summary>
    /// Designer:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Designer
    {
        public Designer()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _keyword;
        private string _description;
        private string _name;
        private string _cname;
        private string _Rongyu;
        private string _School;
        private string _photo;
        private string _des;
        private DateTime? _AddDate = DateTime.Now;
        private int _cyDate;
        private string _GoodAt;
        private string _GoodAtDes;
        private string _QQ;
        private int _IsTop;
        /// <summary>
        /// 从业时间
        /// </summary>
        public int cyDate
        {
            set { _cyDate = value; }
            get { return _cyDate; }
        }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DeptId
        {
            set;
            get;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort
        {
            set;
            get;
        }
        public int Star { set; get; }
        public int Clicks { set; get; }
        /// <summary>
        /// 擅长风格
        /// </summary>
        public string GoodAt
        {
            set { _GoodAt = value; }
            get { return _GoodAt; }
        }
        /// <summary>
        /// 擅长设计
        /// </summary>
        public string GoodAtDes
        {
            set { _GoodAtDes = value; }
            get { return _GoodAtDes; }
        }
        /// <summary>
        /// 荣誉
        /// </summary>
        public string Rongyu
        {
            set { _Rongyu = value; }
            get { return _Rongyu; }
        }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string School
        {
            set { _School = value; }
            get { return _School; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _QQ = value; }
            get { return _QQ; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsTop
        {
            set { _IsTop = value; }
            get { return _IsTop; }
        }
        /// <summary>
        /// 设计理念
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 职称
        /// </summary>
        public string cName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddDate
        {
            set { _AddDate = value; }
            get { return _AddDate; }
        }

        public List<Project> DesCase { set; get; }
        #endregion Model

    }
}

