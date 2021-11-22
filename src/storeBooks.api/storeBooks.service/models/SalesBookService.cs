using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using storeBooks.service.interfaces;

namespace storeBooks.service.models
{
    public class SalesBookService : ISalesBookService
    {
        private readonly ISalesBooksBusiness _salesBookBusiness;

        public SalesBookService(ISalesBooksBusiness salesBookBusiness)
        {
            _salesBookBusiness = salesBookBusiness;
        }

        public SalesBook BuyMade(SalesBook sale, string currency)
        {
            return _salesBookBusiness.BuyMade(sale, currency);
        }

        public SalesBookStoreModel[] TakeAllShopping()
        {
            return _salesBookBusiness.TakeAllShopping();
        }
    }
}
