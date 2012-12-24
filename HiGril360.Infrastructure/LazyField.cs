using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiGirl360.Infrastructure
{
    public class LazyField<T>
    {
        private T value;
        private Func<T> loader;
        private Func<T, T> setter;

        public LazyField()
            : this(null, null) { }

        public LazyField(Func<T> loader)
            : this(loader, null) { }

        public LazyField(Func<T, T> setter)
            : this(null, setter) { }

        public LazyField(Func<T> loader, Func<T, T> setter)
        {
            this.loader = loader;
            this.setter = setter;
            this.value = default(T);
        }

        public bool IsLoaded
        {
            get { return this.loader == null; }
        }

        public T Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public void Loader(Func<T> loader)
        {
            this.loader = loader;
        }

        public void Setter(Func<T, T> setter)
        {
            this.setter = setter;
        }

        private T GetValue()
        {
            if (!this.IsLoaded)
            {
                value = loader();
                loader = null;
            }
            return value;
        }

        private void SetValue(T value)
        {
            loader = null;

            this.value = setter != null
                ? this.setter(value)
                : value;
        }
    }
}
