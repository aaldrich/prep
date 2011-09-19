using System;
using System.Collections.Generic;

namespace nothinbutdotnetprep.utility.filtering
{
    public class ComparableMatchFactory<ItemToFilter, PropertyType> where PropertyType : IComparable<PropertyType>
    {
        Func<ItemToFilter, PropertyType> accessor;
        MatchFactory<ItemToFilter, PropertyType> match_factory;

        public ComparableMatchFactory(Func<ItemToFilter, PropertyType> accessor)
        {
            this.accessor = accessor;
            this.match_factory = new MatchFactory<ItemToFilter, PropertyType>(this.accessor);
        }


        public IMatchA<ItemToFilter> greater_than(PropertyType value)
        {
            return new AnonymousMatch<ItemToFilter>(x => accessor(x).CompareTo(value) > 0);
        }

        public IMatchA<ItemToFilter> between(PropertyType start,PropertyType end)
        {
            return new AnonymousMatch<ItemToFilter>(x => accessor(x).CompareTo(start) >= 0 && accessor(x).CompareTo(end) <= 0);
        }

        public IMatchA<ItemToFilter> equal_to(PropertyType value)
        {
            return match_factory.equal_to(value);
        }

        public IMatchA<ItemToFilter> equal_to_any(params PropertyType[] values)
        {
            return match_factory.equal_to_any(values);
        }

        public IMatchA<ItemToFilter> not_equal_to(PropertyType value)
        {
            return match_factory.not_equal_to(value);
        }
    }
}