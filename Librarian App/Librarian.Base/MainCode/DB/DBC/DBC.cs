using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Librarian.DB
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public partial class DBC: DbContext
    {
        public static ConnectionStringAccessorCallback ConnectionStringAccessor;

        public DbSet<User_Item> Users { get; set; }

        public DBC()
        {
            Database.EnsureCreated();
        }

        /// <exception cref="InvalidOperationException" />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStrAccessor = Volatile.Read( ref ConnectionStringAccessor );
            if ( connStrAccessor == null )
                throw new InvalidOperationException(
                    "Не был обеспечен доступ к строке подключения." );
            string connectionStr = connStrAccessor();
            if ( connectionStr == null )
                throw new InvalidOperationException(
                    "Средство доступо к строке подключения вернуло пустой результат." );
            optionsBuilder.UseSqlServer( connectionStr );
        }
    }
}
