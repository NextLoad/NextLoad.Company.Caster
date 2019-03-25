using System;
namespace Caster.Model
{
	/// <summary>
	/// ManagerInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ManagerInfo
	{
		public ManagerInfo()
		{}
		#region Model
		private int _mid;
		private string _mname;
		private string _mpwd;
		private int? _mtype;
		/// <summary>
		/// 
		/// </summary>
		public int MId
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MName
		{
			set{ _mname=value;}
			get{return _mname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MPwd
		{
			set{ _mpwd=value;}
			get{return _mpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MType
		{
			set{ _mtype=value;}
			get{return _mtype;}
		}
		#endregion Model

	}
}

