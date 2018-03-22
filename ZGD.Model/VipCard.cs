using System;
namespace ZGD.Model
{
	/// <summary>
	/// VipCard:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class VipCard
	{
		public VipCard()
		{}
		#region Model
		private string _cardno;
		private string _openid;
		private int? _status=0;
		private DateTime? _pubtime= DateTime.Now;
		private DateTime? _bindtime= Convert.ToDateTime("2000-1-1");
		/// <summary>
		/// 
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OpenId
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BindTime
		{
			set{ _bindtime=value;}
			get{return _bindtime;}
		}
		#endregion Model

	}
}

