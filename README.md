This console application utilizes dotnet core, NHibernate and MySQL server to manage customer data.

Also see [my blog post](https://erdemkeren.com/blog/category/net/article/nhibernate-api) for more details.

# Installation

First, clone the repository:

git clone https://github.com/erdemkeren/nhibernate-demo

Using cli, inside the folder created by the git command `cd nhibernate-demo`, execute the following command:

`$ dotnet restore`

Finally, create database and run the following `ddl` to create the Customer table:

```sql
CREATE TABLE `Customer` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;
```

# Usage

Open `Program.cs` in your favorite editor (I did not include any configuration object or environment file 
to keep things simple), configure the connection string parameters:

```csharp
const string host = "192.168.10.10";
const string port = "3306";
const string database = "NHibernateDemo";
const string username = "homestead";
const string password = "secret";
const string sslMode = "none";
```

and then using cli run:

`$ dotnet run --project ./NHibernateDemo/NHibernateDemo.csproj`

You will see the output of the program (e.g.):

```
NHibernate: INSERT INTO Customer (FirstName, LastName) VALUES (?p0, ?p1);SELECT LAST_INSERT_ID();?p0 = 'Erdem' [Type: String (5:0:0)], ?p1 = 'Keren' [Type: String (5:0:0)]
NHibernate: select customer0_.Id as id1_0_, customer0_.FirstName as firstname2_0_, customer0_.LastName as lastname3_0_ from Customer customer0_ where customer0_.Id=?p0 limit 1;?p0 = 13 [Type: Int32 (0:0:0)]
Erdem Keren [13]
Customer 13 updated in the database.
Customer 13 deleted from the database.
NHibernate: DELETE FROM Customer WHERE Id = ?p0;?p0 = 13 [Type: Int32 (0:0:0)]
```
