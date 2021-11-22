using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using System.Threading.Tasks;

namespace storeBooks.repository.interfaces
{
    public interface ISalesBookRepository : IRepository<SalesBookStoreModel>
    {
        public SalesBook BuyMade(SalesBook sale, string currency);
        public SalesBookStoreModel[] TakeAllShopping();
    }
}
