using System;
namespace ZGD.Model
{
	/// <summary>
	/// ProjectImg:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ��Ŀ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// ͼƬ·��
		/// </summary>
		public string ImgUrl
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// ͼƬ����ͼ
		/// </summary>
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
		}
		#endregion Model

	}
}

