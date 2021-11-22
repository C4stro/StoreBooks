using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using System.Threading.Tasks;

namespace storeBooks.business.interfaces
{
    public interface ISalesBooksBusiness
    {
        public SalesBook BuyMade(SalesBook sale, string currency);
        public SalesBookStoreModel[] TakeAllShopping();
    }
}