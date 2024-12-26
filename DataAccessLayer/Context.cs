using DomainObjects;
using System.Data.Entity;

namespace DataAccessLayer
{
    internal class Context : DbContext
    {
        public Context() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=C:\Users\SadCy\source\repos\DataAccessLayer\Database.mdf;
                                Integrated Security=True")
        {
            System.Data.Entity.SqlServer.SqlProviderServices ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<Student> Students { get; set; }
    }
}
