using System;
namespace ZGD.Model
{
	/// <summary>
	/// ProjectImg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProjectImg
	{
		public ProjectImg()
		{}
        #region Model
        private int _piid;
        private int _pid;
		private string _title;
		private string _imgurl;
		private string _imagesmall;
        private DateTime _pubtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int piID
        {
            set { _piid = value; }
            get { return _piid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pID
        {
            set { _pid = value; }
            get { return _pid; }
        }
		/// <summary>
		/// 项目标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 图片路径
		/// </summary>
		public string ImgUrl
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// 图片缩略图
		/// </summary>
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
		}
		#endregion Model

	}
}

