using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class Page<T>
    {
        public IList<T> Items { get; }
        public int TotalAmount { get; }
        public int PageNumber { get; }
        public int ItemsPerPage { get; }
        public int TotalPages { get; }

        public Page(IList<T> items, int totalAmount, int pageNumber, int itemsPerPage, int totalPages) {
            Items = items;
            TotalAmount = totalAmount;
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
            TotalPages = totalPages;
        }
    }
}
