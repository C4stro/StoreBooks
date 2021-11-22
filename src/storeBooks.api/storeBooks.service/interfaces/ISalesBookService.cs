using storeBooks.domain.models;
using storeBooks.infra.data.Dto;

namespace storeBooks.service.interfaces
{
    public interface ISalesBookService
    {
        public SalesBook BuyMade(SalesBook sale, string currency);
        public SalesBookStoreModel[] TakeAllShopping();
    }
}
