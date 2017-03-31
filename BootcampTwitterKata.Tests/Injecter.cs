using Ninject;

namespace BootcampTwitterKata.Tests
{
    public class Injecter
    {
        private StandardKernel standardKernel;

        public Injecter()
        {
            standardKernel = new StandardKernel();
        }

        public T Get<T>()
        {
            return this.standardKernel.Get<T>();
        }

        public Injecter Bind<T>(T bus)
        {
            this.standardKernel.Bind<T>().ToConstant(bus);
            return this;
        }

        public Injecter Bind<T, TTo>()
            where TTo : T
        {
            this.standardKernel.Bind<T>().To<TTo>();
            return this;
        }
    }
}