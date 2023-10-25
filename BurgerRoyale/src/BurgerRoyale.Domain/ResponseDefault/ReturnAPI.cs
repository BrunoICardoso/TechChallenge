using BurgerRoyale.Domain.Interface.ResponseDefault;
using System.Net;
using System.Text.Json.Serialization;

namespace BurgerRoyale.Domain.ResponseDefault
{
	public class ReturnAPI : IReturnAPI
	{
		public bool IsSuccessStatusCode
		{
			get { return (int)StatusCode >= 200 && (int)StatusCode <= 299; }
		}

		[JsonIgnore]
		public bool IsNoContentStatusCode
		{
			get { return (int)StatusCode == 204; }
		}

		public string Message { get; set; }
		private Exception _exception;

		public Dictionary<string, string[]> ModelState { get; set; }

		[JsonIgnore]
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

		[JsonIgnore]
		public Exception Exception
		{
			get
			{
				return _exception;
			}
			set
			{
				if (value != null)
				{
					StatusCode = HttpStatusCode.InternalServerError;
					_exception = value;
				}
			}
		}

		public ReturnAPI()
		{
		}

		public ReturnAPI(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}
	}

	public class ReturnAPI<TData> : ReturnAPI, IReturnAPI<TData>
	{
		public TData Data { get; set; }

		public ReturnAPI(TData data)
		{
			Data = data;
		}

        public ReturnAPI(HttpStatusCode statusCode, TData data)
		{
			StatusCode = statusCode;
			Data = data;
		}
	}
}
