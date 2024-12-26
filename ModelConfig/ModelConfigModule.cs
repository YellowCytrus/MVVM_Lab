using DataAccessLayer;
using DomainObjects;
using Ninject.Modules;
using Model;
using Blogic;

namespace ModelConfig
{
    public class ModelConfigModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IRepository<Student>>().To<EntityFrameworkRepository>().InSingletonScope();
            Bind<IRepository<Student>>().To<DapperRepository>().InSingletonScope();
            Bind<IModel>().To<Logic>().InSingletonScope();
        }
    }
}
