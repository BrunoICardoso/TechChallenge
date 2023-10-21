using BurgerRoyale.Domain.Interface.ResponseDefault;

namespace BurgerRoyale.Domain.ResponseDefault
{
	public class ReturnAPIDataTable<T> : ReturnAPI, IReturnDataTable<T>
	{
		private readonly FilterPage _filterPage;
		private int _totalPages;
		private int _totalRecords;

		public ReturnAPIDataTable(FilterPage filterPage)
		{
			_filterPage = filterPage;
		}

		public int TotalRecords
		{
			get
			{
				return _totalRecords;
			}
			set
			{
				_totalRecords = value;
				TotalPages = _totalRecords;
			}
		}
		public int TotalPages
		{
			get
			{
				return _totalPages;
			}
			set
			{

				_totalPages = (value + _filterPage.Size - 1) / _filterPage.Size;

			}
		}
		public T Data { get; set; }
	}

	public class ReturnAPIDataTableResponse<T> : ReturnAPI, IReturnDataTable<T>
	{
		public int TotalRecords { get; set; }

		public int TotalPages { get; set; }

		public T Data { get; set; }
	}

	public class ReturnAPIDataTableProcess<T> : ReturnAPIDataTable<T>
	{
		public ReturnAPIDataTableProcess(FilterPage filterPage) : base(filterPage)
		{
		}

		public bool IsProcess { get; set; } = false;
	}
}
