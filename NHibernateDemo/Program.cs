using System;
using System.Linq;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NHibernateDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string host = "192.168.10.10";
            const string port = "3306";
            const string database = "NHibernateDemo";
            const string username = "homestead";
            const string password = "secret";
            const string sslMode = "none";
            
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = $"server={host};" +
                                     $"port={port};" +
                                     $"database={database};" +
                                     $"user={username};" +
                                     $"password={password};" +
                                     $"sslmode={sslMode};";
                
                x.ConnectionProvider<DriverConnectionProvider>();
                x.Driver<MySqlDataDriver>();
                x.Dialect<MySQL5Dialect>();
                x.LogSqlInConsole = true;
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = cfg.BuildSessionFactory();
            
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                // Create a new customer class.
                var newCustomer = new Customer {FirstName = "Erdem", LastName = "Keren"};
                
                session.Save(newCustomer);
                
                var query = from cstmr in session.Query<Customer>()
                    where cstmr.Id == newCustomer.Id
                    select cstmr;

                var customer = query.First();
                
                Console.WriteLine("{0} {1} [{2}]", customer.FirstName, customer.LastName, customer.Id);

                customer.FirstName = "Hilmi Erdem";
                
                session.Save(customer);
                Console.WriteLine("Customer {0} updated in the database.", customer.Id);

                session.Delete(customer);
                Console.WriteLine("Customer {0} deleted from the database.", customer.Id);
                
                tx.Commit();
            }
        }
    }
}