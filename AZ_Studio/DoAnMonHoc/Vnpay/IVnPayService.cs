using DoAnMonHoc.ViewModel;

namespace DoAnMonHoc.Vnpay
{
	public interface IVnPayService
	{
		string InitialPayment(HttpContext context, OrderInfo model);
		VnpayResponse ExecutePayment(IQueryCollection collections);
	}
}
