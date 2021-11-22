using storeBooks.business.interfaces;
using storeBooks.domain.models;
using storeBooks.infra.data.Dto;
using storeBooks.repository.interfaces;
using System;
using System.Threading.Tasks;

namespace storeBooks.business.models
{
    public class SalesBooksBusiness : ISalesBooksBusiness
    {
        private readonly ISalesBookRepository _salesBookRepository;

        public SalesBooksBusiness(ISalesBookRepository salesBookRepository)
        {
            _salesBookRepository = salesBookRepository;
        }

        public SalesBook BuyMade(SalesBook sale, string currency)
        {
            return _salesBookRepository.BuyMade(sale, currency);
        }

        public SalesBookStoreModel[] TakeAllShopping()
        {
            return _salesBookRepository.TakeAllShopping();
        }
    }
}
