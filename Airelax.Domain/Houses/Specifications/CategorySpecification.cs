using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines;

namespace Airelax.Domain.Houses.Specifications
{
    public class CategorySpecification : Specification<House>
    {
        private readonly IEnumerable<Category> _categories;

        public CategorySpecification(IEnumerable<Category> categories)
        {
            _categories = categories;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => _categories.Contains(house.HouseCategory.Category);
        }
    }
}
