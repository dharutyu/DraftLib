using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraftLib.Core;
using DraftLib.DAL;

namespace DraftLib.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork uow = new UnitOfWork();
            uow.Repository<Test>()
                .Query()
                .Filter(x => x.Id == 1)
                .Filter(x=> x.Created == DateTime.Now.AddDays(-7))
                .Include(x => x.Id)
                .Include(x => x.Modified)
                .OrderBy(x=> x.OrderByDescending(z=> z.Id))
                .Get();
            IQueryable<Test> t;
            

        }
    }

    internal class Test: EntityBase
    {
        public int Id { get; set; }
    }
}
