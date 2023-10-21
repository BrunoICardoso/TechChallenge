using System.Net;

namespace BurgerRoyale.Domain.Interface.ResponseDefault
{
	public interface IReturnAPI
	{
		bool IsSuccessStatusCode { get; }
		bool IsNoContentStatusCode { get; }
		HttpStatusCode StatusCode { get; }
		string Message { get; }
		Exception Exception { get; }
		Dictionary<string, string[]> ModelState { get; }
	}
	public interface IReturnAPI<TData>
	{
		TData Data { get; }
	}
}
