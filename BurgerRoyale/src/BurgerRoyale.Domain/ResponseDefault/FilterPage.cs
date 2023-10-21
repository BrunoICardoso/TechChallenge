namespace BurgerRoyale.Domain.ResponseDefault
{
	public class FilterPage
	{
		public int Size { get; set; }

		public int Skip { get; set; }
	}

	public class FilterPage<TClassFilter> : FilterPage where TClassFilter : class
	{
		public TClassFilter Filter { get; set; }
	}

}
