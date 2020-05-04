using System;
using System.Data;
using System.Linq;
using Xunit;

using System.Data.SqlClient;
using Xunit.Abstractions;

namespace LibraryProject2
{
    public class Linq2SqlDateTimeTests
    {
        private XunitConsoleForwarder consoleForwarder;
        public Linq2SqlDateTimeTests(ITestOutputHelper testOutputHelper)
        {
            consoleForwarder = new XunitConsoleForwarder(testOutputHelper);
        }

        [Fact]
        public void button1_Click()
        {
            using (var db = new DataClasses1DataContext(new SqlConnection(@"Data Source=.;Initial Catalog=Library;Integrated Security=True")))
            {
                if (!db.DatabaseExists())
                    db.CreateDatabase();
                else
                {
                    db.DeleteDatabase();
                    db.CreateDatabase();
                }
                db.Log = consoleForwarder;

                var cutOffTime = new TimeSpan(16, 30, 0);
                db.Books.Where(b => b.PublishDate.TimeOfDay >= cutOffTime && b.PublishDate.Date == DateTime.Today.Date).ToList();
            }
        }
    }
}
