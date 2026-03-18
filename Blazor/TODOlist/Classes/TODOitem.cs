namespace TODOlist.Classes
{
	public class TODOitem//:IComparable<TODOitem>,IEqualityComparer<TODOitem>
	{
		public string Title { get; set; }
		public bool DONE { get; set; }
		//public static bool operator ==(TODOitem left, TODOitem right)
		//{
		//	return left.Title == right.Title;
		//}
		//public static bool operator !=(TODOitem left, TODOitem right)
		//{
		//	return left.Title != right.Title;
		//}
		///////////////////////////////////////
		//public int CompareTo(TODOitem other)
		//{ 
		//	return this.Title.CompareTo(other.Title);
		//}
		//public bool Equals(TODOitem left, TODOitem right)
		//{
		//	return left.Title == right.Title;
		//}
		//public int GetHashCode(TODOitem other)
		//{
		//	//return other.Title.GetHashCode();
		//	return HashCode.Combine(Title);
		//}
		//////////////////////////////////////////////////////////////////////
		//public override bool Equals(object? other)
		//{
		//	return this.Title.Equals((other as TODOitem).Title);
		//}
		//public override int GetHashCode()
		//{
		//	return HashCode.Combine(Title);
		//}
	}
}
