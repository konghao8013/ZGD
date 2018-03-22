using System;
namespace ZGD.Model
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private int _id;
		private string _nickname;
		private string _phone;
		private string _password;
		private int? _islock=0;
		private DateTime? _pubtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
        }
        public string RegIP { get; set; }
        #endregion Model
    }

    [Serializable]
    public class RequestUser
    {
        public string Nickname { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsImgCode { get; set; }
        public string ImgCode { get; set; }
        public bool IsSmsCode { get; set; }
        public string SmsCode { get; set; }
        public bool IsRem { get; set; }
        public bool IsAutoLogin { get; set; }       

    }
}

