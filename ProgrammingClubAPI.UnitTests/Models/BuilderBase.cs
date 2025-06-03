using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingClubAPI.UnitTests.Models
{
    public abstract class BuilderBase<T>
    {
        protected T _objectToBuild;

        public T Build()
        {
            return _objectToBuild;
        }

        public BuilderBase<T> With(Action<T> setter)
        {
            setter(_objectToBuild);
            return this;
        }
    }
}
